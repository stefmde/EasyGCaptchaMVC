using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyGCaptchaMVC.Exceptions;
using System.Diagnostics;
using EasyGCaptchaMVC.Configuration;

namespace EasyGCaptchaMVC
{
	public static class Extensions
	{

		/// <summary>
		/// Renders the Google ReCaptcha with the single EasyGCaptcha settings
		/// </summary>
		/// <param name="helper">MVC Extension</param>
		/// <returns>Outputs raw html to the cshtml-Page</returns>
		public static MvcHtmlString EasyGCaptchaGenerateCaptcha(this HtmlHelper helper)
		{
			string tab = string.Empty;
			string call = string.Empty;
			string errorMessage = string.Empty;
			bool errorOccoured = false;

			if (GCaptchaSettingsProvider.Instance.Tabindex > -1)
			{
				tab = ", 'tabindex' : '" + GCaptchaSettingsProvider.Instance.Tabindex + "'";
			}

			if (!string.IsNullOrEmpty(GCaptchaSettingsProvider.Instance.CallBack))
			{
				call = ", 'callback' : '" + GCaptchaSettingsProvider.Instance.CallBack + "'";
			}

			StringBuilder templateBuilder = new StringBuilder();
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<!-- EasyGCaptchaMVC Section start -->");
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<div id=\"" + GCaptchaSettingsProvider.Instance.DivId + "\"></div>");
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
				templateBuilder.Append("grecaptcha.render( '" + GCaptchaSettingsProvider.Instance.DivId + "', {{'sitekey' : '{0}', 'theme' : '{1}', 'size' : '{2}', 'type' : '{3}' {4} {5}}});");
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

			string html = string.Format(templateBuilder.ToString(), 
										GCaptchaSettingsProvider.Instance.PublicKey, 
										GCaptchaSettingsProvider.Instance.Theme.ToString().ToLower(),
										GCaptchaSettingsProvider.Instance.Size.ToString().ToLower(), 
										GCaptchaSettingsProvider.Instance.Type.ToString().ToLower(), 
										tab, call);

			return MvcHtmlString.Create(html);
		}
	}
}
