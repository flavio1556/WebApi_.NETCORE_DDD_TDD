using System;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdateResult
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodIbge { get; set; }
        public Guid ufId { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
