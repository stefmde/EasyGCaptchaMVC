using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using EasyGCaptchaMVC.Configuration;
using EasyGCaptchaMVC.Model;

namespace EasyGCaptchaMVC.Worker
{
	internal static class Helper
	{

		internal static EnvironmentSetting GetEnvironmentSetting(ForcedConfigurationMode forcedConfigurationMode)
		{
			EnvironmentSetting environmentSetting = EnvironmentSetting.Release;

			if (forcedConfigurationMode == ForcedConfigurationMode.Debug)
			{
				environmentSetting = EnvironmentSetting.Debug;
			}
			else if (forcedConfigurationMode == ForcedConfigurationMode.Release)
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
