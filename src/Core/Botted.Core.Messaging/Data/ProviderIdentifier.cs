using System;
using System.Collections.Generic;

namespace Botted.Core.Messaging.Data
{
	public class ProviderIdentifier : IEquatable<ProviderIdentifier>
	{
		public static ProviderIdentifier Any { get; } = Create();
		
		private readonly List<ProviderIdentifier> _innerIdentifiers = new();
		
		private ProviderIdentifier() {}

		private bool IsComplex => _innerIdentifiers.Count > 0;

		public static ProviderIdentifier operator |(ProviderIdentifier left, ProviderIdentifier right)
		{
			if (left.IsComplex)
			{
				left._innerIdentifiers.Add(right);
				return left;
			}

			var complex = new ProviderIdentifier();
			complex._innerIdentifiers.Add(left);
			complex._innerIdentifiers.Add(right);
			return complex;
		}

		public static bool operator ==(ProviderIdentifier left, ProviderIdentifier right)
		{
			return left.IsMatching(right) || right.IsMatching(left);
		}

		public static bool operator !=(ProviderIdentifier left, ProviderIdentifier right)
		{
			return !(left == right);
		}

		public static ProviderIdentifier Create() => new();

		private bool IsMatching(ProviderIdentifier other)
		{
			return ReferenceEquals(this, other) 
				|| IsComplex 
				&& (other.IsComplex 
					   ? _innerIdentifiers.Equals(other._innerIdentifiers) 
					   : _innerIdentifiers.Contains(other));
		}

		public bool Equals(ProviderIdentifier? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _innerIdentifiers.Equals(other._innerIdentifiers);
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ProviderIdentifier) obj);
		}

		public override int GetHashCode()
		{
			return _innerIdentifiers.GetHashCode();
		}
	}
}