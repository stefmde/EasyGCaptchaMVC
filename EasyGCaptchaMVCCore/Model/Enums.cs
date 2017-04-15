using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EasyGCaptchaMVCCore.Model
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
	/// The type of CAPTCHA to serve. Seams unsupported by google. Default: Image
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
		Compact,
		Invisible
	}

	public enum ForcedConfigurationMode
	{
		None,
		Debug,
		Release
	}

	/// <summary>
	/// Error codes from google returned by webrequest
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GErrorCodes
	{
		/// <summary>
		/// The secret parameter is missing.
		/// </summary>
		[EnumMember(Value = "missing-input-secret")]
		MissingInputSecret,

		/// <summary>
		/// The secret parameter is invalid or malformed.
		/// </summary>
		[EnumMember(Value = "invalid-input-secret")]
		InvalidInputSecret,

		/// <summary>
		/// The response parameter is missing.
		/// </summary>
		[EnumMember(Value = "missing-input-response")]
		MissingInputResponse,

		/// <summary>
		/// The response parameter is invalid or malformed.
		/// </summary>
		[EnumMember(Value = "invalid-input-response")]
		InvalidInputResponse,

		/// <summary>
		/// The request is invalid or malformed.
		/// </summary>
		[EnumMember(Value = "bad-request")]
		BadRequest
	}

	/// <summary>
	/// Internaly used to get the environment type
	/// </summary>
	public  enum EnvironmentSetting
	{
		Unknown,
		Debug,
		Release
	}
}
