using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC.Exceptions
{
	public class MissingActionParameterException : ApplicationException
	{
		public MissingActionParameterException() { }
		public MissingActionParameterException(string message) : base(message) { }
		public MissingActionParameterException(string message, Exception inner) : base(message, inner) { }
	}
}
