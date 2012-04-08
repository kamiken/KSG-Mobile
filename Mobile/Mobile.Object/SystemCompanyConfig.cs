
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SystemCompanyConfig
    {
        public string ConfigID { get; set; }
        public int CompanyID { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Company Company { get; set; }
    }
    
}
