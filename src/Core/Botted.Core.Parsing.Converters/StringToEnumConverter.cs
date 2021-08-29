using System;
using Botted.Parsing.Converters.Abstractions;

namespace Botted.Parsing.Converters
{
	/// <inheritdoc />
	public class StringToEnumConverter : IConverter<string, Enum>
	{
		
		public Enum? Convert(string source, object? argument)
		{
			return Enum.TryParse(argument as Type, source, true, out var result) ? result as Enum : null;
		}
	}
}