namespace Botted.Core.Abstractions.Data
{
	public interface IHasAdditionalData
	{
		TData? GetAdditionalData<TData>() 
			where TData : IAdditionalData;

		void SaveAdditionalData<TData>(TData data)
			where TData : IAdditionalData;
	}
}