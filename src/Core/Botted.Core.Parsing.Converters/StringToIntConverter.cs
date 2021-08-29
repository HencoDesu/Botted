using Botted.Parsing.Converters.Abstractions;

namespace Botted.Parsing.Converters
{
	/// <inheritdoc />
	public class StringToIntConverter : IConverter<string, int>
	{
		public int Convert(string source, object? argument) => int.Parse(source);
	}
}