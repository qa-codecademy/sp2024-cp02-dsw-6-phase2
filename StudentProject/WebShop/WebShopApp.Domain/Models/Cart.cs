using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebShopApp.Domain.Models
{
    public class Cart
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public Userr User { get; set; }

        // A collection of CartItems which will track products and their quantities
        public List<CartItem> CartItems { get; set; }

        public decimal TotalAmount { get; set; }

    }

}

