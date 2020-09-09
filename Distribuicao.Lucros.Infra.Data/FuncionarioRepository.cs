using Dapper;
using Distribuicao.Lucros.Domain.Entities;
using Distribuicao.Lucros.Domain.Interfaces.Repositories;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Distribuicao.Lucros.Infra.Data
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _conn;

        public FuncionarioRepository(string conn)
        {
            _conn = conn;
        }

        public IEnumerable<Funcionario> ObterFuncionarios()
        {
            string query = @"
                select 
	                matricula,
                    nome,
                    area,
                    cargo,
                    salario_bruto,
                    data_de_admissao
                from funcionarios;";

            using (var mysqlConnection = new MySqlConnection(_conn))
            {
                return mysqlConnection.Query<Funcionario>(query);
            }
        }
    }
}
