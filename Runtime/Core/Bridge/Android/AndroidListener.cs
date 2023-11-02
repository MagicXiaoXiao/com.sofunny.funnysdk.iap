#if UNITY_ANDROID
using System;
using System.Threading;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class AndroidListener : AndroidJavaProxy
    {
        private readonly SynchronizationContext OriginalContext;

        internal AndroidListener() : base("com.xmfunny.funnysdk.unitywrapper.listener.FunnySdkListener") // 指定 Android 事件监听类路径
        {
            OriginalContext = SynchronizationContext.Current;
        }

        internal void Post(string identifier)
        {
            OriginalContext.Post(_ =>
            {
                NotificationCenterService.Default.Post(identifier);
            }, null);

        }

        internal void Post(string identifier, string jsonValue)
        {
            OriginalContext.Post(_ =>
            {
                NotificationCenterService.Default.Post(identifier, IAPValue.Create(jsonValue));
            }, null);

        }
    }
}

#endif
