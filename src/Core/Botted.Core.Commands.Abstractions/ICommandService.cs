using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Service that responsible for command handling
	/// </summary>
	public interface ICommandService
	{
		void RegisterCommand<TData>(ICommand<TData> command) 
			where TData : class, ICommandData;
	}
}