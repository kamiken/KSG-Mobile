
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class CompanyProfile
    {
        public int CompanyID { get; set; }
        public int PropertyID { get; set; }
        public string PropertyValue { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual CompanyProfileProperty CompanyProfileProperty { get; set; }
    }
    
}
