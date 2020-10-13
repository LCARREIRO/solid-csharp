using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore
{
    public class CategoriaDao : ICategoriaDao
    {
        protected readonly AppDbContext _context;
        public CategoriaDao(AppDbContext context)
        {
            _context = context;
        }
        public void Adicionar(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            Salvar();
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public IEnumerable<Categoria> BuscarCategorias()
        {
            return _context.Categorias.ToList();
        }

        public void Editar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            Salvar();
        }

        public void Remover(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            Salvar();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}