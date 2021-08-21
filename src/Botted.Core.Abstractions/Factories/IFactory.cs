namespace Botted.Core.Abstractions.Factories
{
	public interface IFactory
	{ }

	public interface IFactory<out TResult> : IFactory
	{
		TResult Create();
	}
}