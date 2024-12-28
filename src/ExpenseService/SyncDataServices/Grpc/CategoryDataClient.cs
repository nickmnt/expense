using AutoMapper;
using CategoryService;
using ExpenseService.Models;
using Grpc.Net.Client;

namespace ExpenseService.SyncDataServices.Grpc;

public class CategoryDataClient : ICategoryDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryDataClient> _logger;

    public CategoryDataClient(IConfiguration configuration, IMapper mapper, ILogger<CategoryDataClient> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
    }


    public IEnumerable<Category> ReturnAllCategories()
    {
        var grpcCategoryUrl = _configuration["GrpcCategory"];

        if (string.IsNullOrEmpty(grpcCategoryUrl))
        {
            _logger.LogError("GRPC Category service URL not found in the configuration.");
            throw new InvalidOperationException("GRPC Category service URL is missing from the configuration.");
        }

        _logger.LogInformation($"--> Calling GRPC Service {grpcCategoryUrl}");
        var channel = GrpcChannel.ForAddress(grpcCategoryUrl);
        var client = new GrpcCategory.GrpcCategoryClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllCategories(request);
            return _mapper.Map<IEnumerable<Category>>(reply.Category);
        }
        catch (Exception ex)
        {
            _logger.LogError($"--> Could not call GRPC Server: {ex.Message}");
            throw new InvalidOperationException("Error occurred while calling the GRPC server.", ex);
        }
    }
}