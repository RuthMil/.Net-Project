using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {
        public long HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public bool[,] Diary { get; set; }
        public Enum_s.Areas Area { get; set; }
        public Enum_s.SubArea SubArea { get; set; }
        public Enum_s.HostingUnitTypes Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Garden { get; set; }
        public bool ChildrenAttractions { get; set; }
        public Enum_s.RequestOption Wifi { get; set; }
        public float Price { get; set; }
        //לעשות טוסטרינג גנרי
        public override string ToString()
        {
            string isSubArea = SubArea != default(Enum_s.SubArea) ? " Specific Area: " + SubArea.ToString("g") : "";
            return "Hosting Unit ID: " + HostingUnitKey.ToString() + " Hosting Unit Name: " + HostingUnitName + " Owner: " +
                Owner.ToString() + "\nArea: " + Area.ToString("g") + isSubArea + " Hosting Unit Type: " + Type + "\nYour Group: Adults- " +
                Adults.ToString() + " Children- " + Children.ToString() + " Special Additions: Pool- " + Pool.ToString() +
                " Jucuzzi- " + Jacuzzi.ToString() + " Garden- " + Garden.ToString() + " Attractions For Children- " +
                ChildrenAttractions.ToString() + "price for a day: " + Price.ToString() + '\n'; 
        }
    }
}
