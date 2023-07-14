using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Common
{
    public static class ConnectionStringHelper
    {
        public static string Get()
        {
            string value = Environment.GetEnvironmentVariable("connectionString");
            return value;
        }
    }
}
