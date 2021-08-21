using System;
using Autofac;
using Botted.Core.Abstractions;
using Botted.Core.Abstractions.Factories;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Commands.Structure;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Parsing;
using Botted.Core.Extensions;

namespace Botted.Tests.TestEnvironment
{
	public abstract class BaseTest
	{
		protected IContainer Container { get; }

		protected BaseTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
				   .PublicOnly()
				   .Where(t => t.InheritedFrom(typeof(IEvent))
							|| t.InheritedFrom(typeof(IService))
							|| t.InheritedFrom(typeof(ICommand))
							|| t.InheritedFrom(typeof(IFactory))
							|| t.InheritedFrom(typeof(IConverter<,>))
							|| t.InheritedFrom(typeof(IEnumConverter<>))
							|| t.InheritedFrom(typeof(ICommandStructureBuilder<>)))
				   .AsImplementedInterfaces()
				   .AsSelf()
				   .InstancePerLifetimeScope();
			
			builder.RegisterType<TestDatabase>()
				   .AsImplementedInterfaces()
				   .AsSelf()
				   .InstancePerLifetimeScope();

			Container = builder.Build();
		}
	}
}