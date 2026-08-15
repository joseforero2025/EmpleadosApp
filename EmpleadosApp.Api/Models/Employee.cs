namespace EmpleadosApp.Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public decimal Salario { get; set; }
    public DateTime FechaIngreso { get; set; }
    public int DepartamentoId { get; set; }
}

