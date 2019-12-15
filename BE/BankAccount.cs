using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankAccount 
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int brunchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public long BankAccountNumber { get; set; }
        public override string ToString()
        {
            return "Bank Number: " + BankNumber.ToString() + " Bank Name: " + BankName + " Branch Number: " + brunchNumber.ToString() +
                " Branch Address: " + BranchAddress + ", " + BranchCity + " Account Number: " + BankAccountNumber.ToString() + '\n';
        }
    }
}
