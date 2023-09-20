#if UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SoFunny.FunnySDK.IAP
{
    internal class SwiftBridge : IBridgeMethods
    {
        internal SwiftBridge()
        {
            /* Tips: 
             * Unity iOS Bug 如不在代码中指定一次构造，
             * 则编译后，运行时使用 JsonConvert.DeserializeObject<IAPReceipt>(json);
             * 会抛出异常，找不到 IAPReceipt 的构造函数。
             * 故在此主动调用一次空构造。
             */
            new IAPReceipt();
        }

        public void CallNavtive(string method)
        {
            SwiftCall.Builder(method).Invoke();
        }

        public void CallNavtive(string method, Dictionary<string, object> parameters)
        {
            SwiftCall.Builder(method).Add(parameters).Invoke();
        }

        public T CallNavtive<T>(string method)
        {
            return SwiftCall.Builder(method).Invoke<T>();
        }

        public T CallNavtive<T>(string method, Dictionary<string, object> parameters)
        {
            return SwiftCall.Builder(method).Add(parameters).Invoke<T>();
        }

        public void CallNavtive<T>(string method, BridgeCompletedHandler<T> callback)
        {
            SwiftCallAndBack.Builder(method).AddCallbackHandler((result, json) =>
            {
                HandlerBridgeCallback(result, json, callback);

            }).Invoke();
        }

        public void CallNavtive<T>(string method, Dictionary<string, object> parameters, BridgeCompletedHandler<T> callback)
        {
            SwiftCallAndBack.Builder(method).Add(parameters).AddCallbackHandler((result, json) =>
            {
                HandlerBridgeCallback(result, json, callback);

            }).Invoke();
        }

        private void HandlerBridgeCallback<T>(bool result, string json, BridgeCompletedHandler<T> callback)
        {
            if (result)
            {
                try
                {
                    var model = JsonConvert.DeserializeObject<T>(json);
                    callback?.Invoke(model, null);
                }
                catch (JsonException ex)
                {
                    Logger.LogError($"数据解析异常,{ex.Message}");
                    callback?.Invoke(default, FunnyIAPError.InternalDataError);
                }
            }
            else
            {
                var errorJson = JObject.Parse(json);
                int code = errorJson.Value<int>("code");
                string message = errorJson.Value<string>("message");

                var error = new FunnyIAPError(code, message);

                callback?.Invoke(default, error);
            }
        }

        public void RegisterEventMessage()
        {
            FIAP_NotificationCenter(PostNotificationHandler);
        }

        private delegate void NotificationMessage(string name, string jsonString);

        [DllImport("__Internal")]
        private static extern void FIAP_NotificationCenter(NotificationMessage message);

        [MonoPInvokeCallback(typeof(NotificationMessage))]
        protected static void PostNotificationHandler(string name, string jsonString)
        {
            if (string.IsNullOrEmpty(name))
            {
                Logger.LogWarning("native event name is empty!");
                return;
            }

            NotificationCenterService.Default.Post(name, IAPValue.Create(jsonString));
        }

    }
}

#endif