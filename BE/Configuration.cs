using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static long GuestRequestID = 0;
        public static long HostID = 0;
        public static long HostingUnitID = 0;
        public static long OrderID = 0;
        public static int commission = 10;
        public static int orderValidTime = 30;
        public static int orderCancelationDays = 14;
    }
}
