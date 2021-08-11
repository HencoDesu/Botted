using System;
using System.Reflection;
using Booted.Core.Commands.Abstractions;
using Booted.Core.Commands.Structure.Abstractions;

namespace Booted.Core.Commands.Structure
{
	public class ArgumentStructure<TValue> : IArgumentStructure
	{
		private readonly PropertyInfo _property;
		private readonly Func<string, TValue> _converter;

		public ArgumentStructure(PropertyInfo property, Func<string, TValue> converter)
		{
			_property = property;
			_converter = converter;
		}

		public void PopulateValue(ICommandData data, string value)
		{
			_property.SetMethod!.Invoke(data, new object[] { _converter(value)! });
		}
	}
}