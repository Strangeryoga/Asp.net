using System.ComponentModel.DataAnnotations;

namespace WebAPIBatch1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Pcat { get; set; }
        public double Price { get; set; }
    }
}
