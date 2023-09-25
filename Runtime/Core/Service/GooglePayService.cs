using System;
using UnityEngine;

namespace SoFunny.FunnySDK.IAP
{
    internal class GooglePayService : IGooglePayService
    {
        internal GooglePayService()
        {
        }

        public void SetBase64EncodedPublicKey(string publicKey)
        {
#if UNITY_ANDROID
            PlayerPrefs.SetString("com.sofunny.funnyiap.googlepay.publickey", publicKey);
            PlayerPrefs.Save();
#endif
        }
    }
}

