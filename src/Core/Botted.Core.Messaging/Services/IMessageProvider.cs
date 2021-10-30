using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Messaging.Data;

namespace Botted.Core.Messaging.Services
{
	public interface IMessageProvider : IService
	{
		Task SendMessage(BottedMessage message);
	}
}