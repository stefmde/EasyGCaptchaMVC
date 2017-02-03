#EasyGCaptchaMVC
*Easy Google ReCaptcha wrapper for MVC*

My try to make it easy to implement the google ReCaptcha service for a MVC Website.

---------------

##Steps to implement

 1. Get the api key pair from google: https://www.google.com/recaptcha
 2. Check the sample/demo project on [GitHub](https://github.com/stefmde/EasyGCaptchaMVC "GitHub - EasyGCaptchaMVC")
 3. Download and install the Package from [nuget](https://www.nuget.org/packages/EasyGCaptchaMVC/ "nuget - EasyGCaptchaMVC")
 4. Implementing the HTML-Extension `EasyGCaptchaGenerateCaptcha` in the view
 5. Implementing the `ActionFilterAttribute` above the Action
 6. Check the given `EasyGCaptchaResult` in the Action

---------------

##Quickstart
###Html-Extension
Calling the `Html`-Extension `EasyGCaptchaGenerateCaptcha`  and pass your publickey/websitekey to it is all you have to do in the view. You kann pass a few more settings like the `Theme` mode to it.

    @Html.EasyGCaptchaGenerateCaptcha(publicKey: "xyzxyzxyz...xyzxyzxyz", theme: Theme.Dark)

###ActionFilterAttribute and the result
Place the `EasyGCaptcha`-Attribute above your `Post`-Action and pass the privatekey to it.
If you have done this you have to add the `EasyGCaptchaResult easyGCaptchaResult`-Parameter to your Action. In this parameter you can check the results from Google.

    [HttpPost]
	[EasyGCaptcha(PrivateKey ="xyzxyzxyz...xyzxyzxyz")]
	public ActionResult Index(IndexViewModel model, EasyGCaptchaResult easyGCaptchaResult)
	{
		if (easyGCaptchaResult.Success)
		{
			// Do your work here
		}
		model.EasyGCaptchaResult = easyGCaptchaResult;
		return View(model);
	}

---------------

##Some notes

 - This extension automatically uses some development/testing keys from google if the app is in developement or no keys are provided
 - The Extension and the Attribute provides some additional parameters to customize it
 
##Supported parameters
###Extension
 - string `publicKey`
  - Default: Dev-Key from Google
  - Description: Contains the public or website key, provides by Google
 - string `divID`
  - Default: `EasyGCaptchaMVC_div`
  - Description: Contains the id for the div which contains the module. Used for styling and things like that
 - Theme `theme`
  - Default: `Theme.Light`
  - Description: Contains the theme setting, provided by Google
 - Size `size`
  - Default: `Size.Normal`
  - Description: Contains the size setting, provided by Google
 - Type `type`
  - Default: `Type.Image`
  - Description: Contains the validation type setting, provided by Google
 - int `tabindex`
  - Default: `-1`
  - Description: Contains the tabindex for the form, provided by Google
 - string `callBack`
  - Default: empty
  - Description: Provides the ability to call a js function if the module is fully loaded, provided by Google
 - bool `forceDebugMode`
  - Default: `false`
  - Description: Forces the debug mode. Should be false on prduction
 - bool `forceReleaseMode`
  - Default: `false`
  - Description: Forces release mode. For testing only
 - bool `usePassthruInDebugMode`
  - Default: `true`
  - Description: Uses special keys, provided by Google, to show the module but always passthru it. Only for testing.
 - bool `showErrorMessagesOnDebug`
  - Default: `true`
  - Description: Can be enabled to show error messages from the extension on the website if the site is in debug mode
 - bool `disableExceptions`
  - Default: `false`
  - Description: Prevents the module from throwing exceptions. But can hide errors if 'ShowErrorMessageOnDebug' is false




###Attribute

- string `PrivateKey`
 - Default: Dev-Key from Google
 - Description: Contains the private key, provides by Google
- bool `DisableExceptions`
 - Default: `false`
 - Description: Prevents the module from throwing exceptions. But can hide errors if 'ShowErrorMessageOnDebug' is false
- bool `ForceDebugMode`
 - Default: `false`
 - Description: Forces the debug mode. Should be false on prduction.
- bool `ForceReleaseMode`
 - Default: `false`
 - Description: Forces release mode. For testing only.
- bool `UsePassthruInDebugMode`
 - Default: `true`
 - Description: Uses special keys, provided by Google, to show the module but always passthru it. Only for testing.
- bool `PassRemoteIPToGoogle`
 - Default: `false`
 - Description: Passes the ip of the client ip to Google. Don't know for what
