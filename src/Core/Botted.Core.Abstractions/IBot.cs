namespace Botted.Core.Abstractions
{
	/// <summary>
	/// Provides an abstraction for bot 
	/// </summary>
	public interface IBot
	{
		/// <summary>
		/// Starts bot
		/// </summary>
		void Start();

		/// <summary>
		/// Stops bot
		/// </summary>
		void Stop();
	}
}