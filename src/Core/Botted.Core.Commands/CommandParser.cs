using System;
using System.Collections.Generic;
using System.Linq;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Providers.Abstractions;
using Botted.Parsing.Converters.Abstractions.Abstractions;
using Pidgin;
using static Pidgin.Parser;

namespace Botted.Core.Commands
{
	/// <inheritdoc />
	public class CommandParser : ICommandParser
	{
		public static Parser<char, Unit> CommandPrefix { get; }
			= Char('!').IgnoreResult();

		public static Parser<char, Unit> Space { get; }
			= Char(' ').IgnoreResult();

		public static Parser<char, Unit> Brace { get; }
			= Char('"').IgnoreResult();

		public static Parser<char, string> Word { get; }
			= AnyCharExcept(' ', '"').AtLeastOnce()
									 .Map(s => new string(s.ToArray()));

		public static Parser<char, string> SimpleArgument { get; }
			= Word.Before(SkipWhitespaces);

		public static Parser<char, string> ComplexArgument { get; }
			= Brace.Then(SimpleArgument.AtLeastOnce()
									   .Map(s => string.Join(' ', s)))
				   .Before(Brace)
				   .Before(SkipWhitespaces);

		public static Parser<char, string> Argument { get; }
			= OneOf(SimpleArgument, ComplexArgument);

		public static Parser<char, string> CommandName { get; }
			= CommandPrefix.Then(Word).Before(Space.Optional());

		public static Parser<char, List<string>> Arguments { get; }
			= CommandName.IgnoreResult()
						 .Then(Argument.Many())
						 .Map(args => args.ToList());

		private readonly Dictionary<(Type, Type), Func<object, object>> _converters = new ();

		public CommandParser()
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var converters = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IConverter<,>)));
				foreach (var converter in converters)
				{
					var sourceType = converter.GenericTypeArguments[0];
					var targetType = converter.GenericTypeArguments[1];
					var convert = converter.GetMethod("Convert");
					var instance = Activator.CreateInstance(converter);
					_converters.Add((sourceType, targetType), d => convert.Invoke(instance, new [] { d }));
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
			var context = new CommandParsingContext(textArgs, null);

			foreach (var argumentStructure in structure.Arguments)
			{
				var source = argumentStructure.DataSource.Invoke(context, Array.Empty<object>());
				var sourceType = source.GetType();
				var targetType = argumentStructure.TargetProperty.GetParameters().First().ParameterType;
				if (sourceType != targetType)
				{
					var converter = _converters[(sourceType, targetType)];
					source = converter(source);
				}
				
				argumentStructure.TargetProperty.Invoke(data, new[] { source });
			}
			
			return data;
		}
	}
}