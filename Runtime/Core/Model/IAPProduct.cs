using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPProduct
    {
        /// <summary>
        /// 商品编号（谷歌内购则为 SKU，苹果内购则为 ProductID）
        /// </summary>
        [JsonProperty("id")]
        public string Id = "";
        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("name")]
        public string Name = "";

        /// <summary>
        /// 商品类型
        /// </summary>
        /// <param name="id">谷歌内购则为 SKU，苹果内购则为 ProductID</param>
        /// <param name="name">名称</param>
        public IAPProduct(string id, string name = "")
        {
            Id = id;
            Name = name;
        }

        internal IAPProduct Clone()
        {
            return (IAPProduct)this.MemberwiseClone();
        }
    }
}

