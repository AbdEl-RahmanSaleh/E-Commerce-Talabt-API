using AutoMapper;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Services.Helper;
using Services.Services.ProductService.Dto;

namespace Services.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
                => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<ProductResultDto> GetProductByIdAsync(int? id)
        {
            var specs = new ProductWithTypesAndBrandsSpecifications(id);

            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpesificationsAsync(specs);

            //if (product == null)
            
            var mappedProduct = _mapper.Map<ProductResultDto>(product);

            return mappedProduct;
        }

        public async Task<Pagination<ProductResultDto>> GetProductsAsync(ProductSpecifications specifications)
        {
            var specs = new ProductWithTypesAndBrandsSpecifications(specifications);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpesificationsAsync(specs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductResultDto>>(products);

            var totalItem = await _unitOfWork.Repository<Product>().CountAsync(specs);

            return new Pagination<ProductResultDto>(specifications.PageIndex,specifications.PageSize,totalItem,mappedProducts );
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
                => await _unitOfWork.Repository<ProductType>().GetAllAsync();
    }
}
