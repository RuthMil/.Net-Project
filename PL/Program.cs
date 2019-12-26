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
            int[] numbers = { 5, 4, 12, 33, 91, 8, 6, 7, 22, 0 };
            IEnumerable<IGrouping<int, int>> GroupHostingUnitByAreas()
            {
                var nums = from num in numbers
                                   group num by num%2;
                return nums;
            }
            List<IGrouping<int,int>> nn = GroupHostingUnitByAreas().ToList();
            foreach (var item in nn)
                Console.WriteLine(item);
        }    
    }
}
