using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopApp.Domain.Models;
using WebShopApp.DTOs.OrderDtos;

namespace WebShopApp.Mappers
{
    public static class OrderMapper
    {
        public static Orderr ToOrder(this AddOrderDto addOrderDto)
        {
            return new Orderr
            {
                OrderDate = DateTime.Now,
                TotalAmount = addOrderDto.TotalAmount,  
                OrderItems = addOrderDto.OrderItems,
               
            };
        }

        public static AddOrderDto ToAddOrderDto(this Orderr order)
        {
            return new AddOrderDto
            {
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems,
            };
        }

        public static OrderDto ToOrderDto (this Orderr order)
        {
            return new OrderDto
            {
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems,
                UserId = order.UserId,
                User = order.User,

                
            };
        }

        public static Orderr ToOrderOrig(this OrderDto orderDto)
        {
            return new Orderr
            {
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.OrderItems,
                User = orderDto.User,
                UserId= orderDto.UserId,


            };

        }
    }
}
