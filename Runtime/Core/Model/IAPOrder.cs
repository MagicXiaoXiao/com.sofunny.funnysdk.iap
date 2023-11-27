using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /* 转换为 JSON 后格式如下
     {
	    "order": {
		"payment": 1,
		"product": {
			"id": "com.sofunny.product",
			"name": ""
		},
		"payer": {
			"id": "user_id",
			"name": "user_name",
			"info": "server_area"
		},
		"quantity": 1,
		"extra": ""
	    }
     }
     */

    /// <summary>
    /// 订单类型
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPOrder
    {

        [JsonProperty("product")]
        private readonly IAPProduct _product;
        /// <summary>
        /// 商品
        /// </summary>
        public IAPProduct Product { get { return _product; } }


        [JsonProperty("payer")]
        private readonly IAPPayer _payer;
        /// <summary>
        /// 支付人
        /// </summary>
        public IAPPayer Payer { get { return _payer; } }

        [JsonProperty("payment")]
        private readonly int _paymentValue;
        /// <summary>
        /// 支付方式
        /// </summary>
        public readonly PaymentType Payment;

        [JsonProperty("quantity")]
        private readonly int _quantity;
        /// <summary>
        /// 购买数量，默认为 1
        /// </summary>
        public int Quantity { get { return _quantity; } }

        [JsonProperty("extra")]
        private readonly string _extra;
        /// <summary>
        /// 附加说明，最多 150 字符
        /// </summary>
        public string Extra { get { return _extra; } }


        internal IAPOrder(IAPProduct product, IAPPayer payer, PaymentType payment, int quantity = 1, string extra = "")
        {
            _product = product;
            _payer = payer;
            Payment = payment;
            _paymentValue = (int)payment;
            _quantity = quantity;
            _extra = extra;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="product">商品信息</param>
        /// <param name="payer">支付人</param>
        /// <param name="payment">支付方式</param>
        /// <param name="quantity">购买数量，默认为 1 </param>
        /// <param name="extra">附加说明，最多 150 字符</param>
        /// <returns></returns>
        public static IAPOrder Create(IAPProduct product, IAPPayer payer, PaymentType payment, int quantity = 1, string extra = "")
        {
            return new IAPOrder(product.Clone(), payer.Clone(), payment, quantity, extra);
        }
    }
}

