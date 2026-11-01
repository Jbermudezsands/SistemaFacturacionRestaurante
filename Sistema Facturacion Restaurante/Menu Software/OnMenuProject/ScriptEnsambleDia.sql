/*=====================================================================================================
  ENSAMBLE DEL DIA (frmEnsambleDia) - TRAZABILIDAD
  Ejecutar una vez por base de datos de cliente. Es idempotente: se puede correr varias veces.

  Ensamble_Dia            Encabezado de cada proceso: dia de las comandas, bodegas, usuario y los
                          documentos generados (Salida Bodega y Mercancia Recibida).
  Ensamble_Dia_Producto   Productos ensamblados en el proceso (cantidad facturada del dia y costo).
  Ensamble_Dia_Componente Componentes descargados por cada producto (receta, cantidad y costo).
=====================================================================================================*/

IF OBJECT_ID('dbo.Ensamble_Dia', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ensamble_Dia
    (
        Id_EnsambleDia     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ensamble_Dia PRIMARY KEY,
        Fecha_Ensamble     SMALLDATETIME     NOT NULL,                                   -- dia de las comandas procesadas
        Fecha_Proceso      DATETIME          NOT NULL CONSTRAINT DF_Ensamble_Dia_FechaProceso DEFAULT (GETDATE()),
        Cod_Bodega_Origen  NVARCHAR(100)     NOT NULL,                                   -- de aqui salen los componentes
        Cod_Bodega_Destino NVARCHAR(100)     NOT NULL,                                   -- aqui entran los productos ensamblados
        Numero_Salida      NVARCHAR(100)     NULL,                                       -- Facturas.Numero_Factura (Salida Bodega)
        Numero_Compra      NVARCHAR(100)     NULL,                                       -- Compras.Numero_Compra (Mercancia Recibida)
        Total_Costo        FLOAT             NOT NULL CONSTRAINT DF_Ensamble_Dia_TotalCosto DEFAULT (0),
        Usuario            NVARCHAR(100)     NULL,
        Equipo             NVARCHAR(100)     NULL,
        Estado             NVARCHAR(20)      NOT NULL CONSTRAINT DF_Ensamble_Dia_Estado DEFAULT ('Procesado'),
        Observaciones      NVARCHAR(500)     NULL
    );
END
GO

IF OBJECT_ID('dbo.Ensamble_Dia_Producto', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ensamble_Dia_Producto
    (
        Id_EnsambleDiaProducto INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ensamble_Dia_Producto PRIMARY KEY,
        Id_EnsambleDia         INT               NOT NULL,
        Cod_Producto           NVARCHAR(100)     NOT NULL,
        Descripcion_Producto   NVARCHAR(500)     NULL,
        Cantidad_Vendida       FLOAT             NOT NULL,                               -- facturado en el dia
        Cantidad               FLOAT             NOT NULL,                               -- unidades ensambladas en este proceso
        Costo_Unitario         FLOAT             NOT NULL CONSTRAINT DF_Ensamble_Dia_Producto_Costo DEFAULT (0),
        Costo_Total            FLOAT             NOT NULL CONSTRAINT DF_Ensamble_Dia_Producto_Total DEFAULT (0),
        CONSTRAINT FK_Ensamble_Dia_Producto_Ensamble_Dia FOREIGN KEY (Id_EnsambleDia) REFERENCES dbo.Ensamble_Dia (Id_EnsambleDia)
    );
    CREATE INDEX IX_Ensamble_Dia_Producto_Ensamble ON dbo.Ensamble_Dia_Producto (Id_EnsambleDia);
    CREATE INDEX IX_Ensamble_Dia_Producto_Producto ON dbo.Ensamble_Dia_Producto (Cod_Producto);
END
GO

IF OBJECT_ID('dbo.Ensamble_Dia_Componente', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ensamble_Dia_Componente
    (
        Id_EnsambleDiaComponente INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ensamble_Dia_Componente PRIMARY KEY,
        Id_EnsambleDia           INT               NOT NULL,
        Cod_Producto             NVARCHAR(100)     NOT NULL,                             -- producto ensamblado
        Cod_Componente           NVARCHAR(100)     NOT NULL,                             -- componente descargado
        Descripcion_Componente   NVARCHAR(500)     NULL,
        Cantidad_Unidad          FLOAT             NOT NULL,                             -- receta por unidad ensamblada
        Cantidad_Total           FLOAT             NOT NULL,                             -- Cantidad_Unidad x unidades ensambladas
        Costo_Unitario           FLOAT             NOT NULL CONSTRAINT DF_Ensamble_Dia_Componente_Costo DEFAULT (0),
        Costo_Total              FLOAT             NOT NULL CONSTRAINT DF_Ensamble_Dia_Componente_Total DEFAULT (0),
        CONSTRAINT FK_Ensamble_Dia_Componente_Ensamble_Dia FOREIGN KEY (Id_EnsambleDia) REFERENCES dbo.Ensamble_Dia (Id_EnsambleDia)
    );
    CREATE INDEX IX_Ensamble_Dia_Componente_Ensamble ON dbo.Ensamble_Dia_Componente (Id_EnsambleDia);
    CREATE INDEX IX_Ensamble_Dia_Componente_Producto ON dbo.Ensamble_Dia_Componente (Cod_Producto);
END
GO

-- Consulta de apoyo para auditar un dia:
--   SELECT E.Id_EnsambleDia, E.Fecha_Ensamble, E.Numero_Salida, E.Numero_Compra, P.Cod_Producto, P.Cantidad, C.Cod_Componente, C.Cantidad_Total
--   FROM Ensamble_Dia E
--   INNER JOIN Ensamble_Dia_Producto P ON P.Id_EnsambleDia = E.Id_EnsambleDia
--   INNER JOIN Ensamble_Dia_Componente C ON C.Id_EnsambleDia = E.Id_EnsambleDia AND C.Cod_Producto = P.Cod_Producto
--   WHERE E.Fecha_Ensamble = '2026-01-31'
