using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Result;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Represents a command that can be executed
	/// </summary>
	/// <typeparam name="TData">Command data</typeparam>
	public interface ICommand<in TData> 
		where TData : class, ICommandData
	{
		/// <summary>
		/// Name of that command
		/// </summary>
		public string Name { get; }
		
		/// <summary>
		/// Executes command
		/// </summary>
		/// <param name="data">Command data</param>
		/// <returns>Execution result</returns>
		ICommandResult Execute(TData data);
	}
}