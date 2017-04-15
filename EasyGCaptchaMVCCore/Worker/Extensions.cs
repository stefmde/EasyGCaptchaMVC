using System;
using System.Text;
using EasyGCaptchaMVCCore.Configuration;
using EasyGCaptchaMVCCore.Model;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EasyGCaptchaMVCCore.Worker
{
	public static class Extensions
	{

		/// <summary>
		/// Renders the Google ReCaptcha with the single EasyGCaptcha settings
		/// </summary>
		/// <param name="helper">MVC Extension</param>
		/// <returns>Outputs raw html to the cshtml-Page</returns>
		public static IHtmlContent EasyGCaptchaGenerateCaptcha(this IHtmlHelper helper)
		{
			string tab = string.Empty;
			string call = string.Empty;

			if (GCaptchaSettingsProvider.Instance.Tabindex > 0)
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

			if (GCaptchaSettingsProvider.Instance.Size == Size.Invisible)
			{
				// Render Invisible with auto exec
				// ##################################################
				templateBuilder.Append("<div id=\"" + GCaptchaSettingsProvider.Instance.DivId + "\"");
				templateBuilder.Append("class=\"g-recaptcha\"");
				templateBuilder.Append("data-sitekey=\""+ GCaptchaSettingsProvider.Instance.PublicKey +"\"");
				templateBuilder.Append("data-theme=\""+ GCaptchaSettingsProvider.Instance.Theme.ToString().ToLower() +"\"");
				templateBuilder.Append("data-size=\"invisible\">");
				templateBuilder.Append("</div>");
				/*
					0 = sitekey
					1 = theme
					2 = size
					3 = type
					4 = tabindex
					5 = callback
					*/
				templateBuilder.Append("<script type=\"text/javascript\">");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("function recaptchaOnloadCallback() {{");
				templateBuilder.Append(Environment.NewLine);
				//templateBuilder.Append("grecaptcha.render( '" + GCaptchaSettingsProvider.Instance.DivId + "', {{'sitekey' : '{0}', 'theme' : '{1}', 'size' : '{2}', 'type' : '{3}' {4} {5}}});");
				templateBuilder.Append("grecaptcha.render( '" + GCaptchaSettingsProvider.Instance.DivId + "', {{'sitekey' : '{0}', 'theme' : '{1}', 'size' : 'invisible', 'type' : '{3}' {4} {5}}});");
				//templateBuilder.Append("grecaptcha.render( '" + GCaptchaSettingsProvider.Instance.DivId + "', {{'sitekey' : '{0}', 'theme' : '{1}', 'size' : 'invisible','type' : '{3}' {5}}});");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("grecaptcha.execute();");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("}}");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("</script>");
				templateBuilder.Append(Environment.NewLine);
			}
			else
			{
				// Render default visible
				// ##################################################
				templateBuilder.Append("<div id=\"" + GCaptchaSettingsProvider.Instance.DivId + "\"></div>");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("<script type=\"text/javascript\">");
				templateBuilder.Append(Environment.NewLine);
				templateBuilder.Append("function recaptchaOnloadCallback() {{");
				templateBuilder.Append(Environment.NewLine);
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
			}

			templateBuilder.Append("<script src='https://www.google.com/recaptcha/api.js?onload=recaptchaOnloadCallback&render=explicit'></script>");
			templateBuilder.Append(Environment.NewLine);
			templateBuilder.Append("<!-- EasyGCaptchaMVC Section end -->");
			templateBuilder.Append(Environment.NewLine);

			string html = string.Format(templateBuilder.ToString(), 
										GCaptchaSettingsProvider.Instance.PublicKey, 
										GCaptchaSettingsProvider.Instance.Theme.ToString().ToLower(),
										GCaptchaSettingsProvider.Instance.Size.ToString().ToLower(), 
										GCaptchaSettingsProvider.Instance.Type.ToString().ToLower(), 
										tab, call);

			return new HtmlString(html);
		}
	}
}
