CREATE PROCEDURE [dbo].[spPerson_FilterByLastName]
	@LastName nvarchar(50)
AS
begin
	set nocount on;
	select [PersonId], [FirstName], [LastName]
	from dbo.Person
	where LastName = @LastName
end
