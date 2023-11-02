#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// 安卓桥接实现类，可根据实际桥接情况进行实现调整。
    /// </summary>
    internal class AndroidBridge : IBridgeMethods
    {
        private readonly AndroidJavaObject AndroidWapper;

        internal AndroidBridge()
        {
            AndroidWapper = new AndroidJavaObject("com.xmfunny.funnysdk.unitywrapper.internal.unity.FunnySdkWrapper4UnityPay"); // 指定桥接类名路径

            // 如需触发初始化，则自行在此调用自定义桥接方法
        }

        public void CallNavtive(string method)
        {
            AndroidWapper.Call(method);
        }

        public void CallNavtive(string method, Dictionary<string, object> parameters)
        {
            string jsonValue = JsonConvert.SerializeObject(parameters);
            AndroidWapper.Call(method, jsonValue);
        }

        public T CallNavtive<T>(string method)
        {
            string returnJsonValue = AndroidWapper.Call<string>(method);
            return JsonConvert.DeserializeObject<T>(returnJsonValue);
        }

        public T CallNavtive<T>(string method, Dictionary<string, object> parameters)
        {
            string jsonValue = JsonConvert.SerializeObject(parameters);
            string returnJsonValue = AndroidWapper.Call<string>(method, jsonValue);

            return JsonConvert.DeserializeObject<T>(returnJsonValue);
        }

        public void CallNavtive<T>(string method, BridgeCompletedHandler<T> callback)
        {
            AndroidWapper.Call(method, new AndroidCallBack<T>(callback));
        }

        public void CallNavtive<T>(string method, Dictionary<string, object> parameters, BridgeCompletedHandler<T> callback)
        {
            string jsonValue = JsonConvert.SerializeObject(parameters);
            AndroidWapper.Call(method, jsonValue, new AndroidCallBack<T>(callback));
        }

        public void RegisterEventMessage()
        {
            AndroidWapper.Call("RegisterEventMessage", new AndroidListener());
        }
    }
}

#endif
