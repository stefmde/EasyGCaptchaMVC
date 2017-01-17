using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyGCaptchaMVC
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
		public List<GErrorCodes> GErrorCodes { get; internal set; }
	}
}
