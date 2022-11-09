using Consulta.Func.Ofac.Aplicacao.Servicos.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Consulta.Func.Ofac
{
    public class AtualizarSdn
    {
        private readonly ILogger _logger;
        private readonly ISdnLoteServico _sdnLoteServico;

        public AtualizarSdn(ILoggerFactory loggerFactory, ISdnLoteServico sdnLoteServico)
        {
            _logger = loggerFactory.CreateLogger<AtualizarSdn>();
            _sdnLoteServico = sdnLoteServico;
        }

        [Function("FunctionAtualizarListaOfac")]
        public void Run([TimerTrigger("0 */5 * * * *"

                , RunOnStartup = true

            )]
 
            MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            _sdnLoteServico.AtualizarRegistrosDaBase(true);
            _sdnLoteServico.AtualizarRegistrosDaBase(false);
        }
    }
}