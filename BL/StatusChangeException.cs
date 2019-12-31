using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    [Serializable]
    class StatusChangeException : Exception
    {
        public new string Message;

        public StatusChangeException(string tmp)
        {
            Message = tmp;
        }
    }
}
