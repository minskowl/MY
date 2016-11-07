using System;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;

namespace Savchin.Core
{
    public struct AssemInfo
    {
        public string Name;
        public string Locale;
        public string Codebase;
        public string Modified;
        public string OSType;
        public string OSVersion;
        public string ProcType;
        public string PublicKey;
        public string PublicKeyToken;
        public string Version;
        public readonly Fusion.CacheType CacheType;
        public string sCustom;
        public string sFusionName;
        public readonly object Source;

        public AssemInfo(object assemInfo)
        {
            Source = assemInfo;
            Name = assemInfo.GetField<string>("Name");
            Locale = assemInfo.GetField<string>("Locale");
            Codebase = assemInfo.GetField<string>("Codebase");
            Modified = assemInfo.GetField<string>("Modified");
            OSType = assemInfo.GetField<string>("OSType");
            OSVersion = assemInfo.GetField<string>("OSVersion");
            ProcType = assemInfo.GetField<string>("ProcType");
            PublicKey = assemInfo.GetField<string>("PublicKey");
            PublicKeyToken = assemInfo.GetField<string>("PublicKeyToken");
            Version = assemInfo.GetField<string>("Version");
            uint nCacheType = (uint)assemInfo.GetField("nCacheType");
            CacheType = (Fusion.CacheType)nCacheType;
            sCustom = (string)assemInfo.GetField("sCustom");
            sFusionName = (string)assemInfo.GetField("sFusionName");
        }
    }

    /// <summary>
    /// Class for GAC managment
    /// </summary>
    public static class Fusion
    {
        const BindingFlags bindingFlags = (BindingFlags)314;
        private static string[] _asmVersions = new[] { "4.0.0.0", "3.5.0.0", "2.0.0.0", "1.0.5000.0" };
        public enum CacheType
        {
            Zap = 0x1,
            GAC = 0x2,
            Download = 0x4
        }

        static Type FusionType;

        static Fusion()
        {
            FusionType = CreateFusionAssembly().GetType("Microsoft.CLRAdmin.Fusion");
        }
        private static Assembly CreateFusionAssembly()
        {
            foreach (var version in _asmVersions)
            {
                try
                {
                    var a = Assembly.Load(string.Format("mscorcfg, Version={0}, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", version));
                    if (a != null) return a;
                }
                catch
                {
                }
            }
            return null;
        }

        /// <summary>
        /// Adds the assemblyto gac.
        /// </summary>
        /// <param name="sAssemFilename">The s assem filename.</param>
        /// <returns></returns>
        public static int AddAssemblytoGac(string sAssemFilename)
        {
            var args = new object[] { sAssemFilename };
            return ((int)(FusionType.InvokeMember("AddAssemblytoGac", bindingFlags, null, null, args)));
        }

        /// <summary>
        /// Gacs the uninstall.
        /// </summary>
        /// <param name="assemblyMathcer">The assembly mathcer.</param>
        /// <returns></returns>
        public static bool GacUninstall(AssemInfo assemblyMathcer)
        {
            return GacUninstall(FindInGac(assemblyMathcer));
        }
        /// <summary>
        /// Gacs the uninstall.
        /// </summary>
        /// <param name="assemInfo">The assem info.</param>
        /// <returns></returns>
        public static bool GacUninstall(object assemInfo)
        {
            if (assemInfo == null)
                return false;
            return (bool)FusionType.InvokeMember("RemoveAssemblyFromGac", bindingFlags, null, null, new object[1] { assemInfo });
        }

        /// <summary>
        /// Gacs the uninstall.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        public static bool GacUninstall(string assemblyName)
        {
            object assemInfo = null;
            var fcGac = new ArrayList();
            ReadCache(fcGac, CacheType.GAC);
            foreach (object oAssemInfo in fcGac)
            {
                if (oAssemInfo.GetField<string>("Name") == assemblyName)
                    assemInfo = oAssemInfo;
            }
            return GacUninstall(assemInfo);
        }

        /// <summary>
        /// Finds the in gac.
        /// </summary>
        /// <param name="assemblyMathcer">The assembly mathcer.</param>
        /// <returns></returns>
        public static object FindInGac(AssemInfo assemblyMathcer)
        {
            var fcGac = new ArrayList();
            ReadCache(fcGac, CacheType.GAC);
            foreach (object oAssemInfo in fcGac)
            {
                if (!string.IsNullOrEmpty(assemblyMathcer.sFusionName) &&
                    oAssemInfo.GetField<string>("sFusionName") == assemblyMathcer.sFusionName)
                    return oAssemInfo;

                if (oAssemInfo.GetField<string>("Name") != assemblyMathcer.Name)
                    continue;


                if (!string.IsNullOrEmpty(assemblyMathcer.Version) &&
                oAssemInfo.GetField<string>("Version") != assemblyMathcer.Version)
                    continue;

                if (!string.IsNullOrEmpty(assemblyMathcer.PublicKeyToken) &&
oAssemInfo.GetField<string>("PublicKeyToken") != assemblyMathcer.PublicKeyToken)
                    continue;
                return oAssemInfo;
            }
            return null;
        }

        /// <summary>
        /// Gets the cache type string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static String GetCacheTypeString(CacheType type)
        {
            return ((String)(FusionType.InvokeMember("GetCacheTypeString", bindingFlags, null, null, new object[] { (UInt32)type })));
        }

        /// <summary>
        /// Reads the cache.
        /// </summary>
        /// <param name="alAssems">The al assems.</param>
        /// <param name="nFlag">The n flag.</param>
        public static void ReadCache(ArrayList alAssems, CacheType nFlag)
        {
            FusionType.InvokeMember("ReadCache", bindingFlags, null, null, new object[] { alAssems, (UInt32)nFlag });
        }

        /// <summary>
        /// Gets the known fusion apps.
        /// </summary>
        /// <returns></returns>
        public static StringCollection GetKnownFusionApps()
        {
            object[] args = new object[0];
            return ((StringCollection)(FusionType.InvokeMember("GetKnownFusionApps", bindingFlags, null, null, args)));
        }
    }
}


