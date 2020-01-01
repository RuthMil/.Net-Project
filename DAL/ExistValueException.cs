using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Serializable]
    class ExistValueException : Exception
    {
        public ExistValueException(string message) : base(message)
        {
        }

        public override string Message => base.Message;
    }
}
