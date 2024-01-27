CREATE TABLE EventData (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    EventDate DATE,
    EventText NVARCHAR(255)
);

select * from EventData

