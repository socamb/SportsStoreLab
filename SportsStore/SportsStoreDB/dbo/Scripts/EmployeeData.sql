if not exists (select * from [dbo].[Employee])
Begin
SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].Employee ([EmployeeId], [Name], [Address], [Age], [City]) VALUES (1, N'Scott', N'1402', 58, 'City 1')
INSERT INTO [dbo].Employee ([EmployeeId], [Name], [Address], [Age], [City]) VALUES (2, N'Corinne', N'1402 Corinne', 22, 'City 2')
INSERT INTO [dbo].Employee ([EmployeeId], [Name], [Address], [Age], [City]) VALUES (3, N'Sue', N'1402 Sue', 62, 'City 3')

SET IDENTITY_INSERT [dbo].[Employee] OFF
End