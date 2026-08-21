-- 1. Жестко удаляем старые таблицы (IF EXISTS работает безотказно)
DROP TABLE IF EXISTS [dbo].[Orders];
DROP TABLE IF EXISTS [dbo].[Cars];
DROP TABLE IF EXISTS [dbo].[Clients];
DROP TABLE IF EXISTS [dbo].[Inventory];
DROP TABLE IF EXISTS [dbo].[Services];
GO

-- 2. Создаем таблицы заново с автосчетчиками
CREATE TABLE [dbo].[Services] (
    [service_id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [name]       NVARCHAR (150)    NOT NULL,
    [price]      DECIMAL (10, 2)   NOT NULL
);
GO

CREATE TABLE [dbo].[Inventory] (
    [part_id]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [part_name] NVARCHAR (150)    NOT NULL,
    [price]     DECIMAL (10, 2)   NOT NULL,
    [quantity]  INT               NOT NULL
);
GO

CREATE TABLE [dbo].[Clients] (
    [client_id]    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [last_name]    NVARCHAR (50)     NOT NULL,
    [first_name]   NVARCHAR (50)     NOT NULL,
    [middle_name]  NVARCHAR (50)     NULL,
    [phone_number] NVARCHAR (20)     NOT NULL
);
GO

CREATE TABLE [dbo].[Cars] (
    [car_id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [brand]         NVARCHAR (50)     NOT NULL,
    [model]         NVARCHAR (50)     NOT NULL,
    [license_plate] NVARCHAR (20)     NOT NULL,
    [client_id]     INT               NOT NULL,
    FOREIGN KEY ([client_id]) REFERENCES [dbo].[Clients]([client_id])
);
GO

CREATE TABLE [dbo].[Orders] (
    [order_id]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [order_date] DATETIME          NOT NULL DEFAULT GETDATE(),
    [status]     NVARCHAR (50)     NOT NULL,
    [car_id]     INT               NOT NULL,
    [service_id] INT               NOT NULL,
    [part_id]    INT               NULL,
    FOREIGN KEY ([car_id]) REFERENCES [dbo].[Cars]([car_id]),
    FOREIGN KEY ([service_id]) REFERENCES [dbo].[Services]([service_id]),
    FOREIGN KEY ([part_id]) REFERENCES [dbo].[Inventory]([part_id])
);
GO

-- 3. Добавляем тестовые данные
INSERT INTO Services (name, price) VALUES ('Замена масла', 1500.00), ('Диагностика', 2000.00);
INSERT INTO Inventory (part_name, price, quantity) VALUES ('Масло 5W-40 4L', 3500.00, 10);
INSERT INTO Clients (last_name, first_name, middle_name, phone_number) VALUES ('Иванов', 'Иван', 'Иванович', '+79991112233');
INSERT INTO Cars (brand, model, license_plate, client_id) VALUES ('BMW', 'M5 F90', 'А001АА777', 1);
INSERT INTO Orders (status, car_id, service_id, part_id) VALUES ('В работе', 1, 1, 1);
GO