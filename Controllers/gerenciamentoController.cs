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

        [HttpPut("editarProduto")]
        public async Task<IActionResult> EditarProduto(int id, [FromBody] requestModel request)
        {

            //simulando lista de produtos sem ID
            List<requestModel> produtosTeste = new List<requestModel>
            {
                new requestModel { nome = "Produto A", categoria = "Categoria 1", preco = "R$ 10,00", quantidade = 5 },
                new requestModel { nome = "Produto A v2", categoria = "Categoria 1", preco = "R$ 12,00", quantidade = 10 },
                new requestModel { nome = "Produto B", categoria = "Categoria 2", preco = "R$ 20,00", quantidade = 3 },
                new requestModel { nome = "Produto C", categoria = "Categoria 1", preco = "R$ 15,00", quantidade = 8 }
            };

            //Como ID 1 corresponde ao índice 0 ( 1 - 1 =  0)
            int indice = id - 1;

            if (indice < 0 || indice >= produtosTeste.Count)
            {
                return NotFound(new { sucesso = false, mensagem = "Produto não encontrado." });
            }

            //atualiza os dados da lista com o que veio da Request
            var produtoParaAtualizar = produtosTeste[indice];
            produtoParaAtualizar.nome = request.nome;
            produtoParaAtualizar.categoria = request.categoria;
            produtoParaAtualizar.preco = request.preco;
            produtoParaAtualizar.quantidade = request.quantidade;

            return Ok(new { sucesso = true, produtoAtualizado = produtoParaAtualizar, mensagem = "Produto atualizado com sucesso."});
        }
    }
}
