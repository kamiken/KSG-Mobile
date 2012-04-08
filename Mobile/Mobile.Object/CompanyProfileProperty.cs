
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class CompanyProfileProperty
    {
        public CompanyProfileProperty()
        {
            this.CompanyProfiles = new Collection<CompanyProfile>();
        }
    
        public int PropertyID { get; set; }
        public string PropertyName { get; set; }
        public int OrderIndex { get; set; }
    
        public virtual Collection<CompanyProfile> CompanyProfiles { get; set; }
    }
    
}
