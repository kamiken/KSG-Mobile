
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemRole
    {
        public SystemRole()
        {
            this.SystemRolePermissions = new Collection<SystemRolePermission>();
            this.SystemRoleUsers = new Collection<SystemRoleUser>();
        }
    
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
    
        public virtual Collection<SystemRolePermission> SystemRolePermissions { get; set; }
        public virtual Collection<SystemRoleUser> SystemRoleUsers { get; set; }
    }
    
}
