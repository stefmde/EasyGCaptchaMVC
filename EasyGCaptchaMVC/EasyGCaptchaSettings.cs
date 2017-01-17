using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC
{
	public class EasyGCaptchaSettings
	{
		public string PublicKey { get; set; }
		public Theme Theme { get; set; } = Theme.Light;
		public Size Size { get; set; } = Size.Normal;
		public Type Type { get; set; } = Type.Image;
		public int Tabindex { get; set; } = -1;
		public string Callback { get; set; }
		public string DivID { get; set; }

	}
}
