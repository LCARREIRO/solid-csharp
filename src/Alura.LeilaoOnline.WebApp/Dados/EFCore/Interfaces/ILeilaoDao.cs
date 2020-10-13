using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore.Interfaces
{
    public interface ILeilaoDao
    {
         IEnumerable<Leilao> BuscarLeiloes();

         IEnumerable<Leilao> PesquisaLeiloes(string termo);
         Leilao BuscarLeilaoPorId(int id);
         void Adicionar(Leilao leilao);
         void Editar(Leilao leilao);
         void Remover(Leilao leilao);


         void Salvar();
    }
}