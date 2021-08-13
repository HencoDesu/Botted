using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Services.Users.Events
{
	public sealed class UserRegistered : IEvent<BotUser> { }
}