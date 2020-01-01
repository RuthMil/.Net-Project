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
        public StatusChangeException(string message) : base(message)
        {
        }

        public override string Message => base.Message;
    }
}
