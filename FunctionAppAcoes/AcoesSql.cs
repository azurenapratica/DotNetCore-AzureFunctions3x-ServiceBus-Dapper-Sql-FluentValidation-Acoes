using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FunctionAppAcoes.Data;

namespace FunctionAppAcoes
{
    public static class AcoesSql
    {
        [FunctionName("AcoesSql")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var listaAcoes = AcoesRepository.GetAll(); 
            log.LogInformation(
                $"AcoesMongo HTTP trigger - número atual de lançamentos: {listaAcoes.Count()}");
            return new OkObjectResult(AcoesRepository.GetAll());
        }
    }
}