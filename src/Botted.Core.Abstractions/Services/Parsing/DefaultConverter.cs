using System;
using Botted.Core.Abstractions.Services.Users.Data;

namespace Botted.Core.Abstractions.Services.Parsing
{
	public class DefaultConverter : IConverter<string, string>,
									IConverter<string, int>,
									IConverter<BotUser, BotUser>,
									IEnumConverter<string>
	{
		string IConverter<string, string>.Convert(string data) 
			=> data;
		
		BotUser IConverter<BotUser, BotUser>.Convert(BotUser data) 
			=> data;

		int IConverter<string, int>.Convert(string data) 
			=> int.Parse(data);

		TEnum IEnumConverter<string>.Convert<TEnum>(string data)
		{
			if (Enum.TryParse(data, true, out TEnum result))
			{
				return result;
			}

			throw new ArgumentOutOfRangeException(nameof(data));
		}
	}
}