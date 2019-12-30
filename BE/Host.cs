using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Host
    {
        public long HostKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public long BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public int Commission { get; set; }
        public override string ToString()
        {
            return "Host ID: " + HostKey.ToString() + " Name: " + FirstName + ' ' + LastName +
                " Phone Number: " + PhoneNumber + " Mail Address: " + MailAddress +
                " Bank Account Details: " + BankBranchDetails.ToString() + "Bank Account Number: " + BankAccountNumber.ToString() + " Collection Clearance: " +
                CollectionClearance;
        }
    }
}
