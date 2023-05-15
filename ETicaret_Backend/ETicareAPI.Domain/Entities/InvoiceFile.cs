using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities
{
    public class InvoiceFile : File
    {
        public int Price { get; set; }
    }
}
