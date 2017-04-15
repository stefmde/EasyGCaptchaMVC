namespace EasyGCaptchaMVCCore.Configuration
{
	public class GCaptchaSettingsProvider
	{
		private static Settings _instance;

		private GCaptchaSettingsProvider() { }

		public static Settings Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Settings();
				}
				return _instance;
			}
		}
	}
}
