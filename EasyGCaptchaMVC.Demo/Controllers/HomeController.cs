using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyGCaptchaMVC.Demo.Models;

namespace EasyGCaptchaMVC.Demo.Controllers
{
	public class HomeController : Controller
	{
		public static EasyGCaptchaSettings captchaSettings = new EasyGCaptchaSettings();

		[HttpGet]
		public ActionResult Index()
		{
			captchaSettings.Theme = Theme.Dark;

			IndexViewModel model = new IndexViewModel();
			model.EasyGCaptchaSettings = captchaSettings;

			return View(new IndexViewModel());
		}

		[HttpPost]
		[EasyGCaptcha(PrivateKey = "xyzxyzxyz...xyzxyzxyz")]
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