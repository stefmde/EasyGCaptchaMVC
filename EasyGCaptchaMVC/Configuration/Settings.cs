using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyGCaptchaMVC.Exceptions;

namespace EasyGCaptchaMVC.Configuration
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

		/// <summary>
		/// Contains the id for the div which contains the module. Used for styling and things like that
		/// </summary>
		public string DivId { get; set; } = "EasyGCaptchaMVC_div";

		/// <summary>
		/// Contains the theme setting, provided by Google
		/// </summary>
		public Theme Theme { get; set; } = Theme.Light;

		/// <summary>
		/// Contains the size setting, provided by Google
		/// </summary>
		public Size Size { get; set; } = Size.Normal;

		/// <summary>
		/// Contains the validation type setting, provided by Google
		/// </summary>
		public Type Type { get; set; } = Type.Image;

		/// <summary>
		/// Contains the tabindex for the form, provided by Google
		/// </summary>
		public int Tabindex { get; set; } = -1;

		/// <summary>
		/// Provides the ability to call a js function if the module is fully loaded, provided by Google
		/// </summary>
		public string CallBack { get; set; } = null;

		/// <summary>
		/// Can be enabled to show error messages from the extension on the website if the site is in debug mode
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


		public EnvironmentSetting EnvironmentSetting
		{
			get
			{
				if (ForceDebugMode && ForceReleaseMode)
				{
					return EnvironmentSetting.Unknown;
				}
				else
				{
					return Helper.GetEnvironmentSetting(ForceDebugMode, ForceReleaseMode);
				}
			}
		}
	}
}
