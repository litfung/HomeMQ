CREATE TABLE [dbo].[IPAddress]
(
	[IPAddressId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IPAddress] NVARCHAR(30) NOT NULL, 
    [SensorId] INT NOT NULL , 
    CONSTRAINT [AK_NetworkAddress_IPAddress] UNIQUE ([IPAddress]), 
    CONSTRAINT [FK_IPAddress_ToSensor] FOREIGN KEY ([SensorId]) REFERENCES [Sensor]([SensorId])

)