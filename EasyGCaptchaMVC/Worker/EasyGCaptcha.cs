using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using EasyGCaptchaMVC.Configuration;
using EasyGCaptchaMVC.Exceptions;
using EasyGCaptchaMVC.Model;
using Newtonsoft.Json;

namespace EasyGCaptchaMVC.Worker
{
	public class EasyGCaptcha : ActionFilterAttribute
	{
		///// <summary>
		///// The EasyGCaptchaSettings-Object
		///// </summary>
		//public EasyGCaptchaSettings EasyGCaptchaSettings { get; set; } = null;
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string userIP = string.Empty;

			if (GCaptchaSettingsProvider.Instance.PassRemoteIpToGoogle)
			{
				userIP = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
			}

			string postData = string.Format("&secret={0}&response={1}&remoteip={2}",
											GCaptchaSettingsProvider.Instance.PrivateKey,
											filterContext.RequestContext.HttpContext.Request.Form["g-recaptcha-response"],
											userIP);

			byte[] postDataAsBytes = Encoding.UTF8.GetBytes(postData);

			// Create web request
			WebRequest request;
			Stream dataStream;
			try
			{
				request = WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = postDataAsBytes.Length;

				dataStream = request.GetRequestStream();
				dataStream.Write(postDataAsBytes, 0, postDataAsBytes.Length);
				dataStream.Close();
			}
			catch (Exception)
			{
				if (GCaptchaSettingsProvider.Instance.DisableExceptions)
				{
					((Controller)filterContext.Controller).ModelState.AddModelError("EasyGCaptchaMVC", "Error on webrequest");
					filterContext.ActionParameters["easyGCaptchaResult"] = new EasyGCaptchaResult();
					return;
				}
				else
				{
					throw;
				}
			}

			// Get the response
			string responseFromServer;
			try
			{
				WebResponse response = request.GetResponse();

				using (dataStream = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(dataStream))
					{
						responseFromServer = reader.ReadToEnd();
					}
				}
			}
			catch (Exception)
			{
				if (GCaptchaSettingsProvider.Instance.DisableExceptions)
				{
					((Controller)filterContext.Controller).ModelState.AddModelError("EasyGCaptchaMVC", "Error on parsing webrequest");
					filterContext.ActionParameters["easyGCaptchaResult"] = new EasyGCaptchaResult();
					return;
				}
				else
				{
					throw;
				}
			}

			EasyGCaptchaResult result = JsonConvert.DeserializeObject<EasyGCaptchaResult>(responseFromServer);

			if (filterContext.ActionParameters.ContainsKey("easyGCaptchaResult"))
			{
				filterContext.ActionParameters["easyGCaptchaResult"] = result;
			}
			else
			{
				throw new MissingActionParameterException("Action have to contan a parameter of type EasyGCaptchaResult with name easyGCaptchaResult");
			}

			if (!result.Success)
			{
				((Controller)filterContext.Controller).ModelState.AddModelError("EasyGCaptchaMVC", "Captcha incorrect");
			}
		}
	}
}
