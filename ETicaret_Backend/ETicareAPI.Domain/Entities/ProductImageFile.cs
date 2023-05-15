using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities
{
    public class ProductImageFile : File
    {
        public string path { get; set; }
        public string fileName { get; set; }
        public ICollection<Product> Products { get; set; }

        public bool? showCase { get; set; }
    }
}
