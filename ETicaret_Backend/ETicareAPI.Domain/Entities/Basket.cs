using ETicareAPI.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicareAPI.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public string userId { get; set; }
        public AppUser User { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
        public bool IsFinishedOrder { get; set; }
        public Order? Order { get; set; }
    }
}
