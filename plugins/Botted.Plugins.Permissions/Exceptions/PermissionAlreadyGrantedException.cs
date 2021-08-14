using System;
using System.Runtime.Serialization;

namespace Botted.Plugins.Permissions.Exceptions
{
	[Serializable]
	public class PermissionAlreadyGrantedException : PermissionException
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public PermissionAlreadyGrantedException()
		{ }

		public PermissionAlreadyGrantedException(string message) : base(message)
		{ }

		public PermissionAlreadyGrantedException(string message, Exception inner) : base(message, inner)
		{ }

		protected PermissionAlreadyGrantedException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{ }
	}
}