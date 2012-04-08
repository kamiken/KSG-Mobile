
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class RepairVoucher
    {
        public long RepairVoucherId { get; set; }
        public string RepairVoucherCode { get; set; }
        public int CompanyId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long CustomerId { get; set; }
        public int TotalDevices { get; set; }
        public string Description { get; set; }
    }
    
}
