using Application.Models.Request;
using AutoMapper;
using Domain.Models;
using Infrastructure.Database.Models;

namespace Application.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Product

        CreateMap<GetProductRequest, ProductFilterDto>();
        CreateMap<CreateProductRequest, CreateProductDto>();
        CreateMap<UpdateProductRequest, UpdateProductDto>();
        CreateMap<ProductDto, ProductEntity>()
            .ForMember(x => x.Inventory, y => y.Ignore());
        CreateMap<ProductEntity, ProductDto>();

        #endregion

        #region Category

        CreateMap<GetCategoryRequest, CategoryFilterDto>();
        CreateMap<CreateCategoryRequest, CreateCategoryDto>();
        CreateMap<UpdateCategoryRequest, UpdateCategoryDto>();
        CreateMap<CategoryDto, CategoryEntity>();
        CreateMap<CategoryEntity, CategoryDto>();

        #endregion

        #region Inventory

        CreateMap<InventoryDto, InventoryEntity>();
        CreateMap<InventoryEntity, InventoryDto>()
            .ForMember(x => x.Product, opt => opt.Ignore());

        #endregion
    }
}