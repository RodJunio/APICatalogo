using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositoy
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriaParameters categoriaParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();

    }
}
