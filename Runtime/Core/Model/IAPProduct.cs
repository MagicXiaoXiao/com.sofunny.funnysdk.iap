using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// 商品数据
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPProduct
    {
        /// <summary>
        /// 商品编号（谷歌内购则为 SKU，苹果内购则为 ProductID）
        /// </summary>
        public string Id => _id;

        [JsonProperty("id")]
        private string _id;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name => _name;

        [JsonProperty("name")]
        private string _name;

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description => _description;

        [JsonProperty("description")]
        private string _description;

        /// <summary>
        /// 商品价格
        /// </summary>
        public string Price => _price;

        [JsonProperty("price")]
        private string _price;

        /// <summary>
        /// 商品显示价格 (包含货币符号)
        /// </summary>
        public string DisplayPrice => _displayPrice;

        [JsonProperty("displayPrice")]
        private string _displayPrice;

        /// <summary>
        /// 货币代码 如: CNY 或 USD 等，根据地区变化
        /// </summary>
        public string CurrencyCode => _currencyCode;

        [JsonProperty("currencyCode")]
        private string _currencyCode;


        internal IAPProduct() { }

        internal IAPProduct Clone()
        {
            return (IAPProduct)this.MemberwiseClone();
        }
    }
}

