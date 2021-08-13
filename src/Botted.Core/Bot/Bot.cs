using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Services;

namespace Botted.Core.Bot
{
	public class Bot : IBot
	{
		// services param needed for service initialization from DI container
		[SuppressMessage("ReSharper", "UnusedParameter.Local")]
		public Bot(IEnumerable<IService> services)
		{
		}
	}
}