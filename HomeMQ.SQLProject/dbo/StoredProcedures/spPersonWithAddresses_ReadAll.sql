CREATE PROCEDURE [dbo].[spPersonWithAddresses_ReadAll]
AS
begin
	set nocount on;
	select pe.*, ad.*
	from dbo.Person pe
	left join dbo.Address ad
		on ad.PersonId = pe.PersonId
end
