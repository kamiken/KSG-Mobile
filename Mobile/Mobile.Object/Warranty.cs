
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Warranty
    {
        public long WarrantyId { get; set; }
        public string Serial { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<System.DateTime> WarrantyDate { get; set; }
        public Nullable<long> OutputVoucherId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int WarrantyCount { get; set; }
    }
    
}
