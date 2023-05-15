using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class userNullException : Exception
    {
        public userNullException() : base("User Name Or Password Is Incorrect")
        {
                
        }
    }
}
