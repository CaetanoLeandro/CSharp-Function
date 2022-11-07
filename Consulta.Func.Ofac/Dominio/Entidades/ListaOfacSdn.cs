using System;
using Consulta.Func.Ofac.Aplicacao.DTO;
using Consulta.Func.Ofac.Dominio.Validacao;

namespace Consulta.Func.Ofac.Dominio.Entidades
{
    public class ListaOfacSdn : EntidadeBase
    {
        public string NomeSdn { get; set; }
        public string IdSdn { get; set; }
        public string TipoSdn { get; set; }

        public List<ListaProgramDto> Program { get; set; }

        public DateTime DataAtualizacao { get; set; }
    

        public ListaOfacSdn() 
        {
        }

        public ListaOfacSdn(string nomeSdn, string idSdn, string tipoSdn, List<ListaProgramDto> program)
        {
            NomeSdn = nomeSdn;
            IdSdn = idSdn;
            TipoSdn = tipoSdn;
            Program = program;

            Validar();
        }

        public ListaOfacSdn(string nomeSdn, string idSdn, string tipoSdn, List<ListaProgramDto> program, DateTime dataAtualizacao)
        {
            NomeSdn = nomeSdn;
            IdSdn = idSdn;
            TipoSdn = tipoSdn;
            Program = program;
            DataAtualizacao = dataAtualizacao;

        }

        public ListaOfacSdn(string nomeSdn, string idSdn, string tipoSdn, string programa)
        {
            throw new NotImplementedException();
        }

        public void Validar()
        {
            ValidationResult = new ListaOfacSdnValidacao().Validate(this);
            if (!ValidationResult.IsValid)
                Notificar(ValidationResult);
        }
        
        public sealed override bool EhValido()
        {
            ValidationResult = new ListaOfacSdnValidacao().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
