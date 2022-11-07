using System.Collections.Generic;

namespace Consulta.Func.Ofac.Dominio.Notificador.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
