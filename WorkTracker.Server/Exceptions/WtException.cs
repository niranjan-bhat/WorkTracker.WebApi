using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Server.Exceptions
{
    public class WtException : Exception
    {
        public readonly string ErrorCode;
        private WtException()
        {
        }

        public WtException(string message, string errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
