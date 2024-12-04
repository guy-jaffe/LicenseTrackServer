Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'LicenseTrackDB')
BEGIN
    DROP DATABASE LicenseTrackDB;
END
Go
Create Database LicenseTrackDB
Go
Use LicenseTrackDB
Go

-- יצירת טבלת users
CREATE TABLE users 
(
    Id INT PRIMARY KEY Identity,
    Email NVARCHAR(250) NOT NULL,
    First_name NVARCHAR(50) NOT NULL,
    Last_name NVARCHAR(50) NOT NULL,
    Pass NVARCHAR(50) NOT NULL,
    City NVARCHAR(50),
    File_extension NVARCHAR(50),
    IsManager BIT NOT NULL DEFAULT 0
);

-- יצירת טבלת teachers
CREATE TABLE teachers 
(
    Id INT PRIMARY KEY REFERENCES users(Id) ,
    School_name NVARCHAR(50),
    Manual_car BIT,
    Vehicle_type NVARCHAR(50),
    Teaching_area NVARCHAR(50),
    ConfirmationStatus BIT default(0)
);

-- יצירת טבלת students
CREATE TABLE students 
(
    Id INT PRIMARY KEY REFERENCES users(Id),
    Street NVARCHAR(50),
    License_acquisition_date DATE NULL,
    License_status INT default(0) -- 'בתיאוריה', 'בשיעורים', 'עם רישיון'
);

-- יצירת טבלת lessons
CREATE TABLE lessons 
(
    Id INT PRIMARY KEY Identity,
    LessonDate DATE,
    LessonTime TIME,
    LessonType NVARCHAR(50),
    Student_id INT,
    Instructor_id INT,
    Comments TEXT,
    FOREIGN KEY (Student_id) REFERENCES students(Id),
    FOREIGN KEY (Instructor_id) REFERENCES teachers(Id)
);

-- יצירת טבלת teacher_work_hours
CREATE TABLE teacher_work_hours 
(
    Teacher_id INT,
    DayDate DATE,
    Start_time TIME,
    End_time TIME,
    PRIMARY KEY (Teacher_id, DayDate, Start_time),
    FOREIGN KEY (Teacher_id) REFERENCES teachers(Id)
);


Insert Into users Values('email@12.com','admin', 'admin', '1234', N'הוד השרון', 'png', 1)
Go

Insert Into users Values('teacher@12.com','teacher', 'teacher', '1234', N'הוד השרון', 'png', 1)
INSERT into teachers values(@@IDENTITY, N'בית ספר רמון', 0, N'טויוטה' ,N'הוד השרון',1)
Go
Insert Into users Values('student@12.com','student', 'student', '1234', N'הוד השרון', 'png', 1)
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 10', null, 0)
Go

-- Create a login for the admin user
CREATE LOGIN [LicenseTrackAdminLogin] WITH PASSWORD = 'admin123';
Go

-- Create a user in the YourProjectNameDB database for the login
CREATE USER [LicenseTrackAdminUser] FOR LOGIN [LicenseTrackAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [LicenseTrackAdminUser];
Go

select * from users
select * from students
select * from teachers
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=admin123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LicenseTrackDbContext -DataAnnotations –force

*/