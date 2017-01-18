using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGCaptchaMVC
{
	internal static class Helper
	{
		
		internal static EnvironmentSetting GetEnvironmentSetting(bool forceDebugMode, bool forceReleaseMode)
		{
			EnvironmentSetting environmentSetting = EnvironmentSetting.Release;

			if (forceDebugMode || forceReleaseMode)
			{
				if (forceDebugMode)
				{
					environmentSetting = EnvironmentSetting.Debug;
				}
			}
			else
			{
				if (Debugger.IsAttached)
				{
					environmentSetting = EnvironmentSetting.Debug;
				}
			}

			return environmentSetting;
		}

	}
}
