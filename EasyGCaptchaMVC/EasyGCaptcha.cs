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
using EasyGCaptchaMVC.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyGCaptchaMVC
{
	public class EasyGCaptcha : ActionFilterAttribute
	{
		/// <summary>
		/// Contains the public or website key, provides by Google. Default: empty/not set
		/// </summary>
		public string PublicKey { get; set; }

		/// <summary>
		/// Contains the private key, provides by Google. Default: empty/not set
		/// </summary>
		public string PrivateKey { get; set; }

		/// <summary>
		/// Prevents the module from throwing exceptions. But can hide errors if 'ShowErrorMessageOnDebug' is false. Default: false
		/// </summary>
		public bool DisableExceptions { get; set; } = false;

		/// <summary>
		/// Forces the debug mode. Should be false on prduction. Default: false
		/// </summary>
		public bool ForceDebugMode { get; set; } = false;

		/// <summary>
		/// Forces release mode. For testing only. Default: false
		/// </summary>
		public bool ForceReleaseMode { get; set; } = false;

		/// <summary>
		/// Uses special keys, provided by Google, to show the module but always passthru it. Only for testing. Default: true
		/// </summary>
		public bool UsePassthruInDebugMode { get; set; } = true;

		/// <summary>
		/// Passes the ip of the client ip to Google. Don't know for what. Default: false
		/// </summary>
		public bool PassRemoteIPToGoogle { get; set; } = false;

		/// <summary>
		/// The EasyGCaptchaSettings-Object
		/// </summary>
		public EasyGCaptchaSettings EasyGCaptchaSettings { get; set; } = null;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string userIP = string.Empty;
			string privateKey = string.Empty;

			if (PassRemoteIPToGoogle)
			{
				userIP = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
			}

			// Try get private key from config or parameter
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EasyGCaptchaMVC.PrivateKey"]))
			{
				privateKey = ConfigurationManager.AppSettings["EasyGCaptchaMVC.PrivateKey"];
			}
			else if (!string.IsNullOrEmpty(PrivateKey))
			{
				privateKey = PrivateKey;
			}
			else
			{
				throw new InvalidKeyException("EasyGCaptchaMVC.PrivateKey missing from appSettings or parameter");
			}

			string postData = string.Format("&secret={0}&response={1}&remoteip={2}",
										 privateKey,
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

			using (dataStream = response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(dataStream))
				{
					string responseFromServer = reader.ReadToEnd();
					EasyGCaptchaResult result = JsonConvert.DeserializeObject<EasyGCaptchaResult>(responseFromServer);

					if (filterContext.ActionParameters.ContainsKey("easyGCaptchaResult"))
					{
						filterContext.ActionParameters["EasyGCaptchaResult"] = result;
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
	}
}
