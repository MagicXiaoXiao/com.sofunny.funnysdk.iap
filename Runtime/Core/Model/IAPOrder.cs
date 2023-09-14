using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// 订单类型
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPOrder
    {
        /// <summary>
        /// 商品
        /// </summary>
        [JsonProperty("product")]
        public IAPProduct Product { get; internal set; }
        /// <summary>
        /// 支付人
        /// </summary>
        [JsonProperty("payer")]
        public IAPPayer Payer { get; internal set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [JsonProperty("payment")]
        internal int paymentValue;
        public PaymentType Payment { get; internal set; }
        /// <summary>
        /// 购买数量，默认为 1
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; internal set; }
        /// <summary>
        /// 附加说明，最多 150 字符
        /// </summary>
        [JsonProperty("extra")]
        public string Extra { get; internal set; }

        private IAPOrder() { }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="product">商品</param>
        /// <param name="payer">支付人</param>
        /// <param name="payment">支付方式</param>
        /// <param name="quantity">购买数量，默认为 1 </param>
        /// <param name="extra">附加说明，最多 150 字符</param>
        /// <returns></returns>
        public static IAPOrder Create(IAPProduct product, IAPPayer payer, PaymentType payment, int quantity = 1, string extra = "")
        {
            IAPOrder order = new IAPOrder();

            order.Product = product.Clone();
            order.Payer = payer.Clone();
            order.Payment = payment;
            order.paymentValue = (int)payment;
            order.Quantity = quantity;
            order.Extra = string.Copy(extra);

            return order;
        }

    }
}

