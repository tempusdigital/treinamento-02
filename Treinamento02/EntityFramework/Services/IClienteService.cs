using System.Threading.Tasks;
using EntityFramework.Models;

namespace EntityFramework.Services
{
    public interface IClienteService
    {
        Task Editar(ClienteDto clienteDto);
        Task Excluir(int id);
        Task<int> Inserir(ClienteDto clienteDto);
        Task<ClienteListarDto[]> Listar(PesquisaDto pesquisa);
    }
}