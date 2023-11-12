using Core.Entities;
using Infrastructure.Specifications;
using Services.Helper;
using Services.Services.ProductService.Dto;

namespace Services.Services.ProductService
{
    public interface IProductService
    {
        Task<ProductResultDto> GetProductByIdAsync(int? id);
        Task<Pagination<ProductResultDto>> GetProductsAsync(ProductSpecifications specifications);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
