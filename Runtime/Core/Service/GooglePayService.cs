using System;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class GooglePayService : IGooglePayService
    {
        private NativeBridgeService Native;

        internal GooglePayService(NativeBridgeService service)
        {
            Native = service;
        }

        public void SetBase64EncodedPublicKey(string publicKey)
        {
#if UNITY_ANDROID
            Native.SetNativeSaveData("com.sofunny.funnyiap.googlepay.publickey", publicKey);
#endif
        }
    }
}

