using Botted.Parsing.Converters.Abstractions.Abstractions;

namespace Botted.Parsing.Converters.Abstractions
{
	/// <inheritdoc />
	public class StringToIntConverter : IConverter<string, int>
	{
		public int Convert(string source) => int.Parse(source);
	}
}