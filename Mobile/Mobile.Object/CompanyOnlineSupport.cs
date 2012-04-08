
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class CompanyOnlineSupport
    {
        public long OnlineSupportID { get; set; }
        public int CompanyID { get; set; }
        public string Nick { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActived { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public int OnlineSupportTypeID { get; set; }
        public int OrderIndex { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual OnlineSupportType OnlineSupportType { get; set; }
    }
    
}
