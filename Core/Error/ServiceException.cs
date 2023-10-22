using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistLab2.Core.Error
{
   public class ServiceException : Exception
    {
        public ServiceException() : base() { }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception error) : base(message, error) { }
    }
}