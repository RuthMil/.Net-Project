using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        public static IDAL GetDAL()
        {
            return Dal_XML_imp.Instance;  
        }
    }
}
