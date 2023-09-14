using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SoFunny.FunnySDK.IAP
{
    internal static class Logger
    {
        const string k_Tag = "[ FunnyIAP ]";

        internal const string k_GlobalVerboseLoggingDefine = "ENABLE_FUNNYSDK_DEBUG";

        [Conditional(k_GlobalVerboseLoggingDefine)]
        internal static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.unityLogger.Log(k_Tag, $"<color=#f3704b>{message}</color>");
#else
            Debug.unityLogger.Log(k_Tag, message);
#endif
        }

        internal static void LogWarning(object message) => Debug.unityLogger.LogWarning(k_Tag, message);

        [Conditional(k_GlobalVerboseLoggingDefine)]
        internal static void LogError(object message)
        {

#if UNITY_EDITOR
            Debug.unityLogger.Log(k_Tag, $"<color=red>{message}</color>");
#else
            Debug.unityLogger.Log(k_Tag, message);
#endif
        }

        [Conditional(k_GlobalVerboseLoggingDefine)]
        internal static void LogException(Exception exception) => Debug.unityLogger.Log(LogType.Exception, k_Tag, exception);

        [Conditional(k_GlobalVerboseLoggingDefine)]

        internal static void LogVerbose(object message) => Debug.unityLogger.Log(k_Tag, message);
    }
}

