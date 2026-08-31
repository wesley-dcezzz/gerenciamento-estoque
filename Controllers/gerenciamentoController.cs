using controleEstoque.Models;
using Microsoft.AspNetCore.Http;
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

            if (request == null || string.IsNullOrEmpty(request.nome) || string.IsNullOrEmpty(request.categoria) || !precoValido || precoDecimal <= 0 || request.quantidade < 0)
            {
                return BadRequest(new { sucesso = false, mensagem = "Dados inválidos." });
            }

            return Ok(new { sucesso = true, mensagem = $"O Produto {request.nome} foi cadastrado com sucesso." });
        }
    }
}
