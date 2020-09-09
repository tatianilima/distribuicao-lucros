using Distribuicao.Lucros.Domain.Entities;
using System.Collections.Generic;

namespace Distribuicao.Lucros.Domain.Interfaces.Repositories
{
    public interface IFuncionarioRepository
    {
        IEnumerable<Funcionario> ObterFuncionarios();
    }
}
