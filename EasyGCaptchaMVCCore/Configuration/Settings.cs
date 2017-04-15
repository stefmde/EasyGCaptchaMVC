using System;
using EasyGCaptchaMVCCore.Model;
using EasyGCaptchaMVCCore.Worker;
using Type = EasyGCaptchaMVCCore.Model.Type;

namespace EasyGCaptchaMVCCore.Configuration
{
	public class Settings
	{
		private string _publicKey = String.Empty;
		/// <summary>
		/// Contains the public key, provides by Google. Default: testing key
		/// </summary>
		public string PublicKey
		{
			get
			{
				if (string.IsNullOrEmpty(_publicKey))
				{
					_publicKey = Helper.GetGPublicKey();
				}

				return _publicKey;
			}
			set => _publicKey = value;
		}

		private string _privateKey = String.Empty;
		/// <summary>
		/// Contains the private key, provides by Google. Default: testing key
		/// </summary>
		public string PrivateKey {
			get
			{
				if (string.IsNullOrEmpty(_privateKey))
				{
					_privateKey = Helper.GetGPrivateKey();
				}

				return _privateKey;
			}
			set => _privateKey = value;
		}

		private string _divId = "EasyGCaptchaMVC";

		/// <summary>
		/// Contains the id for the div which contains the module. Used for styling and things like that. Can't be set to null or empty. Default: "EasyGCaptchaMVC"
		/// </summary>
		public string DivId
		{
			get { return _divId; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_divId = value;
				}
			}
		}

		/// <summary>
		/// Contains the theme setting, provided by Google. Default: Light
		/// </summary>
		public Theme Theme { get; set; } = Theme.Light;

		/// <summary>
		/// Contains the size setting, provided by Google
		/// </summary>
		public Size Size { get; set; } = Size.Normal;

		/// <summary>
		/// Contains the validation type setting, provided by Google. Default: Image
		/// </summary>
		public Type Type { get; set; } = Type.Image;

		/// <summary>
		/// Contains the tabindex for the form, provided by Google. Default: 0
		/// </summary>
		public int Tabindex { get; set; } = 0;

		/// <summary>
		/// Provides the ability to call a js function if the module is fully loaded, provided by Google. Default: None
		/// </summary>
		public string CallBack { get; set; } = String.Empty;

		///// <summary>
		///// Can be enabled to show error messages from the extension on the website if the site is in debug mode. Default: true
		///// </summary>
		//public bool ShowErrorMessagesOnDebug { get; set; } = true;

		/// <summary>
		/// Prevents the module from throwing exceptions. Default: false
		/// </summary>
		public bool DisableExceptions { get; set; } = false;

		/// <summary>
		/// Forces the debug/release mode. Should be None on prduction. Default: None
		/// </summary>
		public ForcedConfigurationMode ForcedConfigurationMode { get; set; } = ForcedConfigurationMode.None;

		/// <summary>
		/// Uses special keys, provided by Google, to show the module but always passthru it. Only for testing. Default: true
		/// </summary>
		public bool UsePassthruInDebugMode { get; set; } = true;

		/// <summary>
		/// Passes the ip of the client ip to Google. Don't know for what. Default: false
		/// </summary>
		public bool PassRemoteIpToGoogle { get; set; } = false;

		/// <summary>
		/// Shows the configuration mode in which the extension is running
		/// </summary>
		public EnvironmentSetting EnvironmentSetting => Helper.GetEnvironmentSetting(ForcedConfigurationMode);
	}
}
