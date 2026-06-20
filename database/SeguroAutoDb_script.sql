/*
    Sistema de Emisión de Seguros de Auto
    Base de datos: SQL Server
    Descripción:
    Script inicial para crear las tablas principales de la prueba:
    Clientes, Vehiculos, Coberturas, Polizas y PolizaCoberturas.
*/

IF DB_ID('SeguroAutoDb') IS NULL
BEGIN
    CREATE DATABASE SeguroAutoDb;
END
GO

USE SeguroAutoDb;
GO

IF OBJECT_ID('dbo.Tbl_PolizaCoberturas', 'U') IS NOT NULL
    DROP TABLE dbo.Tbl_PolizaCoberturas;
GO

IF OBJECT_ID('dbo.Tbl_Polizas', 'U') IS NOT NULL
    DROP TABLE dbo.Tbl_Polizas;
GO

IF OBJECT_ID('dbo.Tbl_Coberturas', 'U') IS NOT NULL
    DROP TABLE dbo.Tbl_Coberturas;
GO

IF OBJECT_ID('dbo.Tbl_Vehiculos', 'U') IS NOT NULL
    DROP TABLE dbo.Tbl_Vehiculos;
GO

IF OBJECT_ID('dbo.Tbl_Clientes', 'U') IS NOT NULL
    DROP TABLE dbo.Tbl_Clientes;
GO

CREATE TABLE dbo.Tbl_Clientes
(
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(150) NOT NULL,
    Identificacion NVARCHAR(30) NOT NULL,
    Correo NVARCHAR(150) NOT NULL,
    Telefono NVARCHAR(30) NULL,
    FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_Clientes_FechaCreacion DEFAULT SYSDATETIME(),
    Activo BIT NOT NULL CONSTRAINT DF_Clientes_Activo DEFAULT 1,

    CONSTRAINT PK_Clientes PRIMARY KEY (Id),
    CONSTRAINT UQ_Clientes_Identificacion UNIQUE (Identificacion),
    CONSTRAINT CK_Clientes_Correo CHECK (Correo LIKE '%_@_%._%')
);
GO

CREATE TABLE dbo.Tbl_Vehiculos
(
    Id INT IDENTITY(1,1) NOT NULL,
    Placa NVARCHAR(20) NOT NULL,
    Marca NVARCHAR(80) NOT NULL,
    Modelo NVARCHAR(80) NOT NULL,
    Anio SMALLINT NOT NULL,
    ValorComercial DECIMAL(18,2) NOT NULL,
    FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_Vehiculos_FechaCreacion DEFAULT SYSDATETIME(),
    Activo BIT NOT NULL CONSTRAINT DF_Vehiculos_Activo DEFAULT 1,

    CONSTRAINT PK_Vehiculos PRIMARY KEY (Id),
    CONSTRAINT UQ_Vehiculos_Placa UNIQUE (Placa),
    CONSTRAINT CK_Vehiculos_Anio CHECK (Anio BETWEEN 1900 AND 2100),
    CONSTRAINT CK_Vehiculos_ValorComercial CHECK (ValorComercial > 0)
);
GO

CREATE TABLE dbo.Tbl_Coberturas
(
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250) NULL,
    MontoCobertura DECIMAL(18,2) NOT NULL,
    FechaCreacion DATETIME2(0) NOT NULL CONSTRAINT DF_Coberturas_FechaCreacion DEFAULT SYSDATETIME(),
    Activo BIT NOT NULL CONSTRAINT DF_Coberturas_Activo DEFAULT 1,

    CONSTRAINT PK_Coberturas PRIMARY KEY (Id),
    CONSTRAINT UQ_Coberturas_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_Coberturas_MontoCobertura CHECK (MontoCobertura > 0)
);
GO

