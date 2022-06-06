using System.ComponentModel.DataAnnotations;

namespace Api_Teste0002.Models
{
    public class Product{
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome do produto deve ter no máximo 60 caracteres")] 
        [MinLength(3, ErrorMessage = "O nome do produto deve ter no mínimo 60 caracteres")] 
        public string Name { get; set; }

        [MaxLength(1024, ErrorMessage = "A descrição do produto deve ter no máximo 1024 caracteres")] 
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}