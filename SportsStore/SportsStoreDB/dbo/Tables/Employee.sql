--This is implemented only to show a change to the database. The web site does not use this table.

CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(30) NULL, 
    [Address] NCHAR(30) NULL, 
    [Age] INT NULL, 
    [City] NCHAR(30) NOT NULL, 
    [Phone] NCHAR(10) NULL, 
    [Cell] NCHAR(10) NULL, 
    [Statezz] NCHAR(2) NULL 
)
