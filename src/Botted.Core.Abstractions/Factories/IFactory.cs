namespace Botted.Core.Abstractions.Factories
{
	public interface IFactory
	{
		object Create();
	}
	
	public interface IFactory<out TResult> : IFactory
	{
		new TResult Create();
	}
}