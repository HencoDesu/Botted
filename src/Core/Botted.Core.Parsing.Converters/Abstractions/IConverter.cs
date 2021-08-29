namespace Botted.Parsing.Converters.Abstractions
{
	public interface IConverter<in TSource, out TTarget>
	{
		TTarget? Convert(TSource source, object? argument);
	}
}