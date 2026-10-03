using EmpleadosApp.Api.Data;
using EmpleadosApp.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=EmpleadosApp.db"));

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Departments.Any())
    {
        db.Departments.AddRange(
            new Department { Nombre = "Administración" },
            new Department { Nombre = "TI" },
            new Department { Nombre = "Recursos Humanos" }
        );
        db.SaveChanges();
    }

    if (!db.Employees.Any())
    {
        var administracion = db.Departments.First(d => d.Nombre == "Administración");
        var ti = db.Departments.First(d => d.Nombre == "TI");

        db.Employees.AddRange(
            new Employee { Nombre = "Ana", Telefono = "3001112233", Cargo = "Contadora", Salario = 3500000, FechaIngreso = DateTime.Today.AddDays(-500), DepartamentoId = administracion.Id },
            new Employee { Nombre = "Luis", Telefono = "3004445566", Cargo = "Analista", Salario = 2800000, FechaIngreso = DateTime.Today.AddDays(-220), DepartamentoId = ti.Id },
            new Employee { Nombre = "Marta", Telefono = "3007778899", Cargo = "Gerente", Salario = 5200000, FechaIngreso = DateTime.Today.AddDays(-1000), DepartamentoId = administracion.Id }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", () => Results.Redirect("/index.html"));
app.MapControllers();

app.Run();

