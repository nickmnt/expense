using AutoMapper;
using CategoryService.Data;
using CategoryService.Dtos;
using CategoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.Controllers;

[ApiController]
[Route("api/{controller}")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategories()
    {
        var result = await _repository.GetAllCategories();

        return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(result));
    }

    [HttpGet("{id}", Name = "GetCategoryById")]
    public async Task<ActionResult<CategoryReadDto>> GetCategoryById(string id)
    {
        var cat = await _repository.GetCategoryById(id);

        if (cat != null)
        {
            return Ok(_mapper.Map<CategoryReadDto>(cat));
        }
        
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
    {
        var category = _mapper.Map<Category>(categoryCreateDto);

        await _repository.CreateCategory(category);

        var categoryReadDto = _mapper.Map<CategoryReadDto>(category);

        return CreatedAtRoute(nameof(GetCategoryById), new { ID = category.ID }, categoryReadDto);
    }
}