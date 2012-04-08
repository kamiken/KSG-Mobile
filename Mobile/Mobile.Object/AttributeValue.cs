
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class AttributeValue
    {
        public int AttributeValueID { get; set; }
        public Nullable<int> AttributeID { get; set; }
        public string AttributeValue1 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string LogNote { get; set; }
        public bool IsDeleted { get; set; }
    }
    
}
