using CatalogCore.Entities;

namespace CatalogCore.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<ProductBrand>> GetAllBrands();
    }
}
