using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShopApp.Domain.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CartId { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
       

        public Productt Product { get; set; }

        public int Quantity { get; set; }
    }
}
