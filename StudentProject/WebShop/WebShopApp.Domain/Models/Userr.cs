﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopApp.Domain.Enums.UserEnum;

namespace WebShopApp.Domain.Models
{
    public class Userr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }


        public List<Orderr> Orders { get; set; }
        public List<Productt> Products { get; set; }
        public Cart Cart { get; set; } 


    }
}
