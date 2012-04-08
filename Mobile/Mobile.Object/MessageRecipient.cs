
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MessageRecipient
    {
        public long MessageRecipientID { get; set; }
        public int RecipientUser { get; set; }
        public long MessageID { get; set; }
        public int MessageFolderID { get; set; }
        public bool IsReaded { get; set; }
    }
    
}
