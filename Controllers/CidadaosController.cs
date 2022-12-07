using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using dapperOmni.DAL;
using dapperOmni.Models;
using System.Text;
using Dapper;

namespace dapperOmni.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadaosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CidadaosController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT IdCidadao, Email, Celular, Nome, Sobrenome, ");
                sql.Append("Cep, CPF, TituloEleitor, ZonaEleitor, Secao, PtCidadao ");
                sql.Append("FROM CIDADAO ");

                List<Cidadao> lista = (await conexao.QueryAsync<Cidadao>(sql.ToString())).ToList();

                return Ok(lista);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            Cidadao c = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT IdCidadao, Email, Celular, Nome, Sobrenome, ");
                sql.Append("Cep, CPF, TituloEleitor, ZonaEleitor, Secao, PtCidadao ");
                sql.Append("FROM CIDADAO WHERE IdCidadao = @IdCidadao");

                c = await conexao.QueryFirstOrDefaultAsync<Cidadao>(sql.ToString(), new { IdCidadao = id });

                if (c != null)
                    return Ok(c);
                else return NotFound("Cidadão não encontrado");
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(Cidadao c)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO CIDADAO (Email, Celular, Nome, Sobrenome, Cep, CPF, TituloEleitor, ZonaEleitor, Secao, ptCidadao) ");
                sql.Append("VALUES (@Email, @Celular, @Nome, @Sobrenome, @Cep, @CPF, @TituloEleitor, @ZonaEleitor, @Secao, @ptCidadao)");
                sql.Append("SELECT CAST(SCOPE_IDENTITY() AS INT)");

                object o = await conexao.ExecuteScalarAsync(sql.ToString(), c);

                if (o != null)
                    c.IdCidadao = Convert.ToInt32(o);
            }
            return Ok(c);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(Cidadao c)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE CIDADAO SET ");
                sql.Append("Email = @Email, Celular = @Celular, Nome = @Nome, Sobrenome = @Sobrenome, Cep = @Cep, CPF = @CPF, ");
                sql.Append("TituloEleitor = @TituloEleitor, ZonaEleitor = @ZonaEleitor, Secao = @Secao, ptCidadao = @ptCidadao ");
                sql.Append("WHERE IdCidadao = @IdCidadao");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), c);
                return Ok(c);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("DELETE FROM CIDADAO ");
                sql.Append("WHERE IdCidadao = @IdCidadao ");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), new { IdCidadao = id });
                return Ok(linhasAfetadas);
            }
        }
    }
}