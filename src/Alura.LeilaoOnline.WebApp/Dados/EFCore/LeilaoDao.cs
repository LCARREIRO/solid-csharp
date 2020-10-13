using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore
{
    public class LeilaoDao : ILeilaoDao
    {
        protected readonly AppDbContext _context;
        public LeilaoDao(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Leilao> BuscarLeiloes()
        {
            return _context.Leiloes
                .Include(l => l.Categoria);
        }

        public void Adicionar(Leilao leilao)
        {
            _context.Add(leilao);
            Salvar();
        }

        public Leilao BuscarLeilaoPorId(int id)
        {
            return _context.Leiloes.Find(id);
        }

        public void Editar(Leilao leilao)
        {
            _context.Leiloes.Update(leilao);
            Salvar();
        }

        public void Remover(Leilao leilao)
        {
            _context.Leiloes.Remove(leilao);
            Salvar();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Leilao> PesquisaLeiloes(string termo)
        {
           return  _context.Leiloes
                .Include(l => l.Categoria)
                .Where(l => string.IsNullOrWhiteSpace(termo) ||
                    l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
                );
        }
    }
}
