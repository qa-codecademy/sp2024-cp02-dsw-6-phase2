using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.Cart
{
    public class CartDto
    {
        public int UserId { get; set; }
        public List<CartItem> CartItems;
        public decimal TotalPrice { get; set; }
    }
}
