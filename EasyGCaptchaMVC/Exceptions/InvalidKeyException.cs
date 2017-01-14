using System;

namespace EasyGCaptchaMVC.Exceptions
{
	public class InvalidKeyException : ApplicationException
	{
		public InvalidKeyException() { }
		public InvalidKeyException(string message) : base(message) { }
		public InvalidKeyException(string message, Exception inner) : base(message, inner) { }
	}
}
