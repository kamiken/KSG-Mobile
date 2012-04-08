using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Mobile.Core.Web
{
    
    [Serializable]
    public static class ResxManager
    {
        private static ResourceManager _resxManager;
        private static ResourceManager ResourceManager
        {
            get
            {
                if (_resxManager == null)
                {
                    try
                    {
                        _resxManager = new ResourceManager("Resources.Lang", System.Reflection.Assembly.Load("App_GlobalResources"));
                    }
                    catch { }
                }
                return _resxManager;
            }
        }

        private static Dictionary<string, CultureInfo> _dicCulture;
        private static Dictionary<string, CultureInfo> DicCulture
        {
            get
            {
                if (_dicCulture == null)
                {
                    _dicCulture = new Dictionary<string, CultureInfo>();
                }
                return _dicCulture;
            }
        }

        private static CultureInfo GetCulture(string pLangName)
        {
            if (!DicCulture.ContainsKey(pLangName))
            {
                DicCulture.Add(pLangName, new CultureInfo(pLangName));
            }
            return DicCulture[pLangName];
        }


        /// <summary>
        /// Ngôn ngữ hiện tại đang sử dụng
        /// </summary>
        public static string CurrentLanguage
        {
            get
            {
                if (ContextManager.CurrentSession != null && ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE] != null)
                    return ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE].ToString();
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }
        /// <summary>
        /// Culture hiện tại đang sử dụng
        /// </summary>
        public static CultureInfo CurrentCultureInfo
        {
            get
            {
                if (ContextManager.CurrentSession != null)
                {
                    if (ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE] == null)
                        ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE] = ConfigManager.DefaultLanguage;
                    return GetCulture(ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE].ToString());
                }
                return GetCulture(ConfigManager.DefaultLanguage);
            }
        }
        /// <summary>
        /// Hàm dựng để đặt ngôn ngữ hiển thị từ Session
        /// </summary>
        static ResxManager()
        {
            //Thread.CurrentThread.CurrentCulture = CurrentCultureInfo;
            Thread.CurrentThread.CurrentCulture = GetCulture(ConfigManager.DefaultLanguage);
        }
        public static void SetLanguage(string pLangName)
        {
            ContextManager.CurrentSession[ConstantManager.SS_LANGUAGE] = pLangName;
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(pLangName);
        }
        /// <summary>
        /// Lấy nội dung từ tài nguyên
        /// </summary>
        /// <param name="pResxName">Tên Chuỗi</param>
        /// <returns></returns>
        public static string GetResx(String pResxName)
        {
            return GetResx(pResxName, false);
        }
        /// <summary>
        /// Lấy nội dung từ tài nguyên
        /// </summary>
        /// <param name="pResxName">Key in ResourceLang</param>
        /// <param name="pLowerFirstKey">IsToLower</param>
        /// <returns></returns>
        public static string GetResx(String pResxName, bool pLowerFirstKey)
        {
            string str = "";
            try
            {
                //_resxManager = new ResourceManager("Resources.Lang", System.Reflection.Assembly.Load("App_GlobalResources"));
                str = ResourceManager.GetString(pResxName, CurrentCultureInfo);
                if (pLowerFirstKey)
                    str = str.Substring(0, 1).ToLower() + str.Substring(1);
            }
            catch { }
            return str;
        }
    }
}
