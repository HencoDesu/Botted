using System;
using System.Runtime.Serialization;

namespace Botted.Plugins.Permissions.Exceptions
{
	[Serializable]
	public class NoSuchPermissionException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public NoSuchPermissionException()
		{ }

		public NoSuchPermissionException(string message) : base(message)
		{ }

		public NoSuchPermissionException(string message, Exception inner) : base(message, inner)
		{ }

		protected NoSuchPermissionException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{ }
	}
}