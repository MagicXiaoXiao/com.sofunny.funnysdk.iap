using System;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP.Editor
{
    /// <summary>
    /// FunnyIAP Android 内购配置抽象类
    /// </summary>
    public abstract class FunnyIAPAndroidExportConfig
    {
        /// <summary>
        /// 服务目标渠道
        /// </summary>
        //public abstract FunnyIAPTarget Target { get; }

        /// <summary>
        /// Google 支付 PublicKey
        /// </summary>
        public abstract string GooglePayPublicKey { get; }

    }
}

