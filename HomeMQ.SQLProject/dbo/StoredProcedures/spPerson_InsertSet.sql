CREATE PROCEDURE [dbo].[spPerson_InsertSet]
	@dataTable [dbo].[BasicUDT] readonly
AS
BEGIN	
	INSERT INTO dbo.Person select * from @dataTable
END