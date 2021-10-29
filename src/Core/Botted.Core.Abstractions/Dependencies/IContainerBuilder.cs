using System;

namespace Botted.Core.Abstractions.Dependencies
{
	public interface IContainerBuilder
	{
		void RegisterService<T>() 
			where T : IService;
		
		void RegisterService<TAbstraction, TImplementation>()
			where TImplementation : TAbstraction
			where TAbstraction : IService;

		void RegisterSingleton<T>(Func<IContainer, T> creator)
			where T : notnull;
		
		void RegisterSingleton<T>(T instance)
			where T : class;
		
		void RegisterSingleton<T>() 
			where T : notnull;

		void RegisterSingleton<TAbstraction, TImplementation>(Func<IContainer, TImplementation> creator)
			where TImplementation : TAbstraction
			where TAbstraction : notnull;

		void RegisterSingleton<TAbstraction, TImplementation>(TImplementation instance)
			where TImplementation : class, TAbstraction
			where TAbstraction : class;
		
		void RegisterSingleton<TAbstraction, TImplementation>()
			where TImplementation : TAbstraction
			where TAbstraction : notnull;
		
		void RegisterConfigurationSection<TConfigSection>(string sectionName)
			where TConfigSection : notnull;
		
		void RegisterBuildCallback(Action<IContainer> callback);
	}
}