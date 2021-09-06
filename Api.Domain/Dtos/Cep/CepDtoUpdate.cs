using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {
        [Required(ErrorMessage = "Id é obrigaotrio")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cep é obrigaotrio")]
        [MaxLength(10, ErrorMessage = "O tamanho maximo para o Cep é {1}")]
        public string Cep { get; set; }


        [Required(ErrorMessage = "Logradouro é obrigaotrio")]
        [MaxLength(60, ErrorMessage = "O tamanho maximo para o Logradouro é {1}")]
        public string Logradouro { get; set; }


        [Required(ErrorMessage = "Numero é obrigaotrio")]
        [MaxLength(60, ErrorMessage = "O tamanho maximo para o numero é {1}")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Municipio é obrigaotrio")]
        public Guid MunicipioId { get; set; }
    }
}
