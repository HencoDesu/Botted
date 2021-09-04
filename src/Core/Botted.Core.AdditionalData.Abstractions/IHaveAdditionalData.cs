namespace Botted.Core.AdditionalData.Abstractions
{
	/// <summary>
	/// Indicates that object can provide additional data
	/// </summary>
	public interface IHaveAdditionalData
	{
		/// <summary>
		/// Get additional data
		/// </summary>
		/// <typeparam name="TData">Data type</typeparam>
		/// <returns>Additional data if present, null otherwise</returns>
		TData? GetAdditionalData<TData>()
			where TData : class;
		
		/// <summary>
		/// Set additional data
		/// </summary>
		/// <param name="data">Data to set</param>
		/// <typeparam name="TData">Data type</typeparam>
		void SetAdditionalData<TData>(TData data)
			where TData : class;
	}
}