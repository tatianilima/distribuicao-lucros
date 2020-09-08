using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Dtos.Response;

namespace Distribuicao.Lucros.Domain.Interfaces.Services
{
    public interface IDistribuicaoService
    {
        DistribuicaoResponse Calcular(DistribuicaoRequest data);
    }
}
