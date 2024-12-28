using AutoMapper;
using ExpenseService.Data;
using ExpenseService.Dtos;
using ExpenseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IExpenseRepo _repository;

    public ExpensesController(IExpenseRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("categories/{categoryId:int}")]
    public ActionResult<IEnumerable<ExpenseReadDto>> GetExpensesForCategory(int categoryId)
    {
        if (!_repository.CategoryExists(categoryId))
        {
            return NotFound();
        }
        
        var expenses = _repository.GetExpensesForCategory(categoryId);

        return Ok(_mapper.Map<IEnumerable<ExpenseReadDto>>(expenses));
    }

    [HttpGet("{expenseId}")]
    public ActionResult<ExpenseReadDto> GetExpense(int expenseId)
    {
        var expense = _repository.GetExpense(expenseId);

        return Ok(_mapper.Map<ExpenseReadDto>(expense));
    }

    [HttpPost]
    public ActionResult<ExpenseReadDto> CreateExpenseForCategory(ExpenseCreateDto expenseDto)
    {
        if (!_repository.CategoryExists(expenseDto.CategoryId))
        {
            return NotFound();
        }

        var expense = _mapper.Map<Expense>(expenseDto);
        
        _repository.CreateExpense(expenseDto.CategoryId, expense);
        _repository.SaveChanges();

        var expenseReadDto = _mapper.Map<ExpenseReadDto>(expense);
        
        return CreatedAtRoute(nameof(GetExpense), new {expenseId = expenseReadDto.Id}, expenseReadDto);
    }
}