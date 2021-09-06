using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdate
    {
        [Required(ErrorMessage = "Id é um campo obrigatório")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome de município é obrigatorio ")]
        [MaxLength(45, ErrorMessage = "O tamanho maximo para nome é  de {1} caracteres")]
        public string Nome { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Codigo do Ibge Invalido")]
        public int CodIbge { get; set; }
        [Required(ErrorMessage = "Codigo de Uf é campo obrigatório")]
        public Guid ufId { get; set; }
    }
}
