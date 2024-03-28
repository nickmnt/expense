using AutoMapper;
using CategoryService;
using ExpenseService.Models;
using Grpc.Net.Client;

namespace ExpenseService.SyncDataServices.Grpc;

public class CategoryDataClient : ICategoryDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public CategoryDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }


    public IEnumerable<Category> ReturnAllCategories()
    {
        Console.Out.WriteLine($"--> Calling GRPC Service {_configuration["GrpcCategory"]}");
        var channel = GrpcChannel.ForAddress(_configuration["GrpcCategory"]);
        var client = new GrpcCategory.GrpcCategoryClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllCategories(request);
            return _mapper.Map<IEnumerable<Category>>(reply.Category);
        }
        catch (Exception ex)
        {
            Console.Out.WriteLine($"--> Could not call GRPC Server {ex.Message}");
            return null;
        }
    }
}