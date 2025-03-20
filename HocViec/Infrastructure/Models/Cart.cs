using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Cart : BaseModel
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }

    }
}
