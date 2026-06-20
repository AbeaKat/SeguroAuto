# Sistema de Emisión de Seguros de Auto

Prueba emisión de pólizas de automóviles.

## Stack propuesto

### Backend
- C# / ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Arquitectura por capas: Controller, Service, Repository, Data, Entities y DTOs

### Frontend
- React
- Vite
- TypeScript
- Tailwind CSS

## Estructura

```text
AutoInsurancePolicySystem/
  backend/
    Insurance.Api/
      Controllers/
      Data/
      Entities/
      DTOs/
      Repositories/
      Services/
      Common/
  frontend/
    insurance-web/
      src/
        api/
        components/
        pages/
        types/
  database/
    SeguroAutoDb_script_inicial.sql
  docs/
    postman/
```

## Base de datos

1. Abrir SQL Server Management Studio.
2. Ejecutar:

```sql
database/SeguroAutoDb_script.sql
```

Esto crea la base de datos `SeguroAutoDb` y las tablas principales:
- Clientes
- Vehiculos
- Coberturas
- Polizas
- PolizaCoberturas

## Ejecutar backend

Desde la carpeta:

```bash
cd backend/Insurance.Api
dotnet restore
dotnet run
```
IMPORTANTE: El proyecto corre con .NET 10 SDK

Swagger quedará disponible normalmente en:

```text
https://localhost:7001/swagger
http://localhost:5001/swagger
```

Antes de ejecutar, revisar la cadena de conexión en:

```text
backend/Insurance.Api/appsettings.json
```

## Ejecutar frontend

Desde la carpeta:

```bash
cd frontend/insurance-web
npm install
npm run dev
```
IMPORTANTE: Utilizar node.js  20.19 o superior puesto que VITE 7 requiere una version moderna para ser ejecutado.

## Endpoints principales

```http
GET    /api/clientes
GET    /api/coberturas
GET    /api/polizas
GET    /api/polizas/{id}
POST   /api/polizas/emitir
```

## Ejemplo de emisión

```json
{
  "clienteId": 1,
  "vehiculo": {
    "placa": "M123456",
    "marca": "Toyota",
    "modelo": "Corolla",
    "anio": 2022,
    "valorComercial": 15000
  },
  "coberturasIds": [1, 2, 3]
}
```


## Postman
IMPORTAR DESDE 

``` docs/
    postman/
           SeguroAuto.postman_collection.json
```
