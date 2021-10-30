using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Botted.Core.Users.Abstractions.Data
{
	/// <summary>
	/// Represents a single bot user
	/// </summary>
	[Table("Users")]
	public class BottedUser
	{
		/// <summary>
		/// Unique identifier of that <see cref="BottedUser"/>
		/// </summary>
		[Key]
		[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
		public long Id { get; set; }
		
		/// <summary>
		/// User's nickname
		/// </summary>
		public string? Nickname { get; set; }
	};
}