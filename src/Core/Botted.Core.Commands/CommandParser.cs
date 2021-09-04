using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Commands.Context;
using Botted.Core.Providers.Abstractions.Data;
using Botted.Parsing.Converters.Abstractions;
using Pidgin;
using static Pidgin.Parser;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	public class CommandParser : ICommandParser
	{
		private static Parser<char, Unit> CommandPrefix { get; }
			= Char('!').IgnoreResult();

		private static Parser<char, Unit> Space { get; }
			= Char(' ').IgnoreResult();

		private static Parser<char, Unit> Brace { get; }
			= Char('"').IgnoreResult();

		private static Parser<char, string> Word { get; }
			= AnyCharExcept(' ', '"').AtLeastOnce()
									 .Map(s => new string(s.ToArray()));

		private static Parser<char, string> SimpleArgument { get; }
			= Word.Before(SkipWhitespaces);

		private static Parser<char, string> ComplexArgument { get; }
			= Brace.Then(SimpleArgument.AtLeastOnce()
									   .Map(s => string.Join(' ', s)))
				   .Before(Brace)
				   .Before(SkipWhitespaces);

		private static Parser<char, string> Argument { get; }
			= OneOf(SimpleArgument, ComplexArgument);

		private static Parser<char, string> CommandName { get; }
			= CommandPrefix.Then(Word).Before(Space.Optional());

		private static Parser<char, List<string>> Arguments { get; }
			= CommandName.IgnoreResult()
						 .Then(Argument.Many())
						 .Map(args => args.ToList());

		private readonly Dictionary<(Type, Type), Func<object, object?, object>> _converters = new ();

		public CommandParser()
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

		public bool TryParseCommandName(Message message, out string commandName)
		{
			var result = CommandName.Parse(message.Text);
			commandName = result.Success ? result.Value : string.Empty;
			return result.Success;
		}

		public ICommandData ParseCommandData(Message message, ICommandDataStructure structure)
		{
			var data = structure.Empty;
			var textArgs = Arguments.ParseOrThrow(message.Text);
			var context = new CommandParsingContext(textArgs, message.User);

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
	}
}