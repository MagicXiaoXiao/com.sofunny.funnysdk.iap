#if UNITY_IOS
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class SwiftCall
    {
        [JsonProperty("method")]
        private readonly string rawValue;

        [JsonProperty("parameters")]
        private Dictionary<string, object> parameters;

        private SwiftCall(string rawValue)
        {
            this.rawValue = rawValue;
            this.parameters = new Dictionary<string, object>();
        }

        internal static SwiftCall Builder(string rawValue)
        {
            return new SwiftCall(rawValue);
        }

        internal SwiftCall Add(string key, object value)
        {
            parameters.Add(key, value);
            return this;
        }

        internal SwiftCall Add(Dictionary<string, object> dictionary)
        {
            parameters = parameters.Union(dictionary).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return this;
        }

        internal void Invoke()
        {
            string json = JsonConvert.SerializeObject(this);

            Logger.Log($"发起服务 {rawValue} 调用 - {json}");

            FIAP_Call(json);
        }

        internal T Invoke<T>()
        {
            string json = JsonConvert.SerializeObject(this);

            Logger.Log($"发起服务 {rawValue} 调用 - {json}");

            string reValue = FIAP_CallAndReturn(json);

            Logger.Log($"服务 {rawValue} 返回结果 - {reValue}");

            return JsonConvert.DeserializeObject<T>(reValue);
        }


        [DllImport("__Internal")]
        private static extern void FIAP_Call(string json);

        [DllImport("__Internal")]
        private static extern string FIAP_CallAndReturn(string json);
    }
}

#endif