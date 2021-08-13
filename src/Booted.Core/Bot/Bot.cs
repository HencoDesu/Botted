using System.Collections.Generic;
using System.Linq;
using Botted.Core.Abstractions.Bot;
using Botted.Core.Abstractions.Data;
using Botted.Core.Abstractions.Services;
using Botted.Core.Abstractions.Services.Commands;
using Botted.Core.Abstractions.Services.Events;
using Botted.Core.Abstractions.Services.Providers;
using Pidgin;
using Parser = Booted.Core.Services.Commands.Parser;

namespace Booted.Core.Bot
{
	public class Bot : IBot
	{
		public Bot(IEnumerable<IService> services)
		{
		}
	}
}