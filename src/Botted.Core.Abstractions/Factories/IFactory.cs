using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Abstractions.Factories
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public interface IFactory
	{
		object Create();
	}
	
	public interface IFactory<out TResult> : IFactory
	{
		new TResult Create();
	}
}