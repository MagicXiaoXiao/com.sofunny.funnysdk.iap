#if UNITY_ANDROID
using System;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class AndroidCallBack<T> : AndroidJavaProxy
    {
        private readonly BridgeCompletedHandler<T> CallbackHandler;
        private readonly SynchronizationContext OriginalContext;

        internal AndroidCallBack(BridgeCompletedHandler<T> handler) : base("") // 指定回调处理类路径
        {
            OriginalContext = SynchronizationContext.Current;
            CallbackHandler = handler;
        }

        internal void OnSuccessHandler(string jsonModel)
        {
            Logger.Log($"AndroidBridgeCallback - Success - {jsonModel}");

            // 部分业务逻辑本身不需要有结果值，故此处无需做非空判断逻辑

            try
            {

                var model = JsonConvert.DeserializeObject<T>(jsonModel);

                OriginalContext.Post(_ =>
                {
                    CallbackHandler?.Invoke(model, null);

                }, null);
            }
            catch (JsonException ex)
            {
                Logger.LogError("AndroidBridgeCallback 回调数据解析出错 - " + ex.Message);

                OriginalContext.Post(_ =>
                {
                    CallbackHandler?.Invoke(default, new FunnyIAPError(1, "数据解析出错"));

                }, null);
            }
        }

        internal void OnFailureHandler(int code, string message)
        {
            Logger.LogError($"AndroidBridgeCallback - Failure - [code = {code}, message = {message}]");

            OriginalContext.Post(_ =>
            {
                CallbackHandler?.Invoke(default, new FunnyIAPError(code, message));

            }, null);
        }

    }
}

#endif
