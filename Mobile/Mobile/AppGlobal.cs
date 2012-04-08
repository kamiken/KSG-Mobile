using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mobile.Business;

namespace Mobile
{
    public class AppGlobal
    {
        public static class Business
        {
            private static BLLRole businessRole = new BLLRole();
            private static BLLUser businessUser = new BLLUser();
            private static BLLConfig businessConfig = new BLLConfig();
            private static BLLCompany businessCompany = new BLLCompany();

            public static BLLRole BusinessRole { get {return businessRole; } }
            public static BLLUser BusinessUser { get { return businessUser; } }
            public static BLLConfig BusinessConfig { get { return businessConfig; } }
            public static BLLCompany BusinessCompany { get { return businessCompany; } }
        }
    }
}