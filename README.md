# EmpleadosApp API

API REST para la gestión de empleados, construida con ASP.NET Core (.NET 10), Entity Framework Core y SQLite.

## Tecnologías

- .NET 10 / ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger (documentación interactiva)

## Requisitos

- [SDK de .NET 10](https://dotnet.microsoft.com/download)

## Cómo ejecutarlo

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/joseforero2025/EmpleadosApp.git
   ```

2. Entrar a la carpeta del proyecto:

   ```bash
   cd EmpleadosApp
   ```

3. Ejecutar la aplicación:

   ```bash
   dotnet run
   ```

4. Abrir Swagger en el navegador: [http://localhost:5249/swagger](http://localhost:5249/swagger)

La base de datos (`EmpleadosApp.db`) se crea automáticamente la primera vez, con 3 empleados de ejemplo.

## Endpoints

| Método | Ruta              | Descripción                |
|--------|-------------------|----------------------------|
| GET    | `/employees`      | Lista todos los empleados  |
| GET    | `/employees/{id}` | Obtiene un empleado por id |
| POST   | `/employees`      | Crea un empleado           |
| PUT    | `/employees/{id}` | Actualiza un empleado      |
| DELETE | `/employees/{id}` | Elimina un empleado        |

## Ejemplo de empleado (JSON)

```json
{
  "nombre": "Ana",
  "telefono": "3001112233",
  "cargo": "Contadora",
  "salario": 3500000,
  "fechaIngreso": "2025-01-15",
  "departamento": "Administración"
}
```

## Pruebas con Postman

Importar el archivo `EmpleadosApp.postman_collection.json` en Postman.

## Autor

Jose Luis Forero
