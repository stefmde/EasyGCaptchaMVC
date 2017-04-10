using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyGCaptchaMVC.Exceptions;
using System.Diagnostics;

namespace EasyGCaptchaMVC
{
	public static class Extensions
	{

		/// <summary>
		/// Renders the Google ReCaptcha with the single EasyGCaptcha settings
		/// </summary>
		/// <param name="helper">MVC Extension</param>
		/// <param name="publicKey">Contains the public or website key, provides by Google</param>
		/// <param name="divID">Contains the id for the div which contains the module. Used for styling and things like that</param>
		/// <param name="theme">Contains the theme setting, provided by Google</param>
		/// <param name="size">Contains the size setting, provided by Google</param>
		/// <param name="type">Contains the validation type setting, provided by Google</param>
		/// <param name="tabindex">Contains the tabindex for the form, provided by Google</param>
		/// <param name="callBack">Provides the ability to call a js function if the module is fully loaded, provided by Google</param>
		/// <param name="forceDebugMode">Forces the debug mode. Should be false on prduction</param>
		/// <param name="forceReleaseMode">Forces release mode. For testing only</param>
		/// <param name="usePassthruInDebugMode">Uses special keys, provided by Google, to show the module but always passthru it. Only for testing.</param>
		/// <param name="showErrorMessagesOnDebug">Can be enabled to show error messages from the extension on the website if the site is in debug mode</param>
		/// <param name="disableExceptions">Prevents the module from throwing exceptions. But can hide errors if 'ShowErrorMessageOnDebug' is false</param>
		/// <returns>Outputs raw html to the cshtml-Page</returns>
		public static MvcHtmlString EasyGCaptchaGenerateCaptcha(this HtmlHelper helper, string publicKey = null,
													string divID = "EasyGCaptchaMVC_div", Theme theme = Theme.Light,
													Size size = Size.Normal, Type type = Type.Image, int tabindex = -1,
													string callBack = null, bool forceDebugMode = false,
													bool forceReleaseMode = false, bool usePassthruInDebugMode = true,
													bool showErrorMessagesOnDebug = true, bool disableExceptions = false)
		{

			string pubKey = string.Empty;
			string tab = string.Empty;
			string call = string.Empty;
			EnvironmentSetting environmentSetting = EnvironmentSetting.Unknown;
			string errorMessage = string.Empty;
			bool errorOccoured = false;

			// Get Environment setting
			if (forceDebugMode && forceReleaseMode)
			{
				string tempMessage = "EasyGCaptchaMVC forceDebugMode and forceReleaseMode can't be both true at the same time";
				if (disableExceptions)
				{
					errorMessage = tempMessage;
					errorOccoured = true;
				}
				else
				{
					throw new InvalidEnvironmentSetting(tempMessage);
				}
			}
			else
			{
				environmentSetting = Helper.GetEnvironmentSetting(forceDebugMode, forceReleaseMode);
			}

			// Try get public key from parameter or appsettings
			if (environmentSetting == EnvironmentSetting.Debug && usePassthruInDebugMode)
			{
				pubKey = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
			}
			else if (ConfigurationManager.AppSettings.AllKeys.Contains("EasyGCaptchaMVC.PublicKey") && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"]))
			{
				pubKey = ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"];
			}
			else if (!string.IsNullOrEmpty(publicKey))
			{
				pubKey = publicKey;
			}
			else
			{
				string tempMessage = "EasyGCaptchaMVC PublicKey missing from appSettings or parameter";
				if (disableExceptions)
				{
					errorMessage = tempMessage;
					errorOccoured = true;
				}
				else
				{
					throw new InvalidKeyException(tempMessage);
				}
			}

			if (tabindex > -1)
			{
				tab = ", 'tabindex' : '" + tabindex + "'";
			}

			if (!string.IsNullOrEmpty(callBack))
			{
				call = ", 'callback' : '" + callBack + "'";
			}

			StringBuilder templateBuilder = new StringBuilder();
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<!-- EasyGCaptchaMVC Section start -->");
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<div id=\"" + divID + "\"></div>");
			templateBuilder.Append(Environment.NewLine);

			if (errorOccoured)
			{
				templateBuilder.Append("<span style=\"color: red;\">");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append(errorMessage);
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("</span>");
				templateBuilder.Append(Environment.NewLine);
			}
			else
			{
				templateBuilder.Append("<script type=\"text/javascript\">");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("function recaptchaOnloadCallback() {{");
				/*
				 0 = sitekey
				 1 = theme
				 2 = size
				 3 = type
				 4 = tabindex
				 5 = callback
				 */
				templateBuilder.Append("grecaptcha.render( '" + divID + "', {{'sitekey' : '{0}', 'theme' : '{1}', 'size' : '{2}', 'type' : '{3}' {4} {5}}});");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("}}");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("</script>");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("<script src='https://www.google.com/recaptcha/api.js?onload=recaptchaOnloadCallback&render=explicit'></script>");
				templateBuilder.Append(Environment.NewLine);
			}

			templateBuilder.Append("<!-- EasyGCaptchaMVC Section end -->");
			templateBuilder.Append(Environment.NewLine);

			string html = string.Format(templateBuilder.ToString(), pubKey, theme.ToString().ToLower(),
										size.ToString().ToLower(), type.ToString().ToLower(), tab, call);

			return MvcHtmlString.Create(html);
		}
	}
}
