using System;
using System.Collections.Generic;
using Botted.Core.AdditionalData.Abstractions;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Core.Providers.Abstractions.Data
{
	public record Message : IHaveAdditionalData
	{
		private readonly Dictionary<Type, object> _additionalData = new();
		
		public Message(string text)
			: this(text, ProviderIdentifier.Any, null)
		{ }

		public Message(string text,
					   ProviderIdentifier provider,
					   User? user)
		{
			Text = text;
			Provider = provider;
			User = user;
		}

		public string Text { get; init; }
		public ProviderIdentifier Provider { get; }

		public User? User { get; }

		/// <inheritdoc />
		public TData? GetAdditionalData<TData>() 
			where TData : class
		{
			var dataType = typeof(TData);
			if (_additionalData.ContainsKey(dataType))
			{
				return (TData) _additionalData[dataType];
			}

			return null;
		}

		/// <inheritdoc />
		public void SetAdditionalData<TData>(TData data)
			where TData : class
		{
			var dataType = typeof(TData);
			if (_additionalData.ContainsKey(dataType))
			{
				_additionalData[dataType] = data;
			} else
			{
				_additionalData.Add(dataType, data);
			}
		}
	}
}