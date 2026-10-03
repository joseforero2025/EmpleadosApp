using EmpleadosApp.Api.Data;
using EmpleadosApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosApp.Api.Controllers;

[ApiController]
[Route("departments")]
public class DepartmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departamentos = await _context.Departments.AsNoTracking().ToListAsync();
        return Ok(departamentos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var departamento = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        if (departamento == null)
        {
            return NotFound();
        }

        return Ok(departamento);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Department nuevoDepartamento)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        _context.Departments.Add(nuevoDepartamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = nuevoDepartamento.Id }, nuevoDepartamento);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Department departamentoActualizado)
    {
        if (id != departamentoActualizado.Id)
        {
            return BadRequest("El id de la ruta no coincide con el del departamento.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var departamentoExistente = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (departamentoExistente == null)
        {
            return NotFound();
        }

        departamentoExistente.Nombre = departamentoActualizado.Nombre;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var departamento = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (departamento == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(departamento);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
