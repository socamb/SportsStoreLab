﻿CREATE TABLE [dbo].[Employee]
(
  [EmployeeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NULL, 
    [City] NVARCHAR(50) NULL
)
