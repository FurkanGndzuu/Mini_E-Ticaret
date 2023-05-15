using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities.IdentityEntities
{
    public class AppUser : IdentityUser<string>
    {
        public string nameSurname { get; set; }
        public string? refreshToken { get; set; }
        public DateTime? refreshTokenEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
