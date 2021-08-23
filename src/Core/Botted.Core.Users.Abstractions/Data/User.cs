using System.ComponentModel.DataAnnotations;

namespace Botted.Core.Users.Abstractions.Data
{
	/// <summary>
	/// Represents a single bot user
	/// </summary>
	public record User
	{
		/// <summary>
		/// Unique identifier of that <see cref="User"/>
		/// </summary>
		[Key]
		public long Id { get; }
	};
}