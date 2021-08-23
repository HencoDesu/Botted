using System;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Result;

namespace Botted.Core.Commands.Abstractions
{
	/// <summary>
	/// Base class for all commands
	/// </summary>
	/// <typeparam name="TData">Command data type</typeparam>
	public abstract class AbstractCommand<TData> 
		: ICommand<TData> 
		where TData : class, ICommandData
	{
		protected AbstractCommand(string name)
		{
			Name = name;
		}

		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public abstract ICommandResult Execute(TData data);

		/// <summary>
		/// Provides a <see cref="OkCommandResult"/>
		/// </summary>
		/// <param name="message">Result message</param>
		/// <returns><see cref="OkCommandResult"/></returns>
		protected ICommandResult Ok(string message) 
			=> new OkCommandResult(message);

		/// <summary>
		/// Provides a <see cref="ErrorCommandResult"/>
		/// </summary>
		/// <param name="message">Result message</param>
		/// <returns><see cref="ErrorCommandResult"/></returns>
		protected ICommandResult Error(string message)
			=> new ErrorCommandResult(message);

		/// <summary>
		/// Provides a <see cref="ErrorCommandResult"/>
		/// </summary>
		/// <param name="exception"></param>
		/// <returns><see cref="ErrorCommandResult"/></returns>
		protected ICommandResult Error(Exception exception)
			=> new ErrorCommandResult(exception.Message);
	}
}