using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyGCaptchaMVCCore.Configuration;
using EasyGCaptchaMVCCore.Demo.Models;
using EasyGCaptchaMVCCore.Model;
using EasyGCaptchaMVCCore.Worker;
using Microsoft.AspNetCore.Mvc;

namespace EasyGCaptchaMVCCore.Demo.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			GCaptchaSettingsProvider.Instance.Theme = Theme.Light;
			GCaptchaSettingsProvider.Instance.Size = Size.Normal;

			return View(new IndexViewModel());
		}

		[HttpPost]
		[EasyGCaptcha]
		public ActionResult Index(IndexViewModel model, EasyGCaptchaResult easyGCaptchaResult)
		{
			if (easyGCaptchaResult.Success)
			{
				// Do your work here
			}
			model.EasyGCaptchaResult = easyGCaptchaResult;
			return View(model);
		}
	}
}