CREATE TABLE dbo.Tbl_Polizas
(
    Id INT IDENTITY(1,1) NOT NULL,
    NumeroPoliza NVARCHAR(30) NOT NULL,
    ClienteId INT NOT NULL,
    VehiculoId INT NOT NULL,
    FechaEmision DATETIME2(0) NOT NULL CONSTRAINT DF_Polizas_FechaEmision DEFAULT SYSDATETIME(),
    SumaAsegurada DECIMAL(18,2) NOT NULL,
    PrimaTotal DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(30) NOT NULL CONSTRAINT DF_Polizas_Estado DEFAULT 'Emitida',

    CONSTRAINT PK_Polizas PRIMARY KEY (Id),
    CONSTRAINT UQ_Polizas_NumeroPoliza UNIQUE (NumeroPoliza),
    CONSTRAINT FK_Polizas_Clientes FOREIGN KEY (ClienteId) REFERENCES dbo.Tbl_Clientes(Id),
    CONSTRAINT FK_Polizas_Vehiculos FOREIGN KEY (VehiculoId) REFERENCES dbo.Tbl_Vehiculos(Id),
    CONSTRAINT CK_Polizas_SumaAsegurada CHECK (SumaAsegurada > 0),
    CONSTRAINT CK_Polizas_PrimaTotal CHECK (PrimaTotal >= 0),
    CONSTRAINT CK_Polizas_Estado CHECK (Estado IN ('Emitida', 'Cancelada', 'Vencida'))
);
GO

CREATE TABLE dbo.Tbl_PolizaCoberturas
(
    Id INT IDENTITY(1,1) NOT NULL,
    PolizaId INT NOT NULL,
    CoberturaId INT NOT NULL,
    MontoAplicado DECIMAL(18,2) NOT NULL,

    CONSTRAINT PK_PolizaCoberturas PRIMARY KEY (Id),
    CONSTRAINT FK_PolizaCoberturas_Polizas FOREIGN KEY (PolizaId) REFERENCES dbo.Tbl_Polizas(Id),
    CONSTRAINT FK_PolizaCoberturas_Coberturas FOREIGN KEY (CoberturaId) REFERENCES dbo.Tbl_Coberturas(Id),
    CONSTRAINT UQ_PolizaCoberturas_Poliza_Cobertura UNIQUE (PolizaId, CoberturaId),
    CONSTRAINT CK_PolizaCoberturas_MontoAplicado CHECK (MontoAplicado > 0)
);
GO

CREATE INDEX IX_Polizas_ClienteId ON dbo.Tbl_Polizas(ClienteId);
GO
CREATE INDEX IX_Polizas_VehiculoId ON dbo.Tbl_Polizas(VehiculoId);
GO
CREATE INDEX IX_Polizas_FechaEmision ON dbo.Tbl_Polizas(FechaEmision);
GO
CREATE INDEX IX_PolizaCoberturas_PolizaId ON dbo.Tbl_PolizaCoberturas(PolizaId);
GO
CREATE INDEX IX_PolizaCoberturas_CoberturaId ON dbo.Tbl_PolizaCoberturas(CoberturaId);
GO

INSERT INTO dbo.Tbl_Clientes (Nombre, Identificacion, Correo, Telefono)
VALUES
('Juan Pérez', '001-010190-0001A', 'juan.perez@email.com', '8888-1111'),
('María López', '001-020292-0002B', 'maria.lopez@email.com', '8888-2222'),
('Carlos Ramírez', '001-030393-0003C', 'carlos.ramirez@email.com', '8888-3333');
GO

INSERT INTO dbo.Tbl_Coberturas (Nombre, Descripcion, MontoCobertura)
VALUES
('Robo', 'Cobertura ante robo total o parcial del vehículo.', 150.00),
('Choque', 'Cobertura ante daños por colisión.', 250.00),
('Responsabilidad Civil', 'Cobertura por daños ocasionados a terceros.', 100.00),
('Daños a Terceros', 'Cobertura adicional por daños materiales a terceros.', 125.00),
('Asistencia Vial', 'Servicio de grúa y asistencia en carretera.', 50.00);
GO

SELECT * FROM dbo.Tbl_Clientes;
SELECT * FROM dbo.Tbl_Coberturas;
GO
