using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mobile.Business;

namespace MvcMobile
{
    public class AppGlobal
    {
        public class Services
        {
            private static BLLRole businessRole = new BLLRole();
            private static BLLUser businessUser = new BLLUser();
            private static BLLConfig businessConfig = new BLLConfig();
            private static BLLCompany businessCompany = new BLLCompany();
            private static BLLPermission businessPermission = new BLLPermission();

            public static BLLRole BusinessRole { get {return businessRole; } }
            public static BLLUser BusinessUser { get { return businessUser; } }
            public static BLLConfig BusinessConfig { get { return businessConfig; } }
            public static BLLCompany BusinessCompany { get { return businessCompany; } }
            public static BLLPermission BusinessPermission { get { return businessPermission; } }

            private static PermissionService.PermissionService _servicePermission = new PermissionService.PermissionService();
            public static PermissionService.PermissionService ServicePermission { get { return _servicePermission; } }
        }
    }
}