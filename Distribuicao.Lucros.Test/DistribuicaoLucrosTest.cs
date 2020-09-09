using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Dtos.Response;
using Distribuicao.Lucros.Domain.Entities;
using Distribuicao.Lucros.Domain.Interfaces.Repositories;
using Distribuicao.Lucros.Service;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Distribuicao.Lucros.Test
{
    [TestFixture]
    class DistribuicaoLucrosTest
    {
        private readonly Mock<IFuncionarioRepository> mockFuncionarioRepository = new Mock<IFuncionarioRepository>();
        const double valorParticipacaoValid = 121883.52;
        const double saldoTotalValid = 5.98;
        const double totalDistribuidoValid = 121883.52;
        const double totalDisponibilizadoValid = 121889.5;

        private DistribuicaoService distribuicaoService;       
        private readonly Funcionario funcionario = new Funcionario()
        {
            matricula = "0009968",
            nome = "Victor Wilson",
            area = "Diretoria",
            salario_bruto = 12696.20,
            data_de_admissao = new DateTime(2012, 01, 05)
        };

        [SetUp]
        public void SetUp()
        {
            distribuicaoService = new DistribuicaoService(
                mockFuncionarioRepository.Object
                );
        }

        [Test]
        public void Success()
        {
            List<ParticipacoesResponse> participacoesTest = new List<ParticipacoesResponse>();
            participacoesTest.Add(new ParticipacoesResponse { matricula = "0009968", nome = "Victor Wilson", valor_da_participacao = valorParticipacaoValid });
            DistribuicaoResponse responseTest = new DistribuicaoResponse()
            {
                Participacoes = participacoesTest,
                saldo_total_disponibilizado = saldoTotalValid,
                total_de_funcionarios = 1,
                total_disponibilizado = totalDisponibilizadoValid,
                total_distribuido = totalDistribuidoValid
            };
            
            var funcionarios = new List<Funcionario>();
            funcionarios.Add(funcionario);

            mockFuncionarioRepository.Setup(m => m.ObterFuncionarios()).Returns(funcionarios);
            var request = new DistribuicaoRequest() { valor_maximo_distribuicao = 121889.5 };
            DistribuicaoResponse distribuicaoLucros = distribuicaoService.ObterBonus(request);
            distribuicaoLucros.Should().BeEquivalentTo(responseTest);
        }

        [TestCaseSource("TestErrors")]
        public void Errors(
            string testName,
            double valorParticipacao,
            double saldoTotal,
            int qtdFuncionarios,
            double totalDistribuido,
            double totalDisponibilizado
            )
        {
            List<ParticipacoesResponse> participacoesTest = new List<ParticipacoesResponse>();
            participacoesTest.Add(new ParticipacoesResponse { matricula = "0009968", nome = "Victor Wilson", valor_da_participacao = valorParticipacao });
            DistribuicaoResponse responseTest = new DistribuicaoResponse()
            {
                Participacoes = participacoesTest,
                saldo_total_disponibilizado = saldoTotal,
                total_de_funcionarios = qtdFuncionarios,
                total_disponibilizado = totalDisponibilizado,
                total_distribuido = totalDistribuido
            };

            var funcionarios = new List<Funcionario>();
            funcionarios.Add(funcionario);

            mockFuncionarioRepository.Setup(m => m.ObterFuncionarios()).Returns(funcionarios);
            var request = new DistribuicaoRequest() { valor_maximo_distribuicao = totalDistribuido };
            DistribuicaoResponse distribuicaoLucros = distribuicaoService.ObterBonus(request);
            distribuicaoLucros.Should().NotBeEquivalentTo(responseTest);
        }

        public static IEnumerable<TestCaseData> TestErrors
        {
            get
            {
                yield return new TestCaseData("ValorParticipacaoError", 0, saldoTotalValid, 1, totalDistribuidoValid, totalDisponibilizadoValid);
                yield return new TestCaseData("SaldoTotalError", valorParticipacaoValid, 0, 1, totalDistribuidoValid, totalDisponibilizadoValid);
                yield return new TestCaseData("QtdFuncionariosError", valorParticipacaoValid, saldoTotalValid, 0, totalDistribuidoValid, totalDisponibilizadoValid);
                yield return new TestCaseData("TotalDistribuidoError", valorParticipacaoValid, saldoTotalValid, 1, 0, totalDisponibilizadoValid);
                yield return new TestCaseData("totalDisponibilizadoError", valorParticipacaoValid, saldoTotalValid, 1, 0, 0);
            }
        }

    }
}
