using System.ComponentModel.DataAnnotations;

namespace StoredProceduresCoreMVCB1.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }

        [Required(ErrorMessage ="Enter Product Name")]
        public string? Pname { get; set; }

        [Required(ErrorMessage = "Enter Product Category")]
        public string? Pcat { get; set; }

        [Required(ErrorMessage = "Enter Product Price")]
        public double Price { get; set; }
    }
}
