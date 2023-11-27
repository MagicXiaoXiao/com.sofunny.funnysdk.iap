using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class NativeBridgeService : IFunnyIAPService
    {
        private readonly IBridgeMethods Bridge;
        private bool _paying = false;

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
            // 监听遗漏的凭据通知
            NotificationCenterService.Default.AddObserver(this, "com.funny.iap.miss.receipt", (value) =>
            {
                if (value.TryGet<IAPReceipt[]>(out var receiptArray)) // 获遗漏的取凭据数组
                {
                    if (receiptArray.Length > 0)
                    {
                        OnMissReceiptHandlerEvents?.Invoke(receiptArray);
                    }
                }
                else
                {
                    Logger.LogVerbose($"遗漏的凭据组解析失败 - {value.RawValue}");
                }
            });

            Bridge.RegisterEventMessage();
        }

        public event Action<IAPReceipt[]> OnMissReceiptHandlerEvents;

        public void Execute(IAPOrder order, Action<IAPReceipt, IAPOrder> onSuccessHandler, Action onCancelHandler, Action<FunnyIAPError> onFailureHandler)
        {
            if (_paying)
            {
                Logger.LogWarning("已有一项交易正在进行中，请等待完成后再发起。");
                return;
            }

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                onFailureHandler?.Invoke(FunnyIAPError.NetworkError);
                return;
            }

            _paying = true;

            Bridge.CallNavtive<IAPReceipt>(
                "ExecutePayment",
                new Dictionary<string, object>() { { "order", order } },
                (receipt, error) =>
                {
                    _paying = false;

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

        internal void SetNativeSaveData(string key, string value)
        {
#if UNITY_ANDROID
            Bridge.CallNavtive("SaveData", new Dictionary<string, object>() { { key, value } });
#endif
        }

        public void CheckMissReceiptQueue()
        {
            Bridge.CallNavtive("CheckMissReceipt");
        }

        public void FetchProductList(string[] productIdArray, Action<IAPProduct[]> onSuccessHandler, Action<FunnyIAPError> onFailureHandler)
        {
            if (productIdArray.Length <= 0)
            {
                onSuccessHandler?.Invoke(Array.Empty<IAPProduct>());
                return;
            }

            Bridge.CallNavtive<IAPProduct[]>(
                "FetchProductList",
                new Dictionary<string, object> { { "ids", productIdArray } },
                (list, error) =>
                {
                    if (error is null)
                    {
                        onSuccessHandler?.Invoke(list);
                    }
                    else
                    {
                        onFailureHandler?.Invoke(error);
                    }
                }
            );
        }
    }
}

