using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Dtos.Response;
using Distribuicao.Lucros.Service;
using NUnit.Framework;
using System;

namespace Distribuicao.Lucros.Test
{
    [TestFixture]
    class DistribuicaoLucrosTest
    {
        private DistribuicaoService distribuicaoService;       

        [SetUp]
        public void SetUp()
        {
            //distribuicaoService = new DistribuicaoService();
        }

        [Test]
        public void Success()
        {
            var data = new DistribuicaoRequest();
            DistribuicaoResponse distribuicaoLucros = distribuicaoService.ObterBonus(data);
            Assert.AreEqual(distribuicaoLucros.GetType(), typeof(DistribuicaoResponse));
        }
    }
}
