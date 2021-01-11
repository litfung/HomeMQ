CREATE PROCEDURE [dbo].[spAddress_Insert]
	@PersonId int,
	@StreetAddress nvarchar(100),
	@City nvarchar(50),
	@State nvarchar(50),
	@ZipCode nvarchar(20)
AS
BEGIN
INSERT INTO dbo.Address(
	[PersonId], [StreetAddress], [City], [State], [ZipCode]
	) VALUES (
	@PersonId, @StreetAddress, @City, @State, @ZipCode
	)
END