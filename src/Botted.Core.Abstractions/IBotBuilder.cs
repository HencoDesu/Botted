namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Builder for <see cref="IBot"/>
	/// </summary>
	public interface IBotBuilder
	{
		/// <summary>
		/// Registers a service to bot's DI container
		/// </summary>
		/// <typeparam name="TInterface">Service interface</typeparam>
		/// <typeparam name="TImplementation">Service implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		IBotBuilder RegisterService<TInterface, TImplementation>() 
			where TInterface : notnull
			where TImplementation : TInterface;

		/// <summary>
		/// Registers a service to bot's DI container
		/// </summary>
		/// <param name="instance">Instance to register</param>
		/// <typeparam name="TInterface">Service interface</typeparam>
		/// <typeparam name="TImplementation">Service implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public IBotBuilder RegisterService<TInterface, TImplementation>(TImplementation instance)
			where TInterface : notnull
			where TImplementation : class, TInterface;
		
		/// <summary>
		/// Registers a service to bot's DI container
		/// </summary>
		/// <param name="instance">Instance to register</param>
		/// <typeparam name="TService">Service implementation</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public IBotBuilder RegisterService<TService>(TService instance)
			where TService : class;
		
		/// <summary>
		/// Builds a bot
		/// </summary>
		/// <returns></returns>
		IBot BuildBot();
	}
}