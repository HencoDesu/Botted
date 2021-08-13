using System;
using System.Collections.Generic;

namespace Botted.Core.Abstractions.Services.Providers
{
	public class ProviderIdentifier
	{
		public static ProviderIdentifier Any { get; } = new();
		
		private readonly List<ProviderIdentifier> _additionalIdentifiers = new();
		
		public Guid Id { get; } = Guid.NewGuid();

		public static ProviderIdentifier operator | (ProviderIdentifier left, ProviderIdentifier right)
		{
			if (left._additionalIdentifiers.Count > 0)
			{
				left.AddIdentifier(right);
				return left;
			} 
			
			var combined = new ProviderIdentifier();
			combined.AddIdentifier(left);
			combined.AddIdentifier(right);
			return combined;
		}

		public bool IsMatching(ProviderIdentifier providerIdentifier)
		{
			return this == Any 
				|| this == providerIdentifier 
				|| _additionalIdentifiers.Contains(providerIdentifier);
		}

		private void AddIdentifier(ProviderIdentifier additionalIdentifier) 
			=> _additionalIdentifiers.Add(additionalIdentifier);
	}
}