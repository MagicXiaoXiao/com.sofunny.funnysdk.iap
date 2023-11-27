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
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; internal set; }

        /// <summary>
        /// 商品显示价格 (包含货币符号)
        /// </summary>
        [JsonProperty("displayPrice")]
        public string DisplayPrice { get; internal set; }

        /// <summary>
        /// 货币符号 如: $ 或 ¥ 等，根据地区变化
        /// </summary>
        [JsonProperty("symbol")]
        public string CurrencySymbol { get; internal set; }

        internal IAPProduct(string id)
        {
            Id = id;
        }

        internal IAPProduct Clone()
        {
            return (IAPProduct)this.MemberwiseClone();
        }
    }
}

