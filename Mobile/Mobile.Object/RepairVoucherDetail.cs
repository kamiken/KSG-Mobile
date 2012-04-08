
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class RepairVoucherDetail
    {
        public long RepairVoucherDetailId { get; set; }
        public long RepairVoucherId { get; set; }
        public long DeviceId { get; set; }
        public Nullable<bool> IsWarranty { get; set; }
        public string Serial { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Description { get; set; }
    }
    
}
