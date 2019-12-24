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
            List<int> b = new List<int>{ 3, 5, 6, 4, 7 };
            var ordersForGuest = from order in b
                                 where order >= 6
                                 select new { };
            Console.WriteLine(ordersForGuest.Count()); 
        }
    }
}
