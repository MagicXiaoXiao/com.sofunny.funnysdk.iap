using System;

namespace SoFunny.FunnySDK.IAP
{
    public interface IFunnyIAPService
    {
        /// <summary>
        /// 遗漏的支付凭据处理事件（因异常情况导致遗漏已支付凭据）
        /// </summary>
        event Action<IAPReceipt> OnMissReceiptHandlerEvents;

        /// <summary>
        /// 预加载商品信息
        /// </summary>
        /// <param name="productIdArray">商品类型</param>
        void PreLoadProductInfo(IAPProduct[] products);

        /// <summary>
        /// 预加载商品信息（谷歌内购则为 SKU，苹果内购则为 ProductID）
        /// </summary>
        /// <param name="productIdArray">SKU 或 ProductID</param>
        void PreLoadProductInfo(string[] idArray);

        /// <summary>
        /// 发起支付
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="onSuccessHandler">支付成功处理</param>
        /// <param name="onCancelHandler">支付取消处理</param>
        /// <param name="onFailureHandler">支付失败处理</param>
        void Execute(IAPOrder order, Action<IAPReceipt, IAPOrder> onSuccessHandler, Action onCancelHandler, Action<FunnyIAPError> onFailureHandler);

    }
}

