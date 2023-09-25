using System;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// 谷歌支付相关服务
    /// </summary>
    public interface IGooglePayService
    {
        /// <summary>
        /// 设置谷歌支付 PublicKey
        /// </summary>
        /// <param name="publicKey"></param>
        void SetBase64EncodedPublicKey(string publicKey);
    }
}

