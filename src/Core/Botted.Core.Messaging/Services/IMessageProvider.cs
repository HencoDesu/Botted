using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Messaging.Data;

namespace Botted.Core.Messaging.Services
{
	public interface IMessageProvider : IBottedService
	{
		Task SendMessage(BottedMessage message);
	}
}