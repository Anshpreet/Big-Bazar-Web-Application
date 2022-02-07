using System;
using System.Collections.Generic;
using System.Text;

namespace CustomException
{
    public class SqlException :Exception
    {
        public SqlException(string message, Exception ex):base(message, ex)
        {

        }
    }
}
