using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Enum_s
    {
        public enum HostingUnitTypes {Hut, Apartment, Hotel, Tent}
        public enum Areas {North, South, Central, Jerusalem}
        public enum OrderStatus {HasNotBeenTreated , MailSended, ClosedDueToUnresponsiveness, ClosedDueToResponsiveness, ClosedDueToClash }
        public enum GuestRequestStatus {Open, ClosedDueToApp, ClosedDueToExpiry }
        //טיפוס בשביל תת אזורים
    }
}
