using AutoMapper;
using CategoryService.Dtos;
using CategoryService.Models;

namespace CategoryService.Profiles;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
    }
}