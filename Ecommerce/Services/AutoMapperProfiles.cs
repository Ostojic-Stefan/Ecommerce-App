using AutoMapper;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<CreateProductDTO, Product>().ReverseMap();
        }
    }
}
