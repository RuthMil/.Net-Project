using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public long HostKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankAccount BankAccountDetails { get; set; }
        public string CollectionClearance { get; set; }
        public override string ToString()
        {
            return "Host ID: " + HostKey.ToString() + " Name: " + FirstName + ' ' + LastName +
                " Phone Number: " + PhoneNumber.ToString() + " Mail Address: " + MailAddress +
                "Bank Account Details: " + BankAccountDetails.ToString() + " Collection Clearance: " +
                CollectionClearance;
        }
    }
}
