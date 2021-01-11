CREATE TABLE [dbo].[Person]
(
	[PersonId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [AK_Person_Column] UNIQUE (FirstName, LastName)
)
