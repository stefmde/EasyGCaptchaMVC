using System;

namespace EasyGCaptchaMVCCore.Exceptions
{
	public class MissingActionParameterException : Exception
	{
		public MissingActionParameterException() { }
		public MissingActionParameterException(string message) : base(message) { }
		public MissingActionParameterException(string message, Exception inner) : base(message, inner) { }
	}
}
