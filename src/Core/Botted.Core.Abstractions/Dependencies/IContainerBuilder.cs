using System;

namespace Botted.Core.Abstractions.Dependencies
{
	/// <summary>
	/// Provides a wrapper for any DI container builder
	/// </summary>
	public interface IContainerBuilder
	{
		/// <summary>
		/// Shortcut to register any <see cref="IBottedService"/>
		/// </summary>
		/// <typeparam name="TAbstraction"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		void RegisterService<TAbstraction, TImplementation>()
			where TImplementation : TAbstraction
			where TAbstraction : IBottedService;

		/// <summary>
		/// Register a singleton dependency which needs to resolve some services first
		/// </summary>
		/// <param name="creator">Constructor call</param>
		/// <typeparam name="T">Dependency type</typeparam>
		void RegisterSingleton<T>(Func<IContainer, T> creator)
			where T : notnull;
		
		/// <summary>
		/// Register a singleton dependency by ready instance
		/// </summary>
		/// <param name="instance">Instance ready to use</param>
		/// <typeparam name="T">Dependency type</typeparam>
		void RegisterSingleton<T>(T instance)
			where T : class;
		
		/// <summary>
		/// Register a singleton dependency by it's type
		/// </summary>
		/// <typeparam name="T">Dependency type</typeparam>
		void RegisterSingleton<T>() 
			where T : notnull;

		/// <summary>
		/// Register a singleton dependency by it's abstraction and implementation
		/// </summary>
		/// <param name="instance">Instance ready to use</param>
		/// <typeparam name="TAbstraction">Abstraction type</typeparam>
		/// <typeparam name="TImplementation">Implementation type</typeparam>
		void RegisterSingleton<TAbstraction, TImplementation>(TImplementation instance)
			where TImplementation : class, TAbstraction
			where TAbstraction : class;
		
		/// <summary>
		/// Register a singleton dependency by it's abstraction and implementation
		/// </summary>
		/// <typeparam name="TAbstraction">Abstraction type</typeparam>
		/// <typeparam name="TImplementation">Implementation type</typeparam>
		void RegisterSingleton<TAbstraction, TImplementation>()
			where TImplementation : TAbstraction
			where TAbstraction : notnull;
		
		/// <summary>
		/// Registers a configuration section
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <typeparam name="TConfigSection">Typed section content</typeparam>
		void RegisterConfigurationSection<TConfigSection>(string sectionName)
			where TConfigSection : notnull;
		
		/// <summary>
		/// Registers a callback executed when DI container will be built
		/// </summary>
		/// <param name="callback"></param>
		void RegisterBuildCallback(Action<IContainer> callback);
	}
}