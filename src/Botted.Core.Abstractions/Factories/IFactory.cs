namespace Botted.Core.Abstractions.Factories
{
	public interface IFactory<out TResult>
	{
		TResult Create();
	}
}