
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class ProductImage
    {
        public long ID { get; set; }
        public long ImageID { get; set; }
        public long ProductID { get; set; }
    
        public virtual MediaImage MediaImage { get; set; }
        public virtual Product Product { get; set; }
    }
    
}
