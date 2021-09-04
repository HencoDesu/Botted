using System;
using Botted.Core.Commands.Abstractions.Data;
using Botted.Core.Commands.Abstractions.Data.Structure;
using Botted.Core.Users.Abstractions.Data;

namespace Botted.Tests.CoreTests.TestData
{
	public class TestCommandData : ICommandData, IEquatable<TestCommandData>
	{
		public static ICommandDataStructure Structure { get; }
			= ICommandData.GetBuilder(() => new TestCommandData())
						  .WithArgument(d => d.IntData, c => c.TextArgs[0])
						  .WithArgument(d => d.StringData, c => c.TextArgs[1])
						  .WithArgument(d => d.EnumData, c => c.TextArgs[2], true)
						  .WithArgument(d => d.User, c => c.User)
						  .Build();

		public TestCommandData()
		{ }

		public TestCommandData(int intData, 
							   string stringData, 
							   TestEnum enumData, 
							   User userData)
		{
			IntData = intData;
			StringData = stringData;
			EnumData = enumData;
			User = userData;
		}
		
		public int IntData { get; set; }

		public string StringData { get; set; } = string.Empty;
			
		public TestEnum EnumData { get; set; }
		
		public User? User { get; set; }

		/// <inheritdoc />
		public bool Equals(TestCommandData? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return IntData == other.IntData 
				&& StringData == other.StringData
				&& EnumData == other.EnumData 
				&& Equals(User, other.User);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return !ReferenceEquals(null, obj) 
				&& (ReferenceEquals(this, obj) 
				  || obj.GetType() == GetType() 
				 && Equals((TestCommandData) obj));
		}

		public static bool operator ==(TestCommandData? left, TestCommandData? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TestCommandData? left, TestCommandData? right)
		{
			return !Equals(left, right);
		}
	}
}