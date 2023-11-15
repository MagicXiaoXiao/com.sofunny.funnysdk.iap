using System;

namespace SoFunny.FunnySDK.IAP
{
    public class IAPPayment
    {
        /// <summary>
        /// Apple 内购渠道
        /// </summary>
        /// <returns></returns>
        public static IAPPayment Apple()
        {
            IAPPayment payment = new IAPPayment();
            payment.type = PaymentType.AppleInAppPurchase;

            return payment;
        }

        /// <summary>
        /// GooglePay 渠道
        /// </summary>
        /// <param name="base64EncodedPublicKey">GooglePay 公钥</param>
        /// <returns></returns>
        public static IAPPayment GooglePay(string base64EncodedPublicKey)
        {
            IAPPayment payment = new IAPPayment();
            payment.type = PaymentType.GooglePay;
            payment.base64EncodedPublicKey = base64EncodedPublicKey;

            return payment;
        }

        internal string base64EncodedPublicKey; // GooglePay 公钥
        internal PaymentType type; // 支付类型

        private IAPPayment() { }

        internal IAPPayment Clone()
        {
            return (IAPPayment)this.MemberwiseClone();
        }

    }
}

