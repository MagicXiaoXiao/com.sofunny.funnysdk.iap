using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    /// <summary>
    /// FunnyIAP 错误类型
    /// </summary>
    public partial class FunnyIAPError : IEquatable<FunnyIAPError>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; private set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// IAP 错误类型
        /// </summary>
        public IAPErrorType IAPError { get; private set; }

        internal FunnyIAPError(int code, string message)
        {
            if (Enum.TryParse<IAPErrorType>(code.ToString(), out var enumValue))
            {
                this.Code = (int)enumValue;
                this.IAPError = enumValue;
            }
            else
            {
                this.Code = (int)IAPErrorType.InternalDataError;
                this.IAPError = IAPErrorType.InternalDataError;

                Logger.LogWarning($"未定义的错误：{code}-{message}，将其转换为 InternalDataError.");
            }

            this.Message = message;
        }

        public bool Equals(FunnyIAPError other)
        {
            return this.Code == other.Code;
        }
    }

    /// <summary>
    /// IAP 错误类型
    /// </summary>
    public enum IAPErrorType
    {
        /// <summary>
        /// 发生未知错误
        /// </summary>
        UnknownError = -1,

        /// <summary>
        /// 内部数据发生错误
        /// </summary>
        InternalDataError = 1,

        /// <summary>
        /// 网络错误（当前无网络）
        /// </summary>
        NetworkError = 2,

        /// <summary>
        /// 当前未登录账号
        /// </summary>
        NotLoginError = 401,

        /// <summary>
        /// 服务器发生错误
        /// </summary>
        ConnectServerError = 500,

        /// <summary>
        /// 商品不存在
        /// </summary>
        ProductNotFoundError = 1001,
    }

    // 内部错误码
    public partial class FunnyIAPError
    {
        private FunnyIAPError(IAPErrorType type)
        {
            this.Code = (int)type;
            this.IAPError = type;

            switch (type)
            {
                case IAPErrorType.NetworkError:
                    this.Message = "网络错误";
                    break;
                case IAPErrorType.ConnectServerError:
                    this.Message = "服务器发生错误";
                    break;
                case IAPErrorType.NotLoginError:
                    this.Message = "未登录账号";
                    break;
                case IAPErrorType.ProductNotFoundError:
                    this.Message = "商品不存在";
                    break;
                case IAPErrorType.InternalDataError:
                    this.Message = "内部数据发生错误";
                    break;
                case IAPErrorType.UnknownError:
                    this.Message = "未知错误";
                    break;
                default:
                    this.Message = "未知错误";
                    break;
            }
        }

        /// <summary>
        /// 内部数据发生错误
        /// </summary>
        internal static FunnyIAPError InternalDataError => new FunnyIAPError(IAPErrorType.InternalDataError);

        /// <summary>
        /// 取消支付
        /// </summary>
        internal static FunnyIAPError CancelPaymentError => new FunnyIAPError(1003, "当前支付已被取消");

        /// <summary>
        /// 取消支付
        /// </summary>
        internal static FunnyIAPError NetworkError => new FunnyIAPError(IAPErrorType.NetworkError);
    }

}

