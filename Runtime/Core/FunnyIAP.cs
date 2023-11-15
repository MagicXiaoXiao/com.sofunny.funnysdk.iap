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
        }

        /// <summary>
        /// Funny 内购主要服务模块
        /// </summary>
        public static IFunnyIAPService Service { get; private set; }

    }
}


