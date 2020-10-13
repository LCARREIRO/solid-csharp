using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces
{
    public interface ICategoriaDao
    {
        IEnumerable<Categoria> BuscarCategorias();
        Categoria BuscarCategoriaPorId(int id);
        void Adicionar(Categoria categoria);
        void Editar(Categoria categoria);
        void Remover(Categoria categoria);
        void Salvar();

    }
}