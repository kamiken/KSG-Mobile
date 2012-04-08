
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Advertising
    {
        public int AdvertisingID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Baner { get; set; }
        public Nullable<int> OrderIndex { get; set; }
        public int CategoryID { get; set; }
        public Nullable<bool> IsActived { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Content { get; set; }
        public Nullable<int> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public int CompanyID { get; set; }
    }
    
}
