using controleEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace controleEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gerenciamentoController : ControllerBase
    {

        [HttpPost("cadastrarProduto")]
        public async Task<IActionResult> CadastrarProduto([FromBody] requestModel request)
        {
            string precoLimpo = request.preco?.Replace("R$", "").Replace(" ", "").Replace(".", "").Replace(",", ".").Trim();
            bool precoValido = decimal.TryParse(precoLimpo, out decimal precoDecimal);

            return Created(string.Empty, new { sucesso = true, mensagem = $"O Produto {request.nome} foi cadastrado com sucesso." });
        }

        [HttpGet("filtrarNome")]
        public async Task<IActionResult> FiltrarNome([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome?.Trim()))
            {
                return BadRequest(new { sucesso = false, mensagem = "Nome inválido." });
            }

            List<requestModel> produtosTeste = new List<requestModel>
            {
                new requestModel { nome = "Produto A", categoria = "Categoria 1", preco = "R$ 10,00", quantidade = 5 },
                new requestModel { nome = "Produto A v2", categoria = "Categoria 1", preco = "R$ 12,00", quantidade = 10 },
                new requestModel { nome = "Produto B", categoria = "Categoria 2", preco = "R$ 20,00", quantidade = 3 },
                new requestModel { nome = "Produto C", categoria = "Categoria 1", preco = "R$ 15,00", quantidade = 8 }
            };

            // Filtra os produtos pelo nome
            var produtosFiltrados = produtosTeste.Where(p => p.nome != null && p.nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(new { sucesso = true, produtos = produtosFiltrados, mensagem = "Produtos filtrados com sucesso." });
        }

        [HttpGet("filtrarCategoria")]
        public async Task<IActionResult> FiltrarCategoria([FromQuery] string categoria)
        {
            if (string.IsNullOrEmpty(categoria?.Trim()))
            {
                return BadRequest(new { sucesso = false, mensagem = "Categoria inválida." });
            }

            List<requestModel> produtosTeste = new List<requestModel>
            {
                new requestModel { nome = "Produto A", categoria = "Categoria 1", preco = "R$ 10,00", quantidade = 5 },
                new requestModel { nome = "Produto A v2", categoria = "Categoria 1", preco = "R$ 12,00", quantidade = 10 },
                new requestModel { nome = "Produto B", categoria = "Categoria 2", preco = "R$ 20,00", quantidade = 3 },
                new requestModel { nome = "Produto C", categoria = "Categoria 1", preco = "R$ 15,00", quantidade = 8 }
            };

            var produtosFiltrados = produtosTeste.Where(p => p.categoria != null && p.categoria.Contains(categoria, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(new { sucesso = true, produtos = produtosFiltrados, mensagem = "Produtos filtrados com sucesso." });
        }
    }
}
