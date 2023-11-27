using System;

namespace SoFunny.FunnySDK.IAP
{
    public interface IFunnyIAPService
    {
        /// <summary>
        /// 遗漏的支付凭据处理事件（因异常情况导致遗漏已支付凭据）
        /// </summary>
        event Action<IAPReceipt[]> OnMissReceiptHandlerEvents;

        /// <summary>
        /// 获取商品列表信息
        /// </summary>
        /// <param name="onCompleteHandler"></param>
        void FetchProductList(string[] productIdArray, Action<IAPProduct[]> onSuccessHandler, Action<FunnyIAPError> onFailureHandler);

        /// <summary>
        /// 发起支付
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="onSuccessHandler">支付成功处理</param>
        /// <param name="onCancelHandler">支付取消处理</param>
        /// <param name="onFailureHandler">支付失败处理</param>
        void Execute(IAPOrder order, Action<IAPReceipt, IAPOrder> onSuccessHandler, Action onCancelHandler, Action<FunnyIAPError> onFailureHandler);

        /// <summary>
        /// 检查是否有遗漏的凭据。如有则会触发 OnMissReceiptHandlerEvents 事件
        /// </summary>
        void CheckMissReceiptQueue();

    }
}

