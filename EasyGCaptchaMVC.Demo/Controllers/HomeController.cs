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
		[HttpGet]
		public ActionResult Index()
		{
			return View(new IndexViewModel());
		}

		[HttpPost]
		[EasyGCaptcha(PrivateKey = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
		public ActionResult Index(IndexViewModel model, EasyGCaptchaResult EasyGCaptchaResult)
		{
			model.EasyGCaptchaResult = EasyGCaptchaResult;
			return View(model);
		}
	}
}