using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyGCaptchaMVCCore.Model
{
	public class EasyGCaptchaResult
	{
		/// <summary>
		/// The master success flag
		/// </summary>
		[JsonProperty(PropertyName = "success")]
		public bool Success { get; internal set; }

		/// <summary>
		/// Date of the challenge load
		/// </summary>
		[JsonProperty(PropertyName = "challenge_ts")]
		public DateTime Date { get; internal set; }

		/// <summary>
		/// The hostname of the site where the reCAPTCHA was solved
		/// </summary>
		[JsonProperty(PropertyName = "hostname")]
		public string Hostname { get; internal set; }

		/// <summary>
		/// Optional error codes. Not tested.
		/// </summary>
		[JsonProperty(PropertyName = "error-codes")]
		//[JsonConverter(typeof(StringEnumConverter))]
		public List<GErrorCodes> GErrorCodes { get; internal set; }
	}
}
