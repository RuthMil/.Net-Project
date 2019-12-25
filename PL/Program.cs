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
            IEnumerable<IGrouping<int, int>> GroupHostByNumberOfHostingUnit()
            {
                var groupByNumberOfHostingUnit = from num in numbers
                                                 group num by num%2 into groupByodd
                                                 group groupByodd.Key by groupByodd.Count() into groupByCount
                                                 select groupByCount;
                return groupByNumberOfHostingUnit;
            }
            

            IEnumerable<IGrouping<int, int>> result = from n in numbers
                                                      group n by n % 2;

            foreach (IGrouping<int, int> g in result)
            {
                switch (g.Key)
                {
                    case 1:
                        Console.WriteLine("Odd Number:");
                        foreach (int n in g)
                            Console.Write("{0}, ", n);
                        Console.WriteLine("\n");
                        break;

                    case 0:
                        Console.WriteLine("Even Number:");
                        foreach (int n in g)
                            Console.Write("{0}, ", n);
                        Console.WriteLine("\n");
                        break;
                }
            }
            var mygr = GroupHostByNumberOfHostingUnit();
            foreach (var item in mygr)
            {
                Console.WriteLine(item.Key);
                foreach (var y in item)
                    Console.WriteLine(y);
            }

    }
}
}
