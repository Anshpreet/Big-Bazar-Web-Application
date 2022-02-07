using System;
using System.Collections.Generic;
using System.Text;

namespace CustomException
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException(string message): base(message)
        {

        }
    }
}
