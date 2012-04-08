
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class ArticleComment
    {
        public long CommentID { get; set; }
        public Nullable<long> ArticleID { get; set; }
        public string FullName { get; set; }
        public string SenderEmail { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string SenderCompany { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public int CompanyID { get; set; }
    
        public virtual Article Article { get; set; }
    }
    
}
