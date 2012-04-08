
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MediaImageCategory
    {
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> ParentCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> CreateedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public int CompanyID { get; set; }
    }
    
}
