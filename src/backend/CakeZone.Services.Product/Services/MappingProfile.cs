using AutoMapper;
using CakeZone.Services.Product.Shared;
using CakeZone.Services.Product.Shared.Categories;
using CakeZone.Services.Product.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductsDto, Model.Product>();
        CreateMap<ProductsUpdateDto, Model.Product>();
        CreateMap<CategoryCreateDto, Model.Category>();
    }
}