using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShopApp.Domain.Models
{
    public class Orderr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }

        // Foreign Key for User
        public int UserId { get; set; }
        [JsonIgnore]
        public Userr User { get; set; } // Navigation property
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }



    }
}
