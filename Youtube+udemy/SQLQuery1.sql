

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Upass NVARCHAR(100) NOT NULL,
    Ustatus BIT NOT NULL DEFAULT 0 -- 0 for unblocked, 1 for blocked
);


CREATE TABLE AdminUsers (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

-- Inserting sample admin user for testing
	INSERT INTO AdminUsers (Email, Password) VALUES ('admin@example.com', 'admin');


-- Table for courses
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL
);

ALTER TABLE Courses
ADD MasterCourseID INT;


CREATE TABLE MasterCourse (
    MasterCourseID INT PRIMARY KEY IDENTITY(1,1),
    MasterCourseName VARCHAR(100)
);

ALTER TABLE Courses
ADD CONSTRAINT FK_Courses_MasterCourse
FOREIGN KEY (MasterCourseID)
REFERENCES MasterCourse(MasterCourseID);


create TABLE Videos (
    VideoID INT IDENTITY(1,1) PRIMARY KEY,
    VideoName NVARCHAR(100),
    VideoPath NVARCHAR(200),
	IsCompleted BIT NOT NULL DEFAULT 0,
	CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
);

-- Step 1: Alter the Videos table to add the new foreign key column
ALTER TABLE Videos
ADD MasterCourseID INT;  -- Change the data type accordingly if necessary

-- Step 2: Define the foreign key constraint
ALTER TABLE Videos
ADD CONSTRAINT FK_Videos_MasterCourseID FOREIGN KEY (MasterCourseID)
REFERENCES MasterCourse(MasterCourseID);


CREATE TABLE UserCourses (
    UserCourseID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    MasterCourseID INT NOT NULL,
    Status BIT NOT NULL DEFAULT 0, -- Default status to 0 (false) for not enrolled
    CONSTRAINT FK_UserCourses_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_UserCourses_MasterCourses FOREIGN KEY (MasterCourseID) REFERENCES MasterCourse(MasterCourseID)
);



CREATE TABLE UserMasterCourses (
    UserMasterCourseID INT PRIMARY KEY IDENTITY,
    UserID INT,
    MasterCourseID INT,
    CONSTRAINT FK_UserMasterCourses_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_UserMasterCourses_MasterCourse FOREIGN KEY (MasterCourseID) REFERENCES MasterCourse(MasterCourseID)
);



 ALTER TABLE Videos
DROP COLUMN VideoPath;

ALTER TABLE Videos
ADD YouTubeURL NVARCHAR(MAX);

 ALTER TABLE Videos
DROP COLUMN YoutubeUrl;

ALTER TABLE Videos
ADD YouTubeEmbedCode NVARCHAR(MAX);


use udemy;

select * from AdminUsers;
select * from Users;
select * from MasterCourse;
select * from Courses;
select * from Videos
select * from UserCourses;
select * from UserMasterCourses;
