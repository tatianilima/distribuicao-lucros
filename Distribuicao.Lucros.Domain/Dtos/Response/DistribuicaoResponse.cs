using System.Collections.Generic;

namespace Distribuicao.Lucros.Domain.Dtos.Response
{
    public class DistribuicaoResponse
    {
        public List<ParticipacoesResponse> Participacoes { get; set; }
        public int total_de_funcionarios { get; set; }
        public double total_distribuido { get; set; }
        public double total_disponibilizado { get; set; }
        public double saldo_total_disponibilizado { get; set; }
    }
}
