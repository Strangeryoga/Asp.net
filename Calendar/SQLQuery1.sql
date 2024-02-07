CREATE TABLE EventData (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    EventDate DATE,
    EventText NVARCHAR(255)
);

ALTER TABLE EventData
ADD EventType VARCHAR(50) 

select * from EventData

