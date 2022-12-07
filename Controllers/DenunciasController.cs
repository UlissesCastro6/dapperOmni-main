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
    public class DenunciasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DenunciasController(IConfiguration config)
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
                sql.Append("SELECT IdDenuncia, IdCidadao, IdTipoDenuncia, IdStatus, DataDenuncia, ");
                sql.Append("DescricaoDenuncia, LocalDenuncia, pontosPrioridade ");
                sql.Append("FROM DENUNCIA ");

                List<Denuncia> lista = (await conexao.QueryAsync<Denuncia>(sql.ToString())).ToList();

                return Ok(lista);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            Denuncia d = null;
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT IdDenuncia, IdCidadao, IdTipoDenuncia, IdStatus, DataDenuncia, ");
                sql.Append("DescricaoDenuncia, LocalDenuncia, pontosPrioridade ");
                sql.Append("FROM DENUNCIA WHERE IdDenuncia = @IdDenuncia");

                d = await conexao.QueryFirstOrDefaultAsync<Denuncia>(sql.ToString(), new { IdDenuncia = id });

                if (d != null)
                    return Ok(d);
                else return NotFound("Denúncia não encontrado");
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertAsync(Denuncia d)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO DENUNCIA(IdCidadao, IdTipoDenuncia, IdStatus, DataDenuncia, DescricaoDenuncia, localDenuncia, pontosPrioridade) ");
                sql.Append("VALUES(@IdCidadao, @IdTipoDenuncia, @IdStatus, @DataDenuncia, @DescricaoDenuncia, @localDenuncia, @pontosPrioridade) ");
                sql.Append("SELECT CAST(SCOPE_IDENTITY() AS INT)");

                object o = await conexao.ExecuteScalarAsync(sql.ToString(), d);

                if (o != null)
                    d.IdDenuncia = Convert.ToInt32(o);
            }
            return Ok(d);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(Denuncia d)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("UPDATE DENUNCIA SET ");
                sql.Append("IdCidadao = @IdCidadao, IdTipoDenuncia = @IdTipoDenuncia, IdStatus = @IdStatus, DataDenuncia =  @DataDenuncia,  ");
                sql.Append("DescricaoDenuncia = @DescricaoDenuncia, localDenuncia = @localDenuncia, pontosPrioridade = @pontosPrioridade ");
                sql.Append("WHERE IdDenuncia = @IdDenuncia");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), d);
                return Ok(d);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("DELETE FROM DENUNCIA ");
                sql.Append("WHERE IdDenuncia = @IdDenuncia ");

                int linhasAfetadas = await conexao.ExecuteAsync(sql.ToString(), new { IdDenuncia = id });
                return Ok(linhasAfetadas);
            }
        }
    }
}