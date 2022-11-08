using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consulta.Func.Ofac
{
    public class AtualizarListaOfac
    {
        private readonly ILogger _logger;
        private readonly IListaOfacSdnServico _listaOfacSdnServico;

        public AtualizarListaOfac(ILoggerFactory loggerFactory, IListaOfacSdnServico listaOfacSdnServico)
        {
            _logger = loggerFactory.CreateLogger<AtualizarListaOfac>();
            _listaOfacSdnServico = listaOfacSdnServico;
        }

        [Function("FunctionAtualizarBancoAgencia")]
        public void Run([TimerTrigger("0 */5 * * * *"

                , RunOnStartup = true

            )]
 
            MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            _listaOfacSdnServico.AtualizarRegistrosDaBase();
        }
    }
}