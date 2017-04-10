using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC.Exceptions
{
	class InvalidEnvironmentSetting : ApplicationException
	{
		public InvalidEnvironmentSetting() { }
		public InvalidEnvironmentSetting(string message) : base(message) { }
		public InvalidEnvironmentSetting(string message, Exception inner) : base(message, inner) { }
	}
}
