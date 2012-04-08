
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemRolePermission
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
    
        public virtual SystemPermission SystemPermission { get; set; }
        public virtual SystemRole SystemRole { get; set; }
    }
    
}
