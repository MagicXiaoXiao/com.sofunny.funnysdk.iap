using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// 支付凭据
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPReceipt
    {
        /// <summary>
        /// 凭据编号（Funny 平台下的支付凭据编号）
        /// </summary>
        [JsonProperty("order_id")]
        public string Id { get; private set; }

        [JsonConstructor]
        internal IAPReceipt(string receiptId = "")
        {
            Id = receiptId;
        }
    }

}

