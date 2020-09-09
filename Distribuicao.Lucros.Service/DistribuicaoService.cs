using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Dtos.Response;
using Distribuicao.Lucros.Domain.Entities;
using Distribuicao.Lucros.Domain.Interfaces.Repositories;
using Distribuicao.Lucros.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Distribuicao.Lucros.Service
{
    public class DistribuicaoService : IDistribuicaoService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        const double salarioMinimo = 1045.00;

        public DistribuicaoService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public DistribuicaoResponse ObterBonus(DistribuicaoRequest data)
        {
            var funcionarios = _funcionarioRepository.ObterFuncionarios().ToList();
            var participacoes = new List<ParticipacoesResponse>();
            double bonusTotal = 0;
            foreach (var funcionario in funcionarios)
            {
                bonusTotal += CalcularBonusIndividual(funcionario);
                participacoes.Add(new ParticipacoesResponse
                { 
                    matricula = funcionario.matricula,
                    nome = funcionario.nome,
                    valor_da_participacao = CalcularBonusIndividual(funcionario)
                });
            }

            return new DistribuicaoResponse()
            {
                Participacoes = participacoes,
                total_de_funcionarios = funcionarios.Count,
                total_distribuido = bonusTotal,
                total_disponibilizado = data.valor_maximo_distribuicao,
                saldo_total_disponibilizado = data.valor_maximo_distribuicao - bonusTotal
            };
        }

        private double CalcularBonusIndividual(Funcionario funcionario)
        {
            var todasAreas = ObterAreasAtuacao();
            double sb = funcionario.salario_bruto;

            int pta = ObterPesoPorTempoAdmissao(funcionario.data_de_admissao);
            int paa = ObterPesoPorArea(todasAreas, funcionario.cargo);
            int pfs = ObterPesoPorSalario(sb, funcionario.cargo);

            return Math.Round((((sb * pta) + (sb * paa)) / pfs) * 12, 2);            
        }

        private Dictionary<string, int> ObterAreasAtuacao()
        {
            Dictionary<string, int> areas = new Dictionary<string, int>();
            areas.Add("Diretoria", 1);
            areas.Add("Contabilidade", 2);
            areas.Add("Financeiro", 2);
            areas.Add("Tecnologia", 2);
            areas.Add("Serviços Gerais", 3);
            areas.Add("Relacionamento com o Cliente", 5);

            return areas;
        }

        private int ObterPesoPorSalario(double salarioBruto, string cargo)
        {
            int peso = 0;
            if (cargo=="Estagiário" || salarioBruto <= (salarioMinimo*3))
            {
                peso = 1;
            }else if (salarioBruto > (salarioMinimo*3) && salarioBruto < (salarioMinimo*5))
            {
                peso = 2;
            }
            else if (salarioBruto > (salarioMinimo * 5) && salarioBruto < (salarioMinimo * 8))
            {
                peso = 3;
            }
            else if(salarioBruto > (salarioMinimo*8))
            {
                peso = 5;
            }

            return peso;
        }

        private int ObterPesoPorArea(Dictionary<string, int> todasAreas,string areaAtuacao) 
        {
           return todasAreas.FirstOrDefault(m => m.Key == areaAtuacao).Value; 
        }

        private int ObterPesoPorTempoAdmissao(DateTime dataAdmissao) 
        {
            int tempoDeCasa = ObterTempoAdmissao(dataAdmissao);
            int peso = 0;
            if(tempoDeCasa <= 1)
            {
                peso = 1;
            }
            else if(tempoDeCasa > 1 && tempoDeCasa < 3)
            {
                peso = 2;
            }
            else if(tempoDeCasa > 3 && tempoDeCasa< 8)
            {
                peso = 3;
            }else if (tempoDeCasa > 8)
            {
                peso = 5;
            }
            return peso;
        }

        private int ObterTempoAdmissao(DateTime dataAdmissao)
        {
            var anoCorrente = DateTime.Now.Year;
            int tempoDeCasa = anoCorrente - dataAdmissao.Year;

            return tempoDeCasa;
        }
    }
}
