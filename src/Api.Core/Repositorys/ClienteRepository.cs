﻿using Api.Core.Contracts.Repositorys;
using Api.Core.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Repositorys
{
    public class ClienteRepository : BaseRepository, IClienteRepository
    {
        public ClienteRepository(IConnectionFactoryDatabase connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<Cliente> BuscarPorCpfAsync(string cpf)
        {
            var conn = _connectionFactory.Connection();
            var query = await conn.QueryAsync<Cliente>(sql: ClienteQuery.SELECT_CLIENTE_POR_CPF, param: new { cpf });

            var result = query.LastOrDefault();
            return result;
        }
    }
    public class EnderecoClienteRepository : BaseRepository, IEnderecoClienteRepository
    {
        public EnderecoClienteRepository(IConnectionFactoryDatabase connectionFactory) : base(connectionFactory)
        {
        }

        public async Task AlterarAsync(EnderecoCliente enderecoCliente)
        {
            var conn = _connectionFactory.Connection();
            await conn.ExecuteAsync(sql: EnderecoQuery.UPDATE_ENDERECO, param: new
            {
                id = enderecoCliente.Id,
                logradouro = enderecoCliente.Logradouro,
                numero = enderecoCliente.Numero,
                complemento = enderecoCliente.Complemento,
                cidade = enderecoCliente.Cidade,
                estado = enderecoCliente.Estado,
                pais = enderecoCliente.Pais,
                cep = enderecoCliente.CEP
            });
        }

        public async Task<EnderecoCliente> BuscaEnderecoPorIdAsync(long idCliente, int idEndereco)
        {
            var conn = _connectionFactory.Connection();
            var query = await conn.QueryAsync<EnderecoCliente>(sql: EnderecoQuery.SELECT_ENDERECOS_POR_ID, param: new { idCliente, id = idEndereco });

            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<EnderecoCliente>> BuscarPorIdClienteAsync(long idCliente)
        {
            var conn = _connectionFactory.Connection();
            var query = await conn.QueryAsync<EnderecoCliente>(sql: EnderecoQuery.SELECT_ENDERECOS_POR_ID_CLIENTE, param: new { idCliente });

            return query;
        }
    }
}
