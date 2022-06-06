using System.ComponentModel.DataAnnotations;

namespace Api_Teste0002.Models
{
    public class Category{
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo � obrigat�rio")]
        [MaxLength(60, ErrorMessage = "O nome da categoria deve ter no m�ximo 60 caracteres")] 
        [MinLength(3, ErrorMessage = "O nome do categoria deve ter no m�nimo 60 caracteres")] 
        public string Name { get; set; }
    }
}