using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyGCaptchaMVC.Configuration;
using EasyGCaptchaMVC.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyGCaptchaMVC
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
			string errorMessage = string.Empty;
			bool errorOccoured = false;

			if (GCaptchaSettingsProvider.Instance.PassRemoteIPToGoogle)
			{
				userIP = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
			}

			string postData = string.Format("&secret={0}&response={1}&remoteip={2}",
											GCaptchaSettingsProvider.Instance.PrivateKey,
											filterContext.RequestContext.HttpContext.Request.Form["g-recaptcha-response"],
											userIP);

			byte[] postDataAsBytes = Encoding.UTF8.GetBytes(postData);

			// Create web request
			WebRequest request = WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = postDataAsBytes.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(postDataAsBytes, 0, postDataAsBytes.Length);
			dataStream.Close();

			// Get the response
			WebResponse response = request.GetResponse();
			string responseFromServer;
			using (dataStream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(dataStream))
				{
					responseFromServer = reader.ReadToEnd();
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
