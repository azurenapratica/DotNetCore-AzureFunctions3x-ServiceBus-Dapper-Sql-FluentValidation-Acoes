using System;
using System.Collections.Generic;
using FunctionAppAcoes.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FunctionAppAcoes.Data
{
    public static class AcoesRepository
    {
        public static void Save(DadosAcao dadosAcao)
        {
            using (var conexao = new SqlConnection(
                Environment.GetEnvironmentVariable("BaseAcoes")))
            {
                var acao = new Acao();
                acao.Codigo = dadosAcao.Codigo;
                acao.DataReferencia = DateTime.Now;
                acao.Valor = dadosAcao.Valor;
                conexao.Insert(acao);
            }
        }

        public static IEnumerable<Acao> GetAll()
        {
            using (var conexao = new SqlConnection(
                Environment.GetEnvironmentVariable("BaseAcoes")))
            {
                return conexao.Query<Acao>(
                    "SELECT * FROM dbo.Acoes " +
                    "ORDER BY Id Desc");
            }
        }
    }
}