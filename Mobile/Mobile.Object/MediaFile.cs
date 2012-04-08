
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MediaFile
    {
        public long FileID { get; set; }
        public int CompanyID { get; set; }
        public string FileUrl { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public int FileTypeID { get; set; }
        public int CategoryID { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual MediaCategory MediaCategory { get; set; }
    }
    
}
