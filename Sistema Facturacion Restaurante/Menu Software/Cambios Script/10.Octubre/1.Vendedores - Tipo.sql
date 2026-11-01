-- Vendedores: nuevo campo Tipo (Mesero, Cajero, Administrador).
-- Define el perfil con el que entra al Restaurante: Mesero toma pedidos, Cajero cierra cuentas,
-- cobra y hace arqueos, Administrador ademas configura impresoras y metodos de pago.
-- Los vendedores existentes quedan como Mesero. Script idempotente.

IF COL_LENGTH('dbo.Vendedores', 'Tipo') IS NULL
    ALTER TABLE dbo.Vendedores ADD Tipo NVARCHAR(20) NULL;
GO

UPDATE dbo.Vendedores SET Tipo = N'Mesero' WHERE Tipo IS NULL;
GO
