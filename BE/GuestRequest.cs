using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        public long GuestRequestKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Pool { get; set; }
        public string Jacuzzi { get; set; }
        public string Garden { get; set; }
        public string ChildrenAttractions { get; set; }
        public override string ToString()
        {
            string isSubArea = SubArea!=null ? "Specific Area: " + SubArea : ""; 
            return "Hosting Request ID: " + GuestRequestKey.ToString() + "   Name: "
                + FirstName + ' ' + LastName + "   Mail Address: " +MailAddress + 
                "\nOrder Status: " + Status + " Application Date: " + RegistrationDate.ToString() +
                "Check In Date: " + EntryDate.ToString() + " Check Out Date: " + ReleaseDate.ToString() +
                "\nArea: " +Area + isSubArea + " Hosting Unit Type: " + Type + "\nYour Group: Adults- " + 
                Adults.ToString() + " Children- " + Children.ToString() +" Special Additions: Pool- " + Pool +
                " Jucuzzi- " + Jacuzzi + " Garden- " +Garden + " Attractions For Children- " +ChildrenAttractions +'\n';  
        }
    }
}
