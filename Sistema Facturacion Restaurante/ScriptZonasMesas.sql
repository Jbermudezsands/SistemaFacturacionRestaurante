-- Zonas de mesas: se usa Clientes.Departamento como zona (Barra, Salon, Terraza).
-- Mesas = Clientes con Limite_Credito = 0. Script idempotente: solo completa mesas sin zona.

IF COL_LENGTH('dbo.Clientes', 'Foto') IS NULL
    ALTER TABLE dbo.Clientes ADD Foto nvarchar(max) NULL
GO

UPDATE dbo.Clientes SET Departamento = 'Barra'
WHERE Limite_Credito = 0 AND ISNULL(Departamento, '') = '' AND Cod_Cliente LIKE 'B%'

UPDATE dbo.Clientes SET Departamento = 'Salon'
WHERE Limite_Credito = 0 AND ISNULL(Departamento, '') = '' AND Cod_Cliente LIKE 'M%'

UPDATE dbo.Clientes SET Departamento = 'Terraza'
WHERE Limite_Credito = 0 AND ISNULL(Departamento, '') = '' AND Cod_Cliente LIKE 'T%'
GO
