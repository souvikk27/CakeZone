using AutoMapper;
using CakeZone.Services.Product.Shared;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductsDto, Model.Product>();
    }
}