using Distribuicao.Lucros.Domain.Dtos.Request;
using Distribuicao.Lucros.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Distribuicao.Lucros.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistribuicaoLucrosController : ControllerBase
    {
        private readonly IDistribuicaoService _distribuicaoService;

        public DistribuicaoLucrosController(IDistribuicaoService distribuicaoService)
        {
            _distribuicaoService = distribuicaoService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DistribuicaoRequest data)
        {
            try
            {
                var response = _distribuicaoService.ObterBonus(data);
                return Created("", response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
