using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Services.Services.ProductService.Dto
{
    public class ProductUrlResolver : IValueResolver<Product, ProductResultDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return _configuration["BaseUrl"] + source.PictureUrl;
            return null;
        }
    }
}
