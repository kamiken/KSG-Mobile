
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            this.SystemRoleUsers = new Collection<SystemRoleUser>();
            this.SystemUser1 = new Collection<SystemUser>();
            this.SystemUsers = new Collection<SystemUser>();
        }
    
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobi { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public bool IsRequirePwdChange { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LockedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> InvitatedBy { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Collection<SystemRoleUser> SystemRoleUsers { get; set; }
        public virtual Collection<SystemUser> SystemUser1 { get; set; }
        public virtual Collection<SystemUser> SystemUsers { get; set; }
    }
    
}
