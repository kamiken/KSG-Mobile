
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MediaImage
    {
        public MediaImage()
        {
            this.ProductImages = new Collection<ProductImage>();
        }
    
        public long ImageID { get; set; }
        public byte[] ImageBinary { get; set; }
        public string MimeType { get; set; }
        public string SeoFileName { get; set; }
    
        public virtual Collection<ProductImage> ProductImages { get; set; }
    }
    
}
