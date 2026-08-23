using Microsoft.AspNetCore.Mvc;
using EmpleadosApp.Api.Models;

namespace EmpleadosApp.Api.Controllers;

[ApiController]
[Route("employees")]
public class EmployeesController : ControllerBase
{
    private static List<Employee> _empleados = new()
    {
        new Employee { Id = 1, Nombre = "Ana", Cargo = "Contadora", Salario = 3500000, FechaIngreso = DateTime.Now, DepartamentoId = 1 },
        new Employee { Id = 2, Nombre = "Luis", Cargo = "Analista", Salario = 2800000, FechaIngreso = DateTime.Now, DepartamentoId = 2 },
        new Employee { Id = 3, Nombre = "Marta", Cargo = "Gerente", Salario = 5200000, FechaIngreso = DateTime.Now, DepartamentoId = 1 }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_empleados);
    }
    [HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var empleado = _empleados.FirstOrDefault(e => e.Id == id);

    if (empleado == null)
    {
        return NotFound();
    }

    return Ok(empleado);
}
[HttpPost]
public  Create(Employee nuevoEmpleado)
{
    if (nuevoEmpleado.Salario < 0)
    {
        return BadRequest("El salario no puede ser negativo");
    }

    nuevoEmpleado.Id = _empleados.Max(e => e.Id) + 1;
    _empleados.Add(nuevoEmpleado);

    return CreatedAtAction(nameof(GetById), new { id = nuevoEmpleado.Id }, nuevoEmpleado);
}
[HttpGet("buscar")]
    public IActionResult BuscarPorCargo([FromQuery] string cargo)
    {
        var resultado = _empleados
            .Where(e => e.Cargo.Contains(cargo, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(resultado);
    }
}
