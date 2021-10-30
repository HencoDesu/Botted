using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Context;
using Botted.Core.Messaging.Data;
using Botted.Parsing.Converters.Abstractions;
using Pidgin;
using static Pidgin.Parser;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	public class CommandParser : ICommandParser
	{
		private Dictionary<(Type, Type), Func<object, object?, object>> _converters = new ();

		public CommandParser(CommandsConfiguration configuration)
		{
			RegisterConverters();

			CommandPrefix = Char(configuration.CommandPrefix).IgnoreResult();
			Space = Char(' ').IgnoreResult();
			Quote = Char('"').IgnoreResult();
			Word = AnyCharExcept(' ', '"').AtLeastOnce()
										  .Map(s => new string(s.ToArray()));
			SimpleArgument = Word.Before(SkipWhitespaces);
			ComplexArgument = Quote.Then(SimpleArgument.AtLeastOnce()
													   .Map(s => string.Join(' ', s)))
								   .Before(Quote)
								   .Before(SkipWhitespaces);
			Argument = OneOf(SimpleArgument, ComplexArgument);
			CommandName = CommandPrefix.Then(Word).Before(Space.Optional());
			Arguments = CommandName.IgnoreResult()
								   .Then(Argument.Many())
								   .Map(args => args.ToList());
		}

		#region Parsers

		private Parser<char, Unit> CommandPrefix { get; }
		private Parser<char, Unit> Space { get; }
		private Parser<char, Unit> Quote { get; }
		private Parser<char, string> Word { get; }
		private Parser<char, string> SimpleArgument { get; }
		private Parser<char, string> ComplexArgument { get; }
		private Parser<char, string> Argument { get; }
		private Parser<char, string> CommandName { get; }
		private Parser<char, List<string>> Arguments { get; }

		#endregion

		public bool TryParseCommandName(BottedMessage message, out string commandName)
		{
			var result = CommandName.Parse(message.Text);
			commandName = result.Success ? result.Value : string.Empty;
			return result.Success;
		}

		public ICommandData ParseCommandData(BottedMessage message, ICommandDataStructure structure)
		{
			var data = structure.Empty;
			var textArgs = Arguments.ParseOrThrow(message.Text);
			var context = new CommandParsingContext(textArgs, message.Sender);

			foreach (var argumentStructure in structure.Arguments)
			{
				try
				{
					var source = argumentStructure.DataSource.Invoke(context)!;
					var sourceType = source.GetType();
					var targetType = argumentStructure.TargetProperty.GetParameters().First().ParameterType;
					if (sourceType != targetType)
					{
						var converter = targetType.IsEnum
							? _converters[(sourceType, typeof(Enum))]
							: _converters[(sourceType, targetType)];
						source = converter(source, targetType);
					}

					argumentStructure.TargetProperty.Invoke(data, new[] { source });
				} catch (Exception e)
				{
					if (!argumentStructure.Optional)
					{
						//TODO: Custom exception here
						throw new Exception("Error while parsing argument", e);
					}
				}
			}
			
			return data;
		}

		private void RegisterConverters()
		{
			var converterInterface = typeof(IConverter<,>);
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var converters = assembly.GetTypes()
										 .Where(t => !t.IsInterface)
										 .Where(t => t.GetInterface(converterInterface.Name) is not null);
				foreach (var converter in converters)
				{
					var interfaces = converter.GetInterface(converterInterface.Name)!;
					var sourceType = interfaces.GenericTypeArguments[0];
					var targetType = interfaces.GenericTypeArguments[1];
					var convert = converter.GetMethod("Convert")!;
					var instance = Activator.CreateInstance(converter);
					_converters.Add((sourceType, targetType), (d, a) => convert.Invoke(instance, new [] { d, a })!);
				}
			}
		}
	}
}