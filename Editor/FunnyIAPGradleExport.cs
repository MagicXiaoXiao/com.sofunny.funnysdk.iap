#if UNITY_ANDROID
using System.IO;
using System.Linq;
using UnityEditor.Android;

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
            DirectoryInfo unityLibrary = new DirectoryInfo(path);
            var gradleFiles = unityLibrary.GetFiles("build.gradle");
            var unityLibraryGradleFile = gradleFiles.First();

            var unityLibraryGradle = new GradleConfig(unityLibraryGradleFile.FullName);

            var depNode = unityLibraryGradle.ROOT.FindChildNodeByName("dependencies");
            depNode.AppendContentNode("implementation 'com.android.billingclient:billing-ktx:5.1.0'");
            unityLibraryGradle.Save();
        }
    }
}
#endif