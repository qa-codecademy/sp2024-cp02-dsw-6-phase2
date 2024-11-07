using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.Cart;

namespace WebShopApp.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                UserId = cart.UserId,
               CartItems = cart.CartItems,
               TotalPrice= cart.TotalAmount,
               Id = cart.Id,
            };

        }

        public static Cart ToCart(this CartDto cartDto)
        {
            return new Cart
            {
                UserId= cartDto.UserId,
                CartItems= cartDto.CartItems,
                Id = cartDto.Id,
                TotalAmount= cartDto.TotalPrice
            };
        }
    }
}
