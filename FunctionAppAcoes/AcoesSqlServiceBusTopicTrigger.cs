using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FunctionAppAcoes.Models;
using FunctionAppAcoes.Validators;
using FunctionAppAcoes.Data;

namespace FunctionAppAcoes
{
    public static class AcoesSqlServiceBusTopicTrigger
    {
        [FunctionName("AcoesSqlServiceBusTopicTrigger")]
        public static void Run([ServiceBusTrigger("topic-acoes", "sqlserver", Connection = "AzureServiceBus")]string mySbMsg, ILogger log)
        {
            log.LogInformation($"AcoesSqlServiceBusTopicTrigger - Dados: {mySbMsg}");

            DadosAcao dadosAcao = null;
            try
            {
                dadosAcao = JsonSerializer.Deserialize<DadosAcao>(mySbMsg,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                log.LogError("AcoesSqlServiceBusTopicTrigger - Erro durante a deserializacao!");
            }

            if (dadosAcao != null)
            {
                var validationResult = new AcaoValidator().Validate(dadosAcao);
                if (validationResult.IsValid)
                {
                    log.LogInformation($"AcoesSqlServiceBusTopicTrigger - Dados pos formatacao: {JsonSerializer.Serialize(dadosAcao)}");
                    AcoesRepository.Save(dadosAcao);
                    log.LogInformation("AcoesSqlServiceBusTopicTrigger - Acao registrada com sucesso!");
                }
                else
                {
                    log.LogError("AcoesSqlServiceBusTopicTrigger - Dados invalidos para a Acao");
                    foreach (var error in validationResult.Errors)
                        log.LogError($"AcoesSqlServiceBusTopicTrigger - {error.ErrorMessage}");
                }
            }
        }
    }
}