using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    public static class FunnyIAP
    {
        static FunnyIAP()
        {
            NativeBridgeService native = new NativeBridgeService();
            Service = native;
            GooglePay = new GooglePayService(native);
        }

        /// <summary>
        /// Funny 内购主要服务模块
        /// </summary>
        public static IFunnyIAPService Service { get; private set; }

        /// <summary>
        /// GooglePay 相关配置模块
        /// </summary>
        public static IGooglePayService GooglePay { get; private set; }
    }
}


