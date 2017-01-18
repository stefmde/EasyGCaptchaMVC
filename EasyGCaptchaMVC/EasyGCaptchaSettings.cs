using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC
{
	public class EasyGCaptchaSettings
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
		/// Contains the theme setting, provided by Google. Default: Theme.Light
		/// </summary>
		public Theme Theme { get; set; } = Theme.Light;

		/// <summary>
		/// Contains the size setting, provided by Google. Default: Size.Normal
		/// </summary>
		public Size Size { get; set; } = Size.Normal;

		/// <summary>
		/// Contains the validation type setting, provided by Google. Default: Type.Image
		/// </summary>
		public Type Type { get; set; } = Type.Image;

		/// <summary>
		/// Contains the tabindex for the form, provided by Google. Default: -1/ Not used
		/// </summary>
		public int Tabindex { get; set; } = -1;

		/// <summary>
		/// Provides the ability to call a js function if the module is fully loaded, provided by Google. Default: empty/not set
		/// </summary>
		public string Callback { get; set; }

		/// <summary>
		/// Contains the id for the div which contains the module. Used for styling and things like that. Default: 'EasyGCaptchaMVC_div'
		/// </summary>
		public string DivID { get; set; } = "EasyGCaptchaMVC_div";

		/// <summary>
		/// Can be enabled to show error messages from the extension on the website if the site is in debug mode. Default: true
		/// </summary>
		public bool ShowErrorMessagesOnDebug { get; set; } = true;

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
	}
}
