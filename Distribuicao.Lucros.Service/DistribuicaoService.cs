using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Dtos.Response;
using Distribuicao.Lucros.Domain.Interfaces.Services;

namespace Distribuicao.Lucros.Service
{
    public class DistribuicaoService : IDistribuicaoService
    {
        public DistribuicaoResponse Calcular(DistribuicaoRequest data)
        {
            //obter dados dos funcionarios a partir do banco de dados
            return Response.Gerar();
        }
    }
}
