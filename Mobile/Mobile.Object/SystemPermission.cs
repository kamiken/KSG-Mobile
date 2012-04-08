
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemPermission
    {
        public SystemPermission()
        {
            this.SystemRolePermissions = new Collection<SystemRolePermission>();
        }
    
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> CompanyID { get; set; }
    
        public virtual Collection<SystemRolePermission> SystemRolePermissions { get; set; }
    }
    
}
