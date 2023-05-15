using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities.IdentityEntities
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<EndPoint> EndPoints { get; set; }
        public AppRole()
        {
            EndPoints = new List<EndPoint>();
        }
    }
}
