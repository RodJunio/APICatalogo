using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositoy
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
    }
}
