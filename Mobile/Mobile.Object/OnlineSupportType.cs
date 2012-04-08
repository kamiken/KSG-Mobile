
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class OnlineSupportType
    {
        public OnlineSupportType()
        {
            this.CompanyOnlineSupports = new Collection<CompanyOnlineSupport>();
        }
    
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Collection<CompanyOnlineSupport> CompanyOnlineSupports { get; set; }
    }
    
}
