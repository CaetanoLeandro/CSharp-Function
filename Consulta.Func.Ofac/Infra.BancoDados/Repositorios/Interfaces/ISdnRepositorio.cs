using Consulta.Func.Ofac.Dominio.Entidades;

namespace Consulta.Func.Ofac.Infra.BancoDados.Repositorios.Interfaces
{
    public interface ISdnRepositorio
    {
        public int Adicionar(Sdn obj);
        public Task < bool> AdicionarLote(List<Sdn> sdn);
    }
}