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
        public Enum_s.GuestRequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Enum_s.Areas Area { get; set; }
        public Enum_s.SubArea SubArea { get; set; }
        public Enum_s.HostingUnitTypes Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Enum_s.RequestOption Pool { get; set; }
        public Enum_s.RequestOption Jacuzzi { get; set; }
        public Enum_s.RequestOption Garden { get; set; }
        public Enum_s.RequestOption ChildrenAttractions { get; set; }
        public override string ToString()
        {
            string isSubArea = SubArea != default(Enum_s.SubArea) ? "Specific Area: " + SubArea.ToString("g") : ""; 
            return "Hosting Request ID: " + GuestRequestKey.ToString() + "   Name: "
                + FirstName + ' ' + LastName + "   Mail Address: " + MailAddress + 
                "\nOrder Status: " + Status.ToString("g") + " Application Date: " + RegistrationDate.ToString() +
                "Check In Date: " + EntryDate.ToString() + " Check Out Date: " + ReleaseDate.ToString() +
                "\nArea: " + Area.ToString("g") + isSubArea + " Hosting Unit Type: " + Type + "\nYour Group: Adults- " + 
                Adults.ToString() + " Children- " + Children.ToString() +" Special Additions: Pool- " + Pool.ToString("g") +
                " Jucuzzi- " + Jacuzzi.ToString("g") + " Garden- " + Garden.ToString("g") + " Attractions For Children- " + 
                ChildrenAttractions.ToString("g") +'\n';  
        }
    }
}
