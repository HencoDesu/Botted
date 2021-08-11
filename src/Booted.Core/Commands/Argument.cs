using System;
using System.Linq.Expressions;
using Booted.Core.Commands.Abstractions;

namespace Booted.Core.Commands
{
	public class Argument<T> : IArgument
	{
		public Argument(MemberExpression member, Func<string, T> converter)
		{
			Member = member;
			Converter = _ => converter(_);
		}

		public Func<string, object> Converter { get; }
		
		public MemberExpression Member { get; }
	}
}