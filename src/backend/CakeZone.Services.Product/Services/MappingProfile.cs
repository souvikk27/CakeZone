using AutoMapper;
using CakeZone.Services.Product.Shared;
using CakeZone.Services.Product.Shared.Attributes;
using CakeZone.Services.Product.Shared.Categories;
using CakeZone.Services.Product.Shared.Products;

namespace CakeZone.Services.Product.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductsDto, Model.Product>();
        CreateMap<ProductsUpdateDto, Model.Product>();
        CreateMap<Model.Product, ProductViewDto>();

        //Category
        CreateMap<CategoryCreateDto, Model.Category>();
        CreateMap<CategoryUpdateDto, Model.Category>();

        //Attribute
        CreateMap<CreateAttributeDto, Model.Attribute>();
        CreateMap<UpdateAttributeDto, Model.Attribute>();
    }
}