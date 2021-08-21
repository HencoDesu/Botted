using System;
using Autofac;

namespace Botted.Core.Extensions
{
	public static class ContainerBuilderExtensions
	{
		public static void RegisterInheritedTypes(this ContainerBuilder builder, 
												  Type type)
			=> builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
					  .PublicOnly()
					  .Where(t => t.InheritedFrom(type))
					  .AsImplementedInterfaces()
					  .AsSelf()
					  .SingleInstance();
	}
}