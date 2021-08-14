using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Data;

namespace Botted.Core.Abstractions.Services.Users.Data
{
	public class BotUser : IHasAdditionalData
	{
		private readonly List<IAdditionalData> _additionalData = new ();

		public ulong Id { get; set; }
		public string Nickname { get; set; }

		public TData? GetAdditionalData<TData>()
			where TData : IAdditionalData
			=> _additionalData.OfType<TData>().SingleOrDefault();

		public void SaveAdditionalData<TData>(TData data) 
			where TData : IAdditionalData 
			=> _additionalData.Add(data);
	}
}