using EasyGCaptchaMVCCore.Model;

namespace EasyGCaptchaMVCCore.Demo.Models
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