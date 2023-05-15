using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<EndPoint> EndPoints { get; set; }
        public Menu()
        {
            EndPoints = new List<EndPoint>();
        }
    }
}
