namespace Botted.Core.AdditionalData.Abstractions
{
	public static class AdditionalDataExtensions
	{
		public static TDataHost WithAdditionalData<TDataHost, TData>(this TDataHost dataHost, TData data)
			where TDataHost : IHaveAdditionalData where TData : class
		{
			dataHost.SetAdditionalData(data);
			return dataHost;
		}
	}
}