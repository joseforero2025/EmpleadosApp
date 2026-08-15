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
}
