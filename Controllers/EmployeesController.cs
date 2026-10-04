using EmpleadosApp.Api.Data;
using EmpleadosApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosApp.Api.Controllers;

[ApiController]
[Route("employees")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empleados = await _context.Employees.AsNoTracking().ToListAsync();
        return Ok(empleados);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empleado = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        if (empleado == null)
        {
            return NotFound();
        }

        return Ok(empleado);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee nuevoEmpleado)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (nuevoEmpleado.Salario < 0)
        {
            return BadRequest("El salario no puede ser negativo.");
        }

        _context.Employees.Add(nuevoEmpleado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = nuevoEmpleado.Id }, nuevoEmpleado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Employee empleadoActualizado)
    {
        if (id != empleadoActualizado.Id)
        {
            return BadRequest("El id de la ruta no coincide con el del empleado.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var empleadoExistente = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (empleadoExistente == null)
        {
            return NotFound();
        }

        empleadoExistente.Nombre = empleadoActualizado.Nombre;
        empleadoExistente.Telefono = empleadoActualizado.Telefono;
        empleadoExistente.Cargo = empleadoActualizado.Cargo;
        empleadoExistente.Salario = empleadoActualizado.Salario;
        empleadoExistente.FechaIngreso = empleadoActualizado.FechaIngreso;
        empleadoExistente.Departamento = empleadoActualizado.Departamento;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var empleado = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (empleado == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(empleado);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}