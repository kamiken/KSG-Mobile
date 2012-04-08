
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class InvitationFriend
    {
        public long InvitationID { get; set; }
        public Nullable<int> FromUser { get; set; }
        public Nullable<int> ToUser { get; set; }
    }
    
}
