using WebShopApp.Domain.Models;
using WebShopApp.Services.Interface;
using WebShopApp.DTOs.OrderDtos;
using WebShopApp.DataAccess.Interface;
using WebShopApp.Mappers;
using Microsoft.AspNetCore.Mvc;



namespace WebShopApp.Services.Implementation
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly MailjetService _mailjetService;

        public OrderServices(IOrderRepository orderRepository, IUserRepository userRepository,IProductRepository productRepository,ICartRepository cartRepository ,MailjetService mailjetService)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _mailjetService = mailjetService;
        }

        public async Task CreateOrder(int userId, AddOrderDto addOrderDto)
        {
            var userDb = _userRepository.GetById(userId);

            if (userDb == null)
            {
                throw new ArgumentException("You must be logged in to make an order");
            }
            if (addOrderDto == null)
            {
                throw new ArgumentException("No products in the order");

            }
            var ordeDb = addOrderDto.ToOrder();

            ordeDb.UserId = userDb.Id;

                
                    

              await  _orderRepository.Add(ordeDb);


        }

       

        public async Task<OrderDto> CreateOrderFromCart(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);

             Orderr order = await _orderRepository.CreateOrderFromCartAsync(cart);
            
            
            OrderDto newOrder = OrderMapper.ToOrderDto(order);
             await _cartRepository.ClearCartAsync(cartId);

            await _mailjetService.SendOrderConfirmationEmail(cart.User.Email, cart.User.Name, order);

            return (newOrder);

            
            
        }

       

        public  Task DeleteOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
             _orderRepository.Delete(order);
            return Task.CompletedTask;
            
        }

        public Orderr GetOrderById(int id)
        {
           var order = _orderRepository.GetById(id);
            return order;
        }

        public  List<OrderDto> GetOrders(bool isOrderForUser)
        {
            var orders = new List<Orderr>();
            if(isOrderForUser)
                orders =  _orderRepository.GetAll();
           
            var ordersDto = new List<OrderDto>();
            foreach (var order in orders)
            {
                var orderDto = order.ToOrderDto();
                ordersDto.Add(orderDto);

            }
            return ordersDto;



        }

        public Task RemoveProductFromOrder(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(Orderr updateOrde)
        {   
            
           return _orderRepository.Update(updateOrde);
        }

        public async Task AddProductToOrder(int orderId, int productId, int quantity)
        {
            var order = _orderRepository.GetById(orderId);
            if(order == null)
            {
                order = new Orderr();
                await _orderRepository.Add(order);

                await _orderRepository.AddProductToOrderAsync(order.Id, productId, quantity);


            }
            await _orderRepository.AddProductToOrderAsync(orderId, productId, quantity);



        }
    }
}
