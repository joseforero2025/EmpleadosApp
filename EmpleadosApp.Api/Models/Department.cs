using System.ComponentModel.DataAnnotations;

namespace EmpleadosApp.Api.Models;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del departamento es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;
}
