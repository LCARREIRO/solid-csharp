using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        protected readonly ILeilaoDao _leilaoDao;

        public LeilaoApiController(ILeilaoDao leilaoDao)
        {
            _leilaoDao = leilaoDao;
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            var leiloes = _leilaoDao.BuscarLeiloes();
            return Ok(leiloes);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            return Ok(leilao);
        }

        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            _leilaoDao.Adicionar(leilao);
            return Ok(leilao);
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            _leilaoDao.Editar(leilao);
            return Ok(leilao);
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            _leilaoDao.Remover(leilao);
            return NoContent();
        }


    }
}
