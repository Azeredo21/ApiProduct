using System.ComponentModel.DataAnnotations;

namespace Api_Teste0002.Models
{
    public class Category{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}