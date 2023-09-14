using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    public static class FunnyIAP
    {
        static FunnyIAP()
        {
            Service = new NativeBridgeService();
        }

        /// <summary>
        /// Funny 内购服务模块
        /// </summary>
        public static IFunnyIAPService Service { get; private set; }
    }
}


