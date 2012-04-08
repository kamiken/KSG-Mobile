
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            this.Products = new Collection<Product>();
        }
    
        public int CategoryID { get; set; }
        public Nullable<int> RefCategoryID { get; set; }
        public Nullable<int> ParentCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_En { get; set; }
        public string Description { get; set; }
        public string Description_En { get; set; }
        public string ImageUrl { get; set; }
        public string SEOURL { get; set; }
        public int OrderIndex { get; set; }
        public bool IsActived { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string LogNote { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Collection<Product> Products { get; set; }
    }
    
}
