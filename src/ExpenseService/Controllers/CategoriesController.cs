using AutoMapper;
using ExpenseService.Data;
using ExpenseService.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IExpenseRepo _repository;
    private readonly IMapper _mapper;

    public CategoriesController(IExpenseRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<CategoryReadDto>> GetCategories()
    {
        var categories = _repository.GetAllCategories();

        return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(categories));
    }
}