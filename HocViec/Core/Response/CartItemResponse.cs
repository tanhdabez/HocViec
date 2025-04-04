using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Response
{
    public class CartItemResponse
    {
        public bool Success { get; set; }
        public bool Warning { get; set; }
        public required string Message { get; set; }
        public int CartItemCount { get; set; }
    }
}
