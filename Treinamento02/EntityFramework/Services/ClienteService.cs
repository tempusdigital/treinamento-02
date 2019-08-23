using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Services
{
    /* 
     * Como salvar a nota e obrer número de nota fiscal:
       
    using (var tx = await _lojaContext.Database.BeginTransactionAsync(isolationLevel: ))
    {
        var numeroNota = await ObterProximoNumero();
        nota.Numero = numeroNota;

        _lojaContext.Add(nota);
        _lojaContext.Add(itensDaNota);

        await _lojaContext.SaveChangesAsync();

        tx.Commit();
    }
     */


    /*
* Errado
* 
var clientes = await _lojaContext
.Set<Cliente>()
.ToArrayAsync();

foreach (var cliente in clientes)
{
var clientes = await _lojaContext
    .Set<NotaFiscal>()
    .Where(n => n.ClienteId == cliente.Id)
    .ToArrayAsync();
}
*/

    /**
     * Correto
     * 
    var clientes = await _lojaContext
        .Set<Cliente>()
        .ToArrayAsync();

    var clienteIds = clientes.Select(p => p.Id).ToArray();

    var notasDeTodosClientes = await _lojaContext
        .Set<NotaFiscal>()
        .Where(p => clienteIds.Contains(p.Id))
        .ToArrayAsync();

    foreach (var cliente in clientes)
    {
        var notasDoCliente = notasDeTodosClientes.Where(p => p.ClienteId == cliente.Id).ToArray();
    }
    */
    
    public class ClienteListarDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class ValidationException : Exception
    {
        public string Campo { get; set; }

        public string Mensagem { get; set; }

        public ValidationException(string campo, string mensagem)
        {
            Campo = campo;
            Mensagem = mensagem;
        }
    }

    public class ClienteService : IClienteService
    {
        readonly LojaContext _lojaContext;

        public ClienteService(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }

        public async Task<ClienteListarDto[]> Listar(PesquisaDto pesquisa)
        {
            var query = _lojaContext
                .Set<Cliente>()
                .AsNoTracking()
                .AsQueryable();

            if (pesquisa.Nome != null && pesquisa.CpfOuCnpj != null)
                query = query.Where(p => p.Nome == pesquisa.Nome || p.CpfOuCnpj == pesquisa.CpfOuCnpj);
            else
            if (pesquisa.Nome != null)
                query = query.Where(p => p.Nome == pesquisa.Nome);
            else
            if (pesquisa.CpfOuCnpj != null)
                query = query.Where(p => p.CpfOuCnpj == pesquisa.CpfOuCnpj);

            switch (pesquisa.Ordenacao)
            {
                case PesquisaOrdenacaoDto.Nome:
                    query = query.OrderBy(p => p.Nome);
                    break;

                case PesquisaOrdenacaoDto.CpfOuCnpj:
                    query = query.OrderBy(p => p.CpfOuCnpj);
                    break;
            }

            var resultado = await query
                .Select(p => new ClienteListarDto
                {
                    Id = p.Id,
                    Nome = p.Nome
                })
                .ToArrayAsync();

            return resultado;
        }
        
        public async Task<int> Inserir(ClienteDto clienteDto)
        {
            if (string.IsNullOrWhiteSpace( clienteDto.Nome))
                throw new ValidationException("Nome", "Preencha o nome");
            
            //cliente.Id = GerarProximoId();
            var cliente = new Cliente();

            cliente.Nome = clienteDto.Nome.ToUpper();
            cliente.CpfOuCnpj = clienteDto.CpfOuCnpj.Trim();
            cliente.DataNascimento = clienteDto.DataNascimento;

            if (clienteDto.Telefones != null)
            {
                foreach (var telefoneDto in clienteDto.Telefones)
                {
                    var telefone = new ClienteTelefone();

                    telefone.Numero = telefoneDto.Numero;

                    cliente.Telefones.Add(telefone);
                }
            }

            _lojaContext.Add(cliente);

            await _lojaContext.SaveChangesAsync();

            return cliente.Id;
        }
        
        public async Task Editar(ClienteDto clienteDto)
        {
            if (clienteDto.Nome == null)
                throw new ValidationException("Nome", "Preencha o nome");
            
            var cliente = await _lojaContext
                .Set<Cliente>()
                .Where(p => p.Id == clienteDto.Id)
                .FirstOrDefaultAsync();

            if (cliente == null)
                throw new ValidationException("", "Registro não encontrado");

            cliente.Nome = clienteDto.Nome.ToUpper();
            cliente.CpfOuCnpj = clienteDto.CpfOuCnpj.Trim();
            cliente.DataNascimento = clienteDto.DataNascimento;

            await _lojaContext.SaveChangesAsync();
        }
        
        public async Task Excluir(int id)
        {
            var cliente = await _lojaContext
                .Set<Cliente>()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (cliente == null)
                throw new ValidationException("", "Registro não encontrado");

            _lojaContext.Remove(cliente);

            await _lojaContext.SaveChangesAsync();
        }
    }
}
