using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyGCaptchaMVC.Demo.Models
{
	public class IndexViewModel
	{
		public ContactViewModel ContactViewModel { get; set; }
		public EasyGCaptchaResult EasyGCaptchaResult { get; set; }


		public IndexViewModel()
		{
			ContactViewModel = new ContactViewModel();
			EasyGCaptchaResult = new EasyGCaptchaResult();
		}
	}
}