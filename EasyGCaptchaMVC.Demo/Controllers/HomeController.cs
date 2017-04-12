using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyGCaptchaMVC.Configuration;
using EasyGCaptchaMVC.Demo.Models;
using EasyGCaptchaMVC.Model;
using EasyGCaptchaMVC.Worker;

namespace EasyGCaptchaMVC.Demo.Controllers
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