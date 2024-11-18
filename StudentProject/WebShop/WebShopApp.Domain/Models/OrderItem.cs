using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopApp.Domain.Models
{
    public class OrderItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        public Orderr Order { get; set; }
        public int ProductId { get; set; }
        public Productt Product { get; set; }

        public int Quantity { get; set; }
       
    }
}
