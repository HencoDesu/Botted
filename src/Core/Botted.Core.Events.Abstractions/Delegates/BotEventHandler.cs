using System.Threading.Tasks;

namespace Botted.Core.Events.Abstractions.Delegates
{
	public delegate void BotEventHandler();

	public delegate void BotEventHandler<in TData>(TData eventData);

	public delegate Task AsyncEventHandler();

	public delegate Task AsyncEventHandler<in TData>(TData eventData);
}