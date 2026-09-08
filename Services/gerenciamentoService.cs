using controleEstoque.Data;
using controleEstoque.Entities;
using controleEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace controleEstoque.Services
{
    public class gerenciamentoService
    {
        //campo para guardar a conexão com o banco de dados
        private readonly AppDbContext _context;

        //injeção de dependencia para o contexto do banco de dados
        public gerenciamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> CadastrarAsync(requestModel request)
        {
            //Verifica se o produto já existe no banco de dados, utilizando o mesmo nome (precisa ser exatamente igual ao nome digitado)
            bool produtoExiste = await _context.Produtos.AnyAsync(p => p.Nome.ToLower() == request.nome.ToLower());

            if (produtoExiste)
            {
                //Lança uma exceção caso o produto já exista no banco de dados
                throw new InvalidOperationException("Produto já cadastrado.");
            }

            //Mapeia o request para a Entity do banco se não existir
            var produtoEntity = new ProdutoEntity()
            {
                Nome = request.nome,
                Categoria = request.categoria,
                Preco = request.preco,
                Quantidade = request.quantidade
            };

            //Adiciona o produto no banco de dados
            await _context.Produtos.AddAsync(produtoEntity);

            //Salva os dados no banco de dados
            await _context.SaveChangesAsync();

            //Retorna o responseModel com ID gerado
            return new ResponseModel
            {
                id = produtoEntity.Id,
                nome = produtoEntity.Nome,
                categoria = produtoEntity.Categoria,
                preco = produtoEntity.Preco,
                quantidade = produtoEntity.Quantidade
            };

        }

        public async Task<List<ResponseModel>> FiltrarNomeAsync(string nome)
        {
            //Verifica todo produto com o nome digitado, podendo retornar mais de um. Como "Celular LG" e "Celular Samsung"
            var produtosEncontrados = await _context.Produtos.Where(p => p.Nome.ToLower().Contains(nome.ToLower())).ToListAsync();

            if (produtosEncontrados == null || produtosEncontrados.Count == 0)
            {
                //lança uma exceção caso o produto não exista no banco de dados
                throw new InvalidOperationException("Produto não encontrado.");
            }

            //Retorna uma lista do tipo responseModel com os dados encontrados
            var responseList = new List<ResponseModel>();

            foreach (var produto in produtosEncontrados)
            {
                //inicializa e atribui os valores do responseModel com os dados encontrados
                responseList.Add(new ResponseModel
                {
                    id = produto.Id,
                    nome = produto.Nome,
                    categoria = produto.Categoria,
                    preco = produto.Preco,
                    quantidade = produto.Quantidade
                });
            }
            
            return responseList;
        }

        public async Task<List<ResponseModel>> FiltrarCategoriaAsync(string categoria)
        {
            var produtosEncontrados = await _context.Produtos.Where(p => p.Categoria.ToLower().Contains(categoria.ToLower())).ToListAsync();

            if (produtosEncontrados == null || produtosEncontrados.Count == 0)
            {
                //Lança uma exceção caso o produto não exista no banco de dados
                throw new InvalidOperationException("Produto não encontrado.");
            }

            //Retorna uma lista do tipo responseModel com os dados encontrados
            var responseList = new List<ResponseModel>();

            foreach (var produto in produtosEncontrados)
            {
                responseList.Add(new ResponseModel
                {
                    id = produto.Id,
                    nome = produto.Nome,
                    categoria = produto.Categoria,
                    preco = produto.Preco,
                    quantidade = produto.Quantidade
                });
            }

            return responseList;
        }
    }
}
