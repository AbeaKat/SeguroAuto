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
- Axios
- Zod

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
    SeguroAutoDb_script.sql
  docs/
    postman/
```

## Requisitos previos

1. .NET 10 SDK

Para verificar puedes abrir powershell y ejecutar
`dotnet --version`

2. NODE.js

Instalar Node.js 20.19 o superior, recomendado  Node LTS actual desde la pagina oficial.
Para verificar puedes abrir powershell y ejecutar
```
node -v
npm -v
```

Un problema común con npm ocurre cuando windows no reconoce npm, cuando esto ocurre puedes ejecutar:

`Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned`

Cuando pregunte si desea cambiar la política, responder:
`S`

Luego cerrar PowerShell, abrirlo de nuevo y probar:
```
powershell
npm -v
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

Abrir el archivo:

```text
backend/Insurance.Api/appsettings.json
```

Revisar la cadena de conexión:

Reemplazala por tu cadena de conexion local o de la instancia donde creaste la base de datos.

## Ejecutar backend

Abrir PowerShell y ubicarse en la carpeta del backend.

Ejemplo:

```powershell
cd "C:\Users\TU_USUARIO\Documents\SeguroAuto\backend\Insurance.Api"
```

Restaurar los paquetes NuGet:

```
powershell
dotnet restore
```

Ejecutar la API:

```
powershell
dotnet run
```

Si todo está correcto, se mostrará algo parecido a:

```text
Now listening on: http://localhost:5001
Now listening on: https://localhost:7001
```

Para desarrollo local:

```text
http://localhost:5001
```

## Abrir Swagger

Con el backend ejecutándose, abrir en el navegador:

```text
http://localhost:5001/swagger
```

Swagger permite probar los endpoints de la API desde el navegador.

Endpoints principales:
```
http
GET    /api/clientes
GET    /api/coberturas
GET    /api/polizas
GET    /api/polizas/{id}
POST   /api/polizas/emitir
```
---

## Probar la API desde Swagger

Primero probar:

```
http
GET /api/clientes
```

Luego:

```
http
GET /api/coberturas
```

Si ambos devuelven datos, la conexión a la base de datos está funcionando.

Para emitir una póliza, probar:
```
http
POST /api/polizas/emitir
```

Body de ejemplo:
```text
json
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

Respuesta esperada:

```text
201 Created
```

## Ejecutar frontend

Abrir otra ventana de PowerShell.

No cerrar la terminal donde está corriendo el backend.

Entrar a la carpeta del frontend, ejemplo:

```powershell
cd "C:\Users\TU_USUARIO\Documents\SeguroAuto\frontend\insurance-web"
```

Instalar las dependencias del frontend:

```powershell
npm ci
```

Si por alguna razón npm ci falla porque no existe package-lock.json, usar:

```
powershell
npm install
```

Ejecutar el frontend:

```
powershell
npm run dev
```

Abrir en el navegador:

```text
http://localhost:5173
```
## Postman

El proyecto incluye una colección básica de Postman en:

```text
docs/postman/SeguroAuto.postman_collection.json
```

Para importarla:

```text
Postman > Import > Files > Seleccionar SeguroAuto.postman_collection.json
```

Configurar la variable:

Requests principales:
```
http
GET  {{baseUrl}}/api/clientes
GET  {{baseUrl}}/api/coberturas
GET  {{baseUrl}}/api/polizas
GET  {{baseUrl}}/api/polizas/{{polizaId}}
POST {{baseUrl}}/api/polizas/emitir
```

Body para emitir póliza:
```
json
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

## Desarrolado por KATHERINE ABEA PALACIOS.
