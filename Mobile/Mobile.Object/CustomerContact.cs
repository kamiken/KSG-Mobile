
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class CustomerContact
    {
        public long ContactId { get; set; }
        public Nullable<long> CustomerId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
    
}
