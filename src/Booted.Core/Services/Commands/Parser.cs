using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Pidgin;
using static Pidgin.Parser;

namespace Booted.Core.Services.Commands
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public static class Parser
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
	}
}