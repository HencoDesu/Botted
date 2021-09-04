using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Botted.Core.AdditionalData.Abstractions;

namespace Botted.Core.Users.Abstractions.Data
{
	/// <summary>
	/// Represents a single bot user
	/// </summary>
	[Table("Users")]
	public class User : IHaveAdditionalData
	{
		[NotMapped]
		private readonly Dictionary<Type, object> _additionalData = new();

		/// <summary>
		/// Unique identifier of that <see cref="User"/>
		/// </summary>
		[Key]
		[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
		public long Id { get; set; }
		
		/// <summary>
		/// User's nickname
		/// </summary>
		public string Nickname { get; set; }

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
	};
}