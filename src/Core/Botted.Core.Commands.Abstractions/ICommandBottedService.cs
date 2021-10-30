using Botted.Core.Abstractions;
using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Service that responsible for command handling
	/// </summary>
	public interface ICommandBottedService : IBottedService
	{
		/// <summary>
		/// Registers a command to handle message containing it
		/// </summary>
		/// <param name="command">Command to register</param>
		/// <typeparam name="TData">Command data type</typeparam>
		void RegisterCommand<TData>(ICommand<TData> command) 
			where TData : class, ICommandData;
	}
}