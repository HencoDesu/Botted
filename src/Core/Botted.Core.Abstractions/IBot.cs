using Microsoft.Extensions.Hosting;

namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Provides an abstraction for bot 
	/// </summary>
	public interface IBot : IHostedService
	{ }
}