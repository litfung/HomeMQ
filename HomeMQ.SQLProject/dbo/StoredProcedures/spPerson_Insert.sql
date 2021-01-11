CREATE PROCEDURE [dbo].[spPerson_Insert]
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
INSERT INTO dbo.Person(
	[FirstName], [LastName]
	) VALUES (
	@FirstName, @LastName
	)
END