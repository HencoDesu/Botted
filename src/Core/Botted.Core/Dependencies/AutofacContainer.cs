using Autofac;
using IContainer = Botted.Core.Abstractions.Dependencies.IContainer;

namespace Botted.Core.Dependencies
{
	public class AutofacContainer : IContainer
	{
		private readonly IComponentContext _scope;

		public AutofacContainer(IComponentContext scope)
		{
			_scope = scope;
		}

		public T Resolve<T>() 
			where T : notnull
		{
			return _scope.Resolve<T>();
		}
	}
}