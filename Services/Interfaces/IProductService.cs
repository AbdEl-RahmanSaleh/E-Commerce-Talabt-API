using Core.Entities;
using Infrastructure.Interfaces;

namespace Services.Interfaces
{
    public interface IProductService 
    {
        Task<Product> GetProductByIdAsync(int? id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
