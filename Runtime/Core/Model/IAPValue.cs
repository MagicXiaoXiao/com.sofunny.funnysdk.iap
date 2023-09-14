using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    internal class IAPValue
    {
        internal static IAPValue Empty = new IAPValue();

        internal static IAPValue Create(string value)
        {
            return new IAPValue(value);
        }

        private readonly string optional;

        private IAPValue()
        {
            optional = "";
        }

        private IAPValue(string value)
        {
            optional = value;
        }

        internal bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(optional);
            }
        }

        internal string RawValue => optional;

        internal bool TryGet<T>(out T target)
        {
            target = default;

            if (string.IsNullOrEmpty(optional))
            {
                return false;
            }
            try
            {
                target = JsonConvert.DeserializeObject<T>(optional);
                return true;
            }
            catch (JsonException ex)
            {
                Logger.LogError("IAPValue Deserialize error. " + ex.Message);
                return false;
            }

        }
    }
}

