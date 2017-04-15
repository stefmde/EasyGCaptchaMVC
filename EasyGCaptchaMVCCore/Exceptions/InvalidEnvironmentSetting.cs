using System;

namespace EasyGCaptchaMVCCore.Exceptions
{
	class InvalidEnvironmentSetting : Exception
	{
		public InvalidEnvironmentSetting() { }
		public InvalidEnvironmentSetting(string message) : base(message) { }
		public InvalidEnvironmentSetting(string message, Exception inner) : base(message, inner) { }
	}
}
