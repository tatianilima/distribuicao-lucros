using System.Collections.Generic;

namespace Distribuicao.Lucros.Domain.Dtos.Response
{
    public static class Response
    {
        public static DistribuicaoResponse Gerar()
        {
            List<ParticipacoesResponse> participacoes = new List<ParticipacoesResponse>();
            participacoes.Add(new ParticipacoesResponse
            {
                matricula = "",
                nome = "",
                valor_da_participacao = 0.00
            });

            return new DistribuicaoResponse()
            {
                Participacoes = participacoes,
                total_de_funcionarios = 0,
                total_distribuido = 0,
                total_disponibilizado = 0.00,
                saldo_total_disponibilizado = 0.00
            };
        }
    }
}
