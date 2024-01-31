﻿CREATE TABLE Attendance (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Checkin DATETIME,
    Checkout DATETIME
);


ALTER TABLE Attendance
ADD TotalWorkHours INT;


truncate table Attendance;


select * from Attendance;