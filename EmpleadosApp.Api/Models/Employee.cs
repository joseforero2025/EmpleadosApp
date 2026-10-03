using System.ComponentModel.DataAnnotations;

namespace EmpleadosApp.Api.Models;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El cargo es obligatorio.")]
    [StringLength(60, ErrorMessage = "El cargo no puede superar los 60 caracteres.")]
    public string Cargo { get; set; } = string.Empty;

    [Range(0, 1000000000, ErrorMessage = "El salario debe ser igual o mayor que cero.")]
    public decimal Salario { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaIngreso { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El departamento debe ser válido.")]
    public int DepartamentoId { get; set; }
}



