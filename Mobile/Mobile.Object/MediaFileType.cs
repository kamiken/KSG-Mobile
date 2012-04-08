
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MediaFileType
    {
        public int MediaTypeID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
    
}
