using CatalogCore.Entities;

namespace CatalogCore.Repositories
{
    public interface ITypeRepository
    {
        Task<IEnumerable<ProductType>> GetALlTypes();
    }
}
