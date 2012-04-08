
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemConfig
    {
        public string ConfigID { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public string LogNote { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
    
}
