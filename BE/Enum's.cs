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
        public enum OrderStatus {לא_בטיפול , נשלח_מייל, נסגר_בשל_חוסר_הענות,
            נסגר_בשל_הענות, נסגר_בשל_התנגשות, נסגר_בשל_רכישה_אחרת, נסגר_בשל_פגות_תוקף }
        public enum GuestRequestStatus {Open, ClosedDueToResponsivenessByApp, ClosedDueToExpiry }
        public enum RequestOption { אפשרי, כן, לא }
        public enum SubArea { חיפה, תל_אביב, גליל, באר_שבע, אילת, ירושלים, צפון, דרום, מרכז }
    }
}
