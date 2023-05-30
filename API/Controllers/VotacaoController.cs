using API.Services;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Domain.Models;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class VotacaoController : ControllerBase
    {
        private IApuracaoService _service;

        public VotacaoController(IApuracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            return "OK";
        }

        [HttpGet]
        [Route("votacao/{idVotacao}")]
        public async Task<DetalheApuracao> GetDetalheApuracao([FromRoute] int idVotacao)
        {
            return await _service.GetDetalheApuracao(idVotacao);
        }
    }
}
