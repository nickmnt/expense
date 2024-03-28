using AutoMapper;
using CategoryService.Data;
using Grpc.Core;

namespace CategoryService.SyncDataServices.Grpc;

public class GrpcCategoryService : GrpcCategory.GrpcCategoryBase
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GrpcCategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<CategoryResponse> GetAllCategories(GetAllRequest request, ServerCallContext context)
    {
        var response = new CategoryResponse();
        var categories = _repository.GetAllCategories().Result;

        foreach (var category in categories)
        {
            response.Category.Add(_mapper.Map<GrpcCategoryModel>(category));
        }

        return Task.FromResult(response);
    }

}