using System;
using System.Runtime.Serialization;

namespace Botted.Plugins.Permissions.Exceptions
{
	[Serializable]
	public class PermissionException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public PermissionException()
		{ }

		public PermissionException(string message) : base(message)
		{ }

		public PermissionException(string message, Exception inner) : base(message, inner)
		{ }

		protected PermissionException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{ }
	}
}