using EmpleadosApp.Api.Data;
using EmpleadosApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    MigrateLegacyDepartmentColumn(db);
    AddEmailColumnIfMissing(db);

    if (!db.Employees.Any())
    {
        db.Employees.AddRange(
            new Employee { Nombre = "Ana", Telefono = "3001112233", Email = "ana@empresa.com", Cargo = "Contadora", Salario = 3500000, FechaIngreso = DateTime.Today.AddDays(-500), Departamento = "Administración" },
            new Employee { Nombre = "Luis", Telefono = "3004445566", Email = "luis@empresa.com", Cargo = "Analista", Salario = 2800000, FechaIngreso = DateTime.Today.AddDays(-220), Departamento = "TI" },
            new Employee { Nombre = "Marta", Telefono = "3007778899", Email = "marta@empresa.com", Cargo = "Gerente", Salario = 5200000, FechaIngreso = DateTime.Today.AddDays(-1000), Departamento = "Administración" }
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

app.MapControllers();

app.Run();

static bool EmployeesHasColumn(AppDbContext db, string columnName)
{
    var connection = db.Database.GetDbConnection();
    var shouldCloseConnection = connection.State != ConnectionState.Open;

    if (shouldCloseConnection)
    {
        connection.Open();
    }

    var hasColumn = false;
    using (var command = connection.CreateCommand())
    {
        command.CommandText = "PRAGMA table_info('Employees');";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            if (reader.GetString(1) == columnName)
            {
                hasColumn = true;
                break;
            }
        }
    }

    if (shouldCloseConnection)
    {
        connection.Close();
    }

    return hasColumn;
}

static void AddEmailColumnIfMissing(AppDbContext db)
{
    if (EmployeesHasColumn(db, "Email"))
    {
        return;
    }

    db.Database.ExecuteSqlRaw("ALTER TABLE Employees ADD COLUMN Email TEXT NULL;");
}

static void MigrateLegacyDepartmentColumn(AppDbContext db)
{
    if (!EmployeesHasColumn(db, "DepartamentoId"))
    {
        return;
    }

    using var transaction = db.Database.BeginTransaction();
    db.Database.ExecuteSqlRaw("ALTER TABLE Employees RENAME COLUMN DepartamentoId TO DepartamentoIdLegacy;");
    db.Database.ExecuteSqlRaw("ALTER TABLE Employees ADD COLUMN Departamento TEXT NOT NULL DEFAULT '';");
    db.Database.ExecuteSqlRaw("""
        UPDATE Employees
        SET Departamento = COALESCE(
            (SELECT Nombre FROM Departments WHERE Departments.Id = Employees.DepartamentoIdLegacy),
            CAST(DepartamentoIdLegacy AS TEXT)
        );
        """);
    db.Database.ExecuteSqlRaw("ALTER TABLE Employees DROP COLUMN DepartamentoIdLegacy;");
    db.Database.ExecuteSqlRaw("DROP TABLE Departments;");
    transaction.Commit();
}
