CREATE PROCEDURE [dbo].[spPersonWithAddresses_FilterByLastName]
	@LastName nvarchar(50)
AS
begin
	set nocount on;
	select pe.*, ad.*
	from dbo.Person pe
	left join dbo.Address ad
		on ad.PersonId = pe.PersonId
	where pe.LastName = @LastName
end
