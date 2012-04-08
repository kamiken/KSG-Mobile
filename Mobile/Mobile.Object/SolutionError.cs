
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class SolutionError
    {
        public long ID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public int ErrorID { get; set; }
        public int SolutionID { get; set; }
    }
    
}
