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
                if (result)
                {
                    var model = JsonConvert.DeserializeObject<T>(json);
                    callback?.Invoke(model, null);
                }
                else
                {
                    var errorJson = JObject.Parse(json);
                    int code = errorJson.Value<int>("code");
                    string message = errorJson.Value<string>("message");

                    var error = new FunnyIAPError(code, message);

                    callback?.Invoke(default, error);
                }
            }).Invoke();
        }

        public void CallNavtive<T>(string method, Dictionary<string, object> parameters, BridgeCompletedHandler<T> callback)
        {
            SwiftCallAndBack.Builder(method).Add(parameters).AddCallbackHandler((result, json) =>
            {
                if (result)
                {
                    var model = JsonConvert.DeserializeObject<T>(json);
                    callback?.Invoke(model, null);
                }
                else
                {
                    var errorJson = JObject.Parse(json);
                    int code = errorJson.Value<int>("code");
                    string message = errorJson.Value<string>("message");

                    var error = new FunnyIAPError(code, message);

                    callback?.Invoke(default, error);
                }
            }).Invoke();
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