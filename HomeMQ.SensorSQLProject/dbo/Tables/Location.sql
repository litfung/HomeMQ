CREATE TABLE [dbo].[Location]
(
	[LocationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InstalledLocation] NVARCHAR(100) NOT NULL, 
    [MeasurementType] NVARCHAR(50) NOT NULL, 
    [SensorId] INT NOT NULL, 
    CONSTRAINT [AK_Location_Column] UNIQUE ([InstalledLocation], [MeasurementType]), 
    CONSTRAINT [FK_Location_ToSensor] FOREIGN KEY ([SensorId]) REFERENCES [Sensor]([SensorId])
)
