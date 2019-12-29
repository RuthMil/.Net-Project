using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class BankBranch 
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public override string ToString()
        {
            return "Bank Number: " + BankNumber.ToString() + " Bank Name: " + BankName + " Branch Number: " + BranchNumber.ToString() +
                " Branch Address: " + BranchAddress + ", " + BranchCity + '\n';
        }
    }
}
