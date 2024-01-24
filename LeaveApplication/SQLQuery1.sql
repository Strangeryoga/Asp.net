CREATE TABLE LeaveRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    UserID NVARCHAR(50), 
    StartDate DATE,
    EndDate DATE,
    Reason NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Pending'
);

select * from LeaveRequests;




