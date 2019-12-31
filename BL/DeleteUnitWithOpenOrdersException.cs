using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    [Serializable]
    class DeleteUnitWithOpenOrdersException : Exception
    {
        public new string Message;
        public DeleteUnitWithOpenOrdersException(string tmp)
        {
            Message = tmp;
        }
    }
}
