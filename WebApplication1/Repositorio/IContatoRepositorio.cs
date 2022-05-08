using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace MasterCoinTest.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoTable> BuscarTodos();
        ContatoTable Adicionar(ContatoTable contato);
    }
}
