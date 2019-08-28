using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Services
{
    public class ProdutoPesquisaDto
    {
        public string Descricao { get; set; }
    }

    public class ProdutoPesquisaResultadoDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
    }

    public class ProdutoInserirEditarDto
    {
        public int? Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }
    }
    
    public interface IProdutoService
    {
        Task<ProdutoPesquisaResultadoDto[]> Listar(ProdutoPesquisaDto pesquisa);

        Task<int> InserirEditar(ProdutoInserirEditarDto produto);

        Task Excluir(int id);
    }
}
