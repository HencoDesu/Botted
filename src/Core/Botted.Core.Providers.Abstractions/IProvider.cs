using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Providers.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions
{
	public interface IProviderService : IService
	{
		Task SendMessage(Message message);
	}
}