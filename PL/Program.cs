using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.IBL bl = BL.BlFactory.GetBL();
            List<HostingUnit> myUnits = bl.FreeHostingUnitsByDates_List(new DateTime(2000,07,5), 5);
            Console.WriteLine(myUnits);
        }
    }
}
