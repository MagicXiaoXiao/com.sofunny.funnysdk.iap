using System;
using System.Collections.Generic;

namespace SoFunny.FunnySDK.IAP
{
    internal class DefaultBridge : IBridgeMethods
    {
        internal DefaultBridge()
        {
        }

        public void CallNavtive(string method)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
        }

        public void CallNavtive(string method, Dictionary<string, object> parameters)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
        }

        public T CallNavtive<T>(string method)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
            return default;
        }

        public T CallNavtive<T>(string method, Dictionary<string, object> parameters)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
            return default;
        }

        public void CallNavtive<T>(string method, BridgeCompletedHandler<T> callback)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
            callback?.Invoke(default, new FunnyIAPError(-99, "未知错误"));
        }

        public void CallNavtive<T>(string method, Dictionary<string, object> parameters, BridgeCompletedHandler<T> callback)
        {
            Logger.LogWarning("当前环境暂不支持该功能，请使用移动端进行。");
            callback?.Invoke(default, new FunnyIAPError(-99, "未知错误"));
        }

        public void RegisterEventMessage()
        {
            // 当前环境暂不支持该功能，忽略处理
        }
    }
}

