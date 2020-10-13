
using System;
using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {
        protected readonly ILeilaoDao _leilaoDao;
        protected readonly ICategoriaDao _categoriaDao;

        public LeilaoController(
            ILeilaoDao leilaoDao,
            ICategoriaDao categoriaDao)
        {
            _leilaoDao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public IActionResult Index()
        {
            var leiloes = _leilaoDao.BuscarLeiloes();

            return View(leiloes);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            // ViewData["Categorias"] = _context.Categorias.ToList();
            ViewData["Operacao"] = "Inclusão";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _leilaoDao.Adicionar(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _categoriaDao.BuscarCategorias();
            ViewData["Operacao"] = "Inclusão";
            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _categoriaDao.BuscarCategorias();
            ViewData["Operacao"] = "Edição";
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null) return NotFound();
            return View("Form", leilao);
        }

        [HttpPost]
        public IActionResult Edit(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _leilaoDao.Editar(model);
                return RedirectToAction("Index");
            }
            ViewData["Categorias"] = _categoriaDao.BuscarCategorias();
            ViewData["Operacao"] = "Edição";
            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Inicia(int id)
        {
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null) return NotFound();
            if (leilao.Situacao != SituacaoLeilao.Rascunho) return StatusCode(405);
            leilao.Situacao = SituacaoLeilao.Pregao;
            leilao.Inicio = DateTime.Now;
            _leilaoDao.Editar(leilao);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null) return NotFound();
            if (leilao.Situacao != SituacaoLeilao.Pregao) return StatusCode(405);
            leilao.Situacao = SituacaoLeilao.Finalizado;
            leilao.Termino = DateTime.Now;
            _leilaoDao.Editar(leilao);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _leilaoDao.BuscarLeilaoPorId(id);
            if (leilao == null) return NotFound();
            if (leilao.Situacao == SituacaoLeilao.Pregao) return StatusCode(405);
            _leilaoDao.Remover(leilao);
            return NoContent();
        }

        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;
            var leiloes = _leilaoDao.PesquisaLeiloes(termo);
            return View("Index", leiloes);
        }
    }
}
