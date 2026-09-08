using controleEstoque.Models;
using controleEstoque.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace controleEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gerenciamentoController : ControllerBase
    {

        private readonly gerenciamentoService _gerenciamentoService;

        public gerenciamentoController(gerenciamentoService gerenciamentoService)
        {
            _gerenciamentoService = gerenciamentoService;
        }

        [HttpPost("cadastrarProduto")]
        public async Task<IActionResult> CadastrarProduto([FromBody] requestModel request)
        {
            //verifica erros de validação gerados pelo DataAnnotations no model (validator)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _gerenciamentoService.CadastrarAsync(request);
                return Ok(result);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpGet("filtrarNome")]
        public async Task<IActionResult> FiltrarNome([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome?.Trim()))
            {
                return BadRequest(new { sucesso = false, mensagem = "Nome inválido." });
            }
            try
            {
                var result = await _gerenciamentoService.FiltrarNomeAsync(nome);
                return Ok(result);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { sucesso = false, mensagem = ex.Message });
            }
            
        }

        [HttpGet("filtrarCategoria")]
        public async Task<IActionResult> FiltrarCategoria([FromQuery] string categoria)
        {
            if (string.IsNullOrEmpty(categoria?.Trim()))
            {
                return BadRequest(new { sucesso = false, mensagem = "Categoria inválida." });
            }

            var result = categoria;
            return Ok(result);
        }

        [HttpPut("editarProduto")]
        public async Task<IActionResult> EditarProduto(int id, [FromBody] requestModel request)
        {
            var result = id;

            return Ok(new { sucesso = true, mensagem = $"Editando produto com ID: {result}" });
        }

        [HttpDelete("deletarProduto")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var result = id;
            return Ok(id);
        }
    }
}
