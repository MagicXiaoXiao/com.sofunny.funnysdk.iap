using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class NativeBridgeService : IFunnyIAPService
    {
        private readonly IBridgeMethods Bridge;

        internal NativeBridgeService()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            this.Bridge = new AndroidBridge();
#elif UNITY_IOS && !UNITY_EDITOR
            this.Bridge = new SwiftBridge();
#else
            this.Bridge = new DefaultBridge();
#endif
            Setup();
        }

        private void Setup()
        {
            NotificationCenterService.Default.AddObserver(this, "com.funny.iap.miss.receipt", (value) =>
            {
                if (value.TryGet<IAPReceipt>(out var receipt))
                {
                    OnMissReceiptHandlerEvents?.Invoke(receipt);
                }
            });

            Bridge.RegisterEventMessage();
        }

        public event Action<IAPReceipt> OnMissReceiptHandlerEvents;

        public void Execute(IAPOrder order, Action<IAPReceipt, IAPOrder> onSuccessHandler, Action onCancelHandler, Action<FunnyIAPError> onFailureHandler)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                onFailureHandler?.Invoke(FunnyIAPError.NetworkError);
                return;
            }

            Bridge.CallNavtive<IAPReceipt>(
                "ExecutePayment",
                new Dictionary<string, object>() { { "order", order } },
                (receipt, error) =>
                {
                    if (error is null) // 成功处理
                    {
                        onSuccessHandler?.Invoke(receipt, order);
                    }
                    else if (error.Equals(FunnyIAPError.CancelPaymentError)) // 取消处理
                    {
                        onCancelHandler?.Invoke();
                    }
                    else // 失败处理
                    {
                        onFailureHandler?.Invoke(error);
                    }
                }
            );
        }

        public void PreLoadProductInfo(IAPProduct[] products)
        {
            string[] array = products.Select(item => item.Id).Distinct().ToArray();

            PreLoadProductInfo(array);
        }

        public void PreLoadProductInfo(string[] productIdArray)
        {
            Bridge.CallNavtive("PreLoadProductInfo", new Dictionary<string, object>() { { "products", productIdArray } });
        }

        internal void SetNativeSaveData(string key, string value)
        {
            Bridge.CallNavtive("SaveData", new Dictionary<string, object>() { { key, value } });
        }

    }
}

