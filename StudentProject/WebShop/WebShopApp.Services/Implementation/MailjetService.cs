using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using WebShopApp.Domain.Models;
using WebShopApp.Services.Interface;
using System.Reflection;
using System.Text;



namespace WebShopApp.Services.Implementation
{
    public class MailjetService
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly IOrderService _orderService;

        public MailjetService(IConfiguration configuration)
        {
            _apiKey = configuration["MailJet:ApiKey"];
            _secretKey = configuration["MailJet:SecretKey"];
            _senderEmail = configuration["MailJet:SenderEmail"];
            _senderName = configuration["MailJet:SenderName"];
        }

        public async Task SendOrderConfirmationEmail(string recipientEmail, string recipientName, Orderr order)
        {

            MailjetClient client = new MailjetClient(_apiKey, _secretKey);

            // Create the email body with order details
            var emailBody = new StringBuilder();
            emailBody.Append($"<h3>Dear {recipientName},</h3><br />");
            emailBody.Append($"Thank you for your order! Your Order ID is <b>{order.Id}</b>.<br /><br />");
            emailBody.Append("<h4>Order Details:</h4>");
            emailBody.Append("<ul>");

            foreach (var orderItem in order.OrderItems)
            {
                emailBody.Append($"<li>{orderItem.Product.ProductName} - Quantity: {orderItem.Quantity} - Price: {orderItem.Product.Price:C}</li>");
            }

            emailBody.Append("</ul>");
            emailBody.Append($"<br /><strong>Total Amount: {order.TotalAmount:C}</strong>");

            // Create the Mailjet request
            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, _senderEmail)
            .Property(Send.FromName, _senderName)
            .Property(Send.Subject, $"Order Confirmation - Order #{order.Id}")
            .Property(Send.HtmlPart, emailBody.ToString())
            .Property(Send.TextPart, $"Dear {recipientName}, Thank you for your order! Your Order ID is {order.Id}.");

            // Set the recipient
            request.Property(Send.Recipients, new JArray {
        new JObject {
            { "Email", recipientEmail }
        }
    });

            // Send the email
            await client.PostAsync(request);
        }
    }
}





















           