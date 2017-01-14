using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC
{
	/// <summary>
	/// The color theme of the widget. Default: Light
	/// </summary>
	public enum Theme
	{
		Light,
		Dark
	}

	/// <summary>
	/// The type of CAPTCHA to serve. Default: Image
	/// </summary>
	public enum Type
	{
		Image,
		Audio
	}

	/// <summary>
	/// The size of the widget. Default: Normal
	/// </summary>
	public enum Size
	{
		Normal,
		Compact
	}

	/// <summary>
	/// Error codes from google returned by webrequest
	/// </summary>
	public enum GErrorCodes
	{
		/// <summary>
		/// The secret parameter is missing.
		/// </summary>
		MissingInputSecret,

		/// <summary>
		/// The secret parameter is invalid or malformed.
		/// </summary>
		InvalidInputSecret,

		/// <summary>
		/// The response parameter is missing.
		/// </summary>
		MissingInputResponse,

		/// <summary>
		/// The response parameter is invalid or malformed.
		/// </summary>
		InvalidInputResponse
	}
}
