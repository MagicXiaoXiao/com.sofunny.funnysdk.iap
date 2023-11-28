#if UNITY_ANDROID
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Android;
using System.Xml;
using System.Text;


namespace SoFunny.FunnySDK.IAP.Editor
{
    public class FunnyIAPGradleExport : IPostGenerateGradleAndroidProject
    {
        public virtual string NamespaceURI => "http://schemas.android.com/apk/res/android";

        public int callbackOrder
        {
            get
            {
                return 701;
            }
        }

        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
            List<Type> configList = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(FunnyIAPAndroidExportConfig).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .ToList();

            if (configList.Count == 0)
            {
                Debug.LogWarning("[FunnyIAP] - [Build] - Warning: 当前项目不存在 FunnyIAPAndroidExportConfig 的子类配置对象。无法进行 FunnyIAP 构建逻辑");
                return;
            }

            if (configList.Count > 1)
            {
                foreach (var item in configList)
                {
                    Debug.LogWarning($"[FunnyIAP] - [Build] - Warning: 重复的 FunnyIAPAndroidExportConfig 配置类 -> {item.Name}");
                }

                Debug.LogException(new Exception("[FunnyIAP] - [Build] - Error: 存在多个配置项,请确保项目中只有 1 个 FunnyIAPAndroidExportConfig 的子类"));
                return;
            }

            FunnyIAPAndroidExportConfig exportConfig = (FunnyIAPAndroidExportConfig)Activator.CreateInstance(configList.First());

            if (string.IsNullOrEmpty(exportConfig.GooglePayPublicKey))
            {
                Debug.LogWarning("[FunnyIAP] - [Build] - Warning: 配置类中 GooglePayPublicKey 值为空。无法进行 FunnyIAP 构建逻辑");
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(exportConfig.GooglePayPublicKey);
            string publicKey = Convert.ToBase64String(bytes);

            #region strings.xml 构建

            var stringsPath = Path.Combine(path, "src/main/res/values/strings.xml");
            XmlDocument stringsDoc = new XmlDocument();

            if (File.Exists(stringsPath))
            {
                stringsDoc.Load(stringsPath);
            }
            else
            {
                XmlDeclaration xmlDeclaration = stringsDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = stringsDoc.DocumentElement;
                stringsDoc.InsertBefore(xmlDeclaration, root);

                XmlElement resources = stringsDoc.CreateElement(string.Empty, "resources", string.Empty);
                stringsDoc.AppendChild(resources);
            }


            var nodeResources = stringsDoc.SelectSingleNode("resources");

            XmlElement publicKeyStrings = stringsDoc.CreateElement("string");
            publicKeyStrings.SetAttribute("name", "funnyiap_googlepay_publickey");
            publicKeyStrings.InnerText = publicKey;
            nodeResources.AppendChild(publicKeyStrings);

            stringsDoc.Save(stringsPath);

            #endregion


            #region AndroidManifest.xml 构建

            var manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");

            XmlDocument manifestDoc = new XmlDocument();
            manifestDoc.Load(manifestPath);

            var rootNode = manifestDoc.DocumentElement;
            var applicationNode = rootNode.SelectSingleNode("application");

            XmlElement publicKeyNode = manifestDoc.CreateElement("meta-data");
            publicKeyNode.SetAttribute("name", NamespaceURI, "funnyiap.googlepay.publickey");
            publicKeyNode.SetAttribute("value", NamespaceURI, "@string/funnyiap_googlepay_publickey");
            applicationNode.AppendChild(publicKeyNode);

            manifestDoc.Save(manifestPath);

            #endregion



            #region gradle 构建

            DirectoryInfo unityLibrary = new DirectoryInfo(path);
            var gradleFiles = unityLibrary.GetFiles("build.gradle");
            var unityLibraryGradleFile = gradleFiles.First();

            var unityLibraryGradle = new GradleConfig(unityLibraryGradleFile.FullName);

            var depNode = unityLibraryGradle.ROOT.FindChildNodeByName("dependencies");
            depNode.AppendContentNode("implementation 'com.android.billingclient:billing:5.2.1'");
            unityLibraryGradle.Save();

            #endregion
        }
    }
}
#endif