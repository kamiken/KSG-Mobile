
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Company
    {
        public Company()
        {
            this.CompanyOnlineSupports = new Collection<CompanyOnlineSupport>();
            this.Manufacturers = new Collection<Manufacturer>();
            this.MediaCategories = new Collection<MediaCategory>();
            this.MediaFiles = new Collection<MediaFile>();
            this.SystemCompanyConfigs = new Collection<SystemCompanyConfig>();
            this.SystemUsers = new Collection<SystemUser>();
        }
    
        public int CompanyID { get; set; }
        public Nullable<bool> IsActived { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string TaxCode { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string CompanyCode { get; set; }
        public string Key { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public Nullable<int> ParentID { get; set; }
        public bool IsRootSite { get; set; }
    
        public virtual Collection<CompanyOnlineSupport> CompanyOnlineSupports { get; set; }
        public virtual CompanyProfile CompanyProfile { get; set; }
        public virtual Collection<Manufacturer> Manufacturers { get; set; }
        public virtual Collection<MediaCategory> MediaCategories { get; set; }
        public virtual Collection<MediaFile> MediaFiles { get; set; }
        public virtual Collection<SystemCompanyConfig> SystemCompanyConfigs { get; set; }
        public virtual Collection<SystemUser> SystemUsers { get; set; }
    }
    
}
