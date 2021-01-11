CREATE PROCEDURE [dbo].[spPerson_ReadAll]
AS
begin
	set nocount on;
	select [PersonId], [FirstName], [LastName]
	from dbo.Person
end