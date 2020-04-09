using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email e campo obrigatorios para Login")]
        [EmailAddress(ErrorMessage = "Email em formato invalido")]
        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} caracteres")]
        public string Email { get; set; }
    }
}
