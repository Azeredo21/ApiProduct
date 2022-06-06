using System.ComponentModel.DataAnnotations;

namespace Api_Teste0002.Models
{
    public class Category{
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "O nome da categoria deve ter no máximo 60 caracteres")] 
        [MinLength(3, ErrorMessage = "O nome do categoria deve ter no mínimo 60 caracteres")] 
        public string Name { get; set; }
    }
}