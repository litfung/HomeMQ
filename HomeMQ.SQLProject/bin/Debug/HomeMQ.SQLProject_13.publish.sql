﻿/*
Deployment script for DemoDB2

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DemoDB2"
:setvar DefaultFilePrefix "DemoDB2"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Creating [dbo].[AK_Address_Unique]...';


GO
ALTER TABLE [dbo].[Address]
    ADD CONSTRAINT [AK_Address_Unique] UNIQUE NONCLUSTERED ([StreetAddress] ASC, [City] ASC, [State] ASC, [ZipCode] ASC);


GO
PRINT N'Creating [dbo].[spAddress_Insert]...';


GO
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
GO
PRINT N'Creating [dbo].[spPersonWithAddresses_FilterByLastName]...';


GO
CREATE PROCEDURE [dbo].[spPersonWithAddresses_FilterByLastName]
	@LastName nvarchar(50)
AS
begin
	set nocount on;
	select pe.*, ad.*
	from dbo.Person pe
	left join dbo.Address ad
		on ad.PersonId = pe.PersonId
	where LastName = @LastName
end
GO
PRINT N'Creating [dbo].[spPersonWithAddresses_ReadAll]...';


GO
CREATE PROCEDURE [dbo].[spPersonWithAddresses_ReadAll]
AS
begin
	set nocount on;
	select pe.*, ad.*
	from dbo.Person pe
	left join dbo.Address ad
		on ad.PersonId = pe.PersonId
end
GO
PRINT N'Update complete.';


GO
