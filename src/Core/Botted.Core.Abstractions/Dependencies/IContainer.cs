namespace Botted.Core.Abstractions.Dependencies
{
	/// <summary>
	/// Provides simple abstraction to wrap any DI containers
	/// </summary>
	public interface IContainer
	{
		/// <summary>
		/// Resolve any dependency from wrapped DI container
		/// </summary>
		/// <typeparam name="T">Dependency type</typeparam>
		/// <returns>Resolved dependency</returns>
		T Resolve<T>() 
			where T : notnull;
	}
}