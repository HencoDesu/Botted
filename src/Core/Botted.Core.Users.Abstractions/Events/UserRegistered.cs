using System.Diagnostics.CodeAnalysis;
using Botted.Core.Events.Abstractions.Events;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Users.Abstractions.Events
{
	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
	public class UserRegistered : EventWithData<User> { }
}