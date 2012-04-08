
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemUserPermission
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
    }
    
}
