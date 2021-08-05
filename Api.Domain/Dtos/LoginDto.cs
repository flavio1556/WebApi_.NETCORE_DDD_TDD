using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é um campo obrigatorio pra Login.")]
        [EmailAddress(ErrorMessage = "Email em formato Invalido.")]
        [StringLength(100, ErrorMessage = "Email deve ter no maximo {1} Caracteres.")]
        public string Email { get; set; }
    }
}
