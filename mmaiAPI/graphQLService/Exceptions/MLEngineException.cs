using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.Exceptions
{
    public class MLEngineException : Exception
    {
        public MLEngineException() : base() { }
        public MLEngineException(string? message) : base(message) { }
        public MLEngineException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
