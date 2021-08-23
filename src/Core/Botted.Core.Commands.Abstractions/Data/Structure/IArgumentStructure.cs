using System.Reflection;

namespace Botted.Core.Commands.Abstractions.Data.Structure
{
	/// <summary>
	/// Argument structure used for data parsing
	/// </summary>
	public interface IArgumentStructure
	{
		/// <summary>
		/// Data property setter method
		/// </summary>
		MethodInfo TargetProperty { get; }
		
		/// <summary>
		/// Parsing context source for that argument
		/// </summary>
		MethodInfo DataSource { get; }
		
		/// <summary>
		/// Marks that argument is optional
		/// </summary>
		bool Optional { get; }
	}
}