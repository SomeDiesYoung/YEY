using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Service.Exceptions
{
    public class FileSaveException : Exception
    {
        public FileSaveException(string message) : base(message)
        {
        }

        public FileSaveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
