using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Data;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Extensions;
using Pidgin;
using static Pidgin.Parser;

namespace Botted.Core.Services.Commands
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public class Parser
	{
		public static Parser<char, Unit> CommandPrefix { get; }
			= Char('!').IgnoreResult();

		public static Parser<char, Unit> Space { get; }
			= Char(' ').IgnoreResult();

		public static Parser<char, Unit> Brace { get; }
			= Char('"').IgnoreResult();

		public static Parser<char, string> Word { get; }
			= AnyCharExcept(' ', '"').AtLeastOnce().Map(s => new string(s.ToArray()));

		public static Parser<char, string> SimpleArgument { get; }
			= Word.Before(SkipWhitespaces);

		public static Parser<char, string> ComplexArgument { get; }
			= Brace.Then(SimpleArgument.AtLeastOnce().Map(s => string.Join(' ', s)))
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

		public static string ParseCommandName(BotMessage message) 
			=> CommandName.ParseOrThrow(message.Text);

		public static ICommandData? ParseCommandData(BotMessage message, ICommandStructure? structure)
		{
			if (structure is null) return null;
			
			var data = structure.DataFactory();
			var textArgs = Arguments.ParseOrThrow(message.Text);
			var context = new CommandParsingContext(message.User, textArgs);
			for (var i = 0; i < textArgs.Count; i++)
			{
				var argument = structure.Arguments[i];
				var value = argument.Source.Invoke(context);
				argument.Target.Invoke(data, value!);
			}
			
			return data;
		}
	}
}