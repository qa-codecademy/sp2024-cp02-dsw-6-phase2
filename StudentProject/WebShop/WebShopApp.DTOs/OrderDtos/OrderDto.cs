using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;

namespace WebShopApp.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int Id {  get; set; }    
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }
        public Userr User { get; set; } // Navigation property
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
