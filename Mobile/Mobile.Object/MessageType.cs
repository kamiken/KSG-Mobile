
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class MessageType
    {
        public MessageType()
        {
            this.Messages = new Collection<Message>();
        }
    
        public int MessageType1 { get; set; }
        public string Name { get; set; }
    
        public virtual Collection<Message> Messages { get; set; }
    }
    
}
