namespace Botted.Core.Abstractions.Builders
{
	/// <summary>
	/// Simple abstraction for Builder pattern
	/// </summary>
	/// <typeparam name="TResult">Type of built object</typeparam>
	public interface IBuilder<out TResult>
	{
		/// <summary>
		/// Creates a new instance of the result type
		/// with parameters defined by implementation of <see cref="IBuilder{TResult}"/>
		/// </summary>
		/// <returns>Configured instance</returns>
		TResult Build();
	}
}