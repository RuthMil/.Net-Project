using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Enum_s
    {
        public enum HostingUnitTypes {הכל, בקתה, דירה, מלון, קמפינג, וילה}
        public enum Areas {צפון, דרום, מרכז, ירושלים}
        public enum OrderStatus {HasNotBeenTreated , MailSended, ClosedDueToUnresponsiveness,
            ClosedDueToResponsiveness, ClosedDueToClash, ClosedDueToOtherPurchase, ClosedDueToExpired }
        public enum GuestRequestStatus {Open, ClosedDueToResponsivenessByApp, ClosedDueToExpiry }
        public enum RequestOption { אפשרי, כן, לא }
        public enum SubArea { חיפה, תל_אביב, גליל, באר_שבע, אילת, ירושלים, צפון, דרום, מרכז }
    }
}
