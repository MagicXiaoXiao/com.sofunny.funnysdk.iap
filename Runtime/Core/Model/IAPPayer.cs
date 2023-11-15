using System;
using Newtonsoft.Json;

namespace SoFunny.FunnySDK.IAP
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IAPPayer
    {
        /// <summary>
        /// 支付人编号（如，游戏角色唯一编号）
        /// </summary>
        [JsonProperty("id")]
        public string Id = "";
        /// <summary>
        /// 支付人名称（如，游戏角色昵称）
        /// </summary>
        [JsonProperty("name")]
        public string Name = "";
        /// <summary>
        /// 支付人服务器信息（如，游戏区服等）
        /// </summary>
        [JsonProperty("info")]
        public string ServerInfo = "";

        /// <summary>
        /// 支付人类型
        /// </summary>
        internal IAPPayer() { }

        /// <summary>
        /// 支付人类型
        /// </summary>
        /// <param name="id">支付人编号（如，游戏角色唯一编号）</param>
        /// <param name="name">支付人名称（如，游戏角色昵称）</param>
        /// <param name="serverInfo">支付人服务器信息（如，游戏区服等）</param>
        private IAPPayer(string id, string name, string serverInfo)
        {
            Id = id;
            Name = name;
            ServerInfo = serverInfo;
        }

        /// <summary>
        /// 创建支付类型
        /// </summary>
        /// <param name="id">支付人编号（如，游戏角色唯一编号）</param>
        /// <param name="name">支付人名称（如，游戏角色昵称）</param>
        /// <param name="serverInfo">支付人服务器信息（如，游戏区服等）</param>
        /// <returns></returns>
        public static IAPPayer Create(string id, string name, string serverInfo)
        {
            return new IAPPayer(id, name, serverInfo);
        }

        internal IAPPayer Clone()
        {
            return (IAPPayer)this.MemberwiseClone();
        }

    }
}

