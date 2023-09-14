using System;
using System.Collections.Generic;

namespace SoFunny.FunnySDK.IAP
{
    internal delegate void BridgeCompletedHandler<T>(T model, FunnyIAPError error);

    internal interface IBridgeMethods
    {
        void CallNavtive(string method);

        void CallNavtive(string method, Dictionary<string, object> parameters);

        T CallNavtive<T>(string method);

        T CallNavtive<T>(string method, Dictionary<string, object> parameters);

        void CallNavtive<T>(string method, BridgeCompletedHandler<T> callback);

        void CallNavtive<T>(string method, Dictionary<string, object> parameters, BridgeCompletedHandler<T> callback);

        void RegisterEventMessage();
    }

}

