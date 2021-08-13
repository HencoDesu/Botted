using System;
using System.Linq;

namespace Booted.Core.Extensions
{
	public static class ReflectionExtensions
	{
		public static bool InheritedFrom(this Type typeToCheck,
										 Type baseType)
		{
			if (typeToCheck.GUID == baseType.GUID)
			{
				return true;
			}
			
			if (baseType.IsInterface)
			{
				return typeToCheck.GetInterfaces().Any(i => i.GUID == baseType.GUID);
			}

			if (baseType.IsClass && typeToCheck.BaseType is not null)
			{
				return typeToCheck.BaseType.InheritedFrom(baseType);
			}

			return false;
		}
	}
}