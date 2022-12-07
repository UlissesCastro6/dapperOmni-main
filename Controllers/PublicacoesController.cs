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
    public class PublicacoesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public PublicacoesController(IConfiguration config){
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            using (IDbConnection conexao = ConnectionFactory.GetStringConexao(_config))
            {
                conexao.Open();

                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT IdPublicacao, IdTipoPublicacao, ttPublicacao, txPublicacao, ");
                sql.Append("inPublicacao, dtInicioExibicao, dtFimExibicao, snPublicacao, prPublicacao ");
                sql.Append("FROM PUBLICACOES ");

                List<Publicacao> lista = (await conexao.QueryAsync<Publicacao>(sql.ToString())).ToList();

                return Ok(lista);
            }
        }
    }
}