using System;
using System.IO;
using System.Net;
using System.Text;
using EasyGCaptchaMVCCore.Configuration;
using EasyGCaptchaMVCCore.Exceptions;
using EasyGCaptchaMVCCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace EasyGCaptchaMVCCore.Worker
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
				userIP = filterContext.HttpContext.Request.Host.Host;
			}

			string postData = string.Format("&secret={0}&response={1}&remoteip={2}",
											GCaptchaSettingsProvider.Instance.PrivateKey,
											filterContext.HttpContext.Request.Form["g-recaptcha-response"],
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
				//request.ContentLength = postDataAsBytes.Length;

				dataStream = request.GetRequestStreamAsync().Result;
				dataStream.Write(postDataAsBytes, 0, postDataAsBytes.Length);
				dataStream.Dispose();
			}
			catch (Exception)
			{
				if (GCaptchaSettingsProvider.Instance.DisableExceptions)
				{
					((Controller)filterContext.Controller).ModelState.AddModelError("EasyGCaptchaMVC", "Error on webrequest");
					filterContext.ActionArguments["easyGCaptchaResult"] = new EasyGCaptchaResult();
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
				WebResponse response = request.GetResponseAsync().Result;

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
					filterContext.ActionArguments["easyGCaptchaResult"] = new EasyGCaptchaResult();
					return;
				}
				else
				{
					throw;
				}
			}

			EasyGCaptchaResult result = JsonConvert.DeserializeObject<EasyGCaptchaResult>(responseFromServer);

			if (filterContext.ActionArguments.ContainsKey("easyGCaptchaResult"))
			{
				filterContext.ActionArguments["easyGCaptchaResult"] = result;
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
