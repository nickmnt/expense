using AutoMapper;
using CategoryService;
using ExpenseService.Models;

namespace ExpenseService.Profiles;

public class ExpensesProfile : Profile
{
    public ExpensesProfile()
    {
        CreateMap<GrpcCategoryModel, Category>()
            .ForMember(dest => dest.ExternalId, opt
                => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Expenses, opt 
                => opt.Ignore());
    }
}