using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyGCaptchaMVC.Configuration;
using EasyGCaptchaMVC.Exceptions;

namespace EasyGCaptchaMVC
{
	internal static class Helper
	{

		internal static EnvironmentSetting GetEnvironmentSetting(bool forceDebugMode, bool forceReleaseMode)
		{
			EnvironmentSetting environmentSetting = EnvironmentSetting.Release;

			if (forceDebugMode)
			{
				environmentSetting = EnvironmentSetting.Debug;
			}
			else if (forceReleaseMode)
			{
				environmentSetting = EnvironmentSetting.Release;
			}
			else if (Debugger.IsAttached)
			{
				environmentSetting = EnvironmentSetting.Debug;
			}

			return environmentSetting;
		}

		internal static string GetGPublicKey()
		{
			string key = String.Empty;

			if (GCaptchaSettingsProvider.Instance.EnvironmentSetting == EnvironmentSetting.Debug && GCaptchaSettingsProvider.Instance.UsePassthruInDebugMode)
			{
				key = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
			}
			else if (ConfigurationManager.AppSettings.AllKeys.Contains("EasyGCaptchaMVC.PublicKey") && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"]))
			{
				key = ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"];
			}

			return key;
		}
		internal static string GetGPrivateKey()
		{
			string key = String.Empty;

			if (GCaptchaSettingsProvider.Instance.EnvironmentSetting == EnvironmentSetting.Debug && GCaptchaSettingsProvider.Instance.UsePassthruInDebugMode)
			{
				key = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";
			}
			else if (ConfigurationManager.AppSettings.AllKeys.Contains("EasyGCaptchaMVC.PrivateKey") && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EasyGCaptchaMVC.PrivateKey"]))
			{
				key = ConfigurationManager.AppSettings["EasyGCaptchaMVC.PrivateKey"];
			}

			return key;
		}
	}
}
