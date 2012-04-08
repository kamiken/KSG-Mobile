using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mobile.Business;

namespace Mobile.Core
{
    public class ConfigManager
    {
        /// <summary>
        /// Get value from App Config
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value : default string.Empty</returns>
        public static string GetAppConfig(string key)
        {
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// Get System Config from Database (System_Config)
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Value: default string.Empty</returns>
        public static string GetSystemConfig(string key)
        {
            BLLConfig c = new BLLConfig();
            var obj = c.GetSystemConfig(key);
            return obj == null ? string.Empty : obj.ConfigValue;
        }

        public static string GetSystemConfig(string key, bool isActived)
        {
            BLLConfig c = new BLLConfig();
            var obj = c.GetSystemConfig(key , isActived);
            return obj == null ? string.Empty : obj.ConfigValue;
        }

        /// <summary>
        /// Get Company Config (System_Company_Config)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public static string GetCompanyConfig(string key, int companyID)
        {
            return string.Empty;
        }

        public static string DefaultLanguage
        {
            get
            {
                var defaultLanguage = GetSystemConfig(ConstantManager.DEFAULT_LANGUAGE);
                if (string.IsNullOrEmpty(defaultLanguage))
                {
                    defaultLanguage = GetAppConfig(ConstantManager.DEFAULT_LANGUAGE);
                    if (string.IsNullOrEmpty(defaultLanguage))
                    {
                        defaultLanguage = ConstantManager.vi_VN;
                    }
                }
                return defaultLanguage;
            }
        }

    }
}
