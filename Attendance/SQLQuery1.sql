CREATE TABLE Attendance (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Checkin DATETIME,
    Checkout DATETIME
);

select * from Attendance;