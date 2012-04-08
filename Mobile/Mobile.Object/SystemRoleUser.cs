
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemRoleUser
    {
        public int ID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual SystemRole SystemRole { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
    
}
