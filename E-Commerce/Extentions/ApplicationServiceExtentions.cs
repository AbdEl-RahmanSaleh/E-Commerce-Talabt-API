using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ProductService.Dto;
using Services.Services.ProductService;
using E_Commerce.HandleResponses;
using Services.Services.CacheService;
using Services.Services.BasketService.Dto;
using Infrastructure.BasketRepository;
using Infrastructure.BasketRepository.BasketEntities;
using Services.Services.BasketService;
using Services.Services.TokenService;
using Services.Services.UserService;
using Services.Services.OrderService.Dto;
using Services.Services.PaymentService;
using Services.Services.OrderService;

namespace E_Commerce.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepositeory<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();



            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                        .Where(model => model.Value.Errors.Count > 0)
                                        .SelectMany(model => model.Value.Errors)
                                        .Select(error => error.ErrorMessage).ToList();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);

                };
            });
            //services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            return services;
        }
    }
}
