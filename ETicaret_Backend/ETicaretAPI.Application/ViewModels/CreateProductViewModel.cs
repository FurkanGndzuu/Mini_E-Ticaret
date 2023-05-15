using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.ViewModels
{
    public class CreateProductViewModel
    {
        public string name { get; set; }
        public int stock { get; set; }
        public long price { get; set; }
    }
}
