namespace Botted.Parsing.Converters.Abstractions.Abstractions
{
	public interface IConverter<in TSource, out TTarget>
	{
		TTarget Convert(TSource source);
	}
}