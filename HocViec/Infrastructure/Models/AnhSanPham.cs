﻿using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AnhSanPham : BaseModel
    {
        public string? ImageUrl { get; set; }
        public Guid SanPhamId { get; set; }
        public virtual SanPham? SanPham { get; set; }
    }
}
