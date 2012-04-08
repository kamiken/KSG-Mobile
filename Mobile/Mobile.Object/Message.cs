
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Message
    {
        public long MessageID { get; set; }
        public int SendByUserID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int MessageTypeID { get; set; }
    
        public virtual MessageType MessageType { get; set; }
    }
    
}
