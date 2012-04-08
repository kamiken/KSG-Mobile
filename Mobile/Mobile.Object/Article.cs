
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Article
    {
        public Article()
        {
            this.ArticleComments = new Collection<ArticleComment>();
        }
    
        public long ArticleID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string Title { get; set; }
        public string SEOURL { get; set; }
        public string Abstract { get; set; }
        public string Abstract_En { get; set; }
        public string Body { get; set; }
        public string Keyword { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> IsActived { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public string LogNote { get; set; }
        public int TotalViews { get; set; }
        public int TotalLikes { get; set; }
    
        public virtual Collection<ArticleComment> ArticleComments { get; set; }
    }
    
}
