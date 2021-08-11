using System;
using System.Linq.Expressions;

namespace Booted.Core.Commands.Abstractions
{
	public interface IArgument
	{
		public Func<string, object?> Converter { get; }
		
		public MemberExpression Member { get; }
	}
}