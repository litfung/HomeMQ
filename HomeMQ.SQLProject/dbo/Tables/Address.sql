CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [StreetAddress] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [ZipCode] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [FK_Address_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId]), 
    CONSTRAINT [AK_Address_Unique] UNIQUE (StreetAddress, City, State, ZipCode)
)
