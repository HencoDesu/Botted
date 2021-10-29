namespace Botted.Core.Abstractions.Dependencies
{
	public interface IContainer
	{
		T Resolve<T>() 
			where T : notnull;
	}
}