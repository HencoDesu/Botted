using System;
using System.Threading.Tasks;
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
		public abstract Task<ICommandResult> Execute(TData data);

		/// <summary>
		/// Provides a <see cref="OkCommandResult"/>
		/// </summary>
		/// <param name="message">Result message</param>
		/// <returns><see cref="OkCommandResult"/></returns>
		protected Task<ICommandResult> Ok(string message) 
			=> Task.FromResult<ICommandResult>(new OkCommandResult(message));

		/// <summary>
		/// Provides a <see cref="ErrorCommandResult"/>
		/// </summary>
		/// <param name="message">Result message</param>
		/// <returns><see cref="ErrorCommandResult"/></returns>
		protected Task<ICommandResult> Error(string message)
			=> Task.FromResult<ICommandResult>( new ErrorCommandResult(message));

		/// <summary>
		/// Provides a <see cref="ErrorCommandResult"/>
		/// </summary>
		/// <param name="exception"></param>
		/// <returns><see cref="ErrorCommandResult"/></returns>
		protected Task<ICommandResult> Error(Exception exception)
			=> Task.FromResult<ICommandResult>(new ErrorCommandResult(exception.Message));
	}

	/// <inheritdoc />
	public abstract class AbstractCommand : AbstractCommand<EmptyCommandData>
	{
		protected AbstractCommand(string name) : base(name)
		{ }
		
		public override Task<ICommandResult> Execute(EmptyCommandData data)
			=> Execute();

		public abstract Task<ICommandResult> Execute();
	}
}