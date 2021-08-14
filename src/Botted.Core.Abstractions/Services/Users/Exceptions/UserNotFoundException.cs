using System;
using System.Runtime.Serialization;

namespace Botted.Core.Abstractions.Services.Users.Exceptions
{
	[Serializable]
	public class UserNotFoundException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public UserNotFoundException()
		{ }

		public UserNotFoundException(string message) : base(message)
		{ }

		public UserNotFoundException(string message, Exception inner) : base(message, inner)
		{ }

		protected UserNotFoundException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{ }
	}
}