
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mobile.Object
{
    public partial class Contact
    {
        public int ContactID { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Address { get; set; }
        public string IPAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }
        public Nullable<System.DateTime> QuestionDate { get; set; }
        public Nullable<int> AnswerPerson { get; set; }
        public string AnswerContent { get; set; }
        public Nullable<System.DateTime> AnswerDate { get; set; }
        public Nullable<bool> IsAnswer { get; set; }
        public int CompanyID { get; set; }
    }
    
}
