using ETicareAPI.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities
{
    public class EndPoint : BaseEntity
    {
        public Menu Menu { get; set; }
        public string? ActionType { get; set; }
        public string? HttpType { get; set; }
        public string? Definition { get; set; }
        public string? Code { get; set; }

        public ICollection<AppRole> Roles { get; set; }
        public EndPoint()
        {
                Roles = new List<AppRole>();
        }
    }
}
