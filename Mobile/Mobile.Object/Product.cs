
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Product
    {
        public Product()
        {
            this.ProductImages = new Collection<ProductImage>();
        }
    
        public long ProductID { get; set; }
        public string ProductCode { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string SEOURL { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductName_En { get; set; }
        public string Description_En { get; set; }
        public bool IsDeleted { get; set; }
        public long OrderIndex { get; set; }
        public int TotalViews { get; set; }
        public int TotalLikes { get; set; }
        public Nullable<int> ManufacturerID { get; set; }
        public bool IsInHomepage { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public string LogNote { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Collection<ProductImage> ProductImages { get; set; }
    }
    
}
