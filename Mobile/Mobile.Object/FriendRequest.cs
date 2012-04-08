
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class FriendRequest
    {
        public long RequestID { get; set; }
        public long RequestBy { get; set; }
        public int RequestUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
    
}
