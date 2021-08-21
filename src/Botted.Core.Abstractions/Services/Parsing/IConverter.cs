using System;

namespace Botted.Core.Abstractions.Services.Parsing
{
	public interface IConverter<in TSource, out TTarget>
	{
		TTarget Convert(TSource data);
	}
	
	public interface IEnumConverter<in TSource>
	{
		TEnum Convert<TEnum>(TSource data) 
			where TEnum : struct, Enum;
	}
}