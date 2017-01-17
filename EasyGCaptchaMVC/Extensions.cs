using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyGCaptchaMVC.Exceptions;

namespace EasyGCaptchaMVC
{
	public static class Extensions
	{
		public static MvcHtmlString EasyGCaptchaGenerateCaptcha(this HtmlHelper helper, EasyGCaptchaSettings settings)
		{
			return helper.EasyGCaptchaGenerateCaptcha(settings.PublicKey, settings.DivID, settings.Theme, settings.Size, settings.Type,
				settings.Tabindex, settings.Callback);
		}

		public static MvcHtmlString EasyGCaptchaGenerateCaptcha(this HtmlHelper helper, string publicKey = null,
													string divID = "EasyGCaptchaMVC_div", Theme theme = Theme.Light,
													Size size = Size.Normal, Type type = Type.Image, int tabindex = -1,
													string callBack = null)
		{
			string pubKey = string.Empty;
			string tab = string.Empty;
			string call = string.Empty;

			StringBuilder templateBuilder = new StringBuilder();
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<!-- EasyGCaptchaMVC Section start -->");
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<div id=\"" + divID + "\"></div>");
			templateBuilder.Append(Environment.NewLine);
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
			templateBuilder.Append("<!-- EasyGCaptchaMVC Section end -->");
			templateBuilder.Append(Environment.NewLine);


			// Try get public key from parameter or appsettings
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"]))
			{
				pubKey = ConfigurationManager.AppSettings["EasyGCaptchaMVC.PublicKey"];
			}
			else if (!string.IsNullOrEmpty(publicKey))
			{
				pubKey = publicKey;
			}
			else
			{
				throw new InvalidKeyException("EasyGCaptchaMVC PublicKey missing from appSettings or parameter");
			}

			if (tabindex > -1)
			{
				tab = ", 'tabindex' : '" + tabindex + "'";
			}

			if (!string.IsNullOrEmpty(callBack))
			{
				call = ", 'callback' : '" + callBack + "'";
			}

			string html = string.Format(templateBuilder.ToString(), pubKey, theme.ToString().ToLower(),
										size.ToString().ToLower(), type.ToString().ToLower(), tab, call);

			return MvcHtmlString.Create(html);
		}
	}
}
