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
--Create Table AppUsers
--(
--	Id int Primary Key Identity,
--	UserName nvarchar(50) Not Null,
--	UserLastName nvarchar(50) Not Null,
--	UserEmail nvarchar(50) Unique Not Null,
--	UserPassword nvarchar(50) Not Null,
--	IsManager bit Not Null Default 0
--)
--Insert Into AppUsers Values('admin', 'admin', 'kuku@kuku.com', '1234', 1)
--Go
---- Create a login for the admin user
--CREATE LOGIN [LicenseTrackAdminLogin] WITH PASSWORD = 'thePassword';
--Go

---- Create a user in the YourProjectNameDB database for the login
--CREATE USER [LicenseTrackAdminUser] FOR LOGIN [LicenseTrackAdminLogin];
--Go

---- Add the user to the db_owner role to grant admin privileges
--ALTER ROLE db_owner ADD MEMBER [LicenseTrackAdminUser];
--Go


-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=thePassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LicenseTrackDbContext -DataAnnotations –force

--יצירת טבלה users
CREATE TABLE users 
(
    id INT PRIMARY KEY Identity,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    pass VARCHAR(50) NOT NULL,
    city VARCHAR(50),
    profile_picture_id INT,
    IsManager bit Not Null Default 0,
    FOREIGN KEY (profile_picture_id) REFERENCES images(id)
);

-- יצירת טבלת teachers
CREATE TABLE teachers (
    id INT PRIMARY KEY Identity,
    userID INT,
    school_name VARCHAR(50),
    manual_car BIT, 
    vehicle_type VARCHAR(50),
    teaching_area VARCHAR(50),
    ConfirmationStatus BIT,
    FOREIGN KEY (userID) REFERENCES users(id)
);

-- יצירת טבלת students
CREATE TABLE students 
(
    id INT PRIMARY KEY Identity,
    userID INT,
    lesson_count INT,
    street VARCHAR(50),
    license_acquisition_date DATE,
    license_status INT,  --'בתיאוריה', 'בשיעורים', 'עם רישיון'
    FOREIGN KEY (userID) REFERENCES users(id)
);

-- יצירת טבלת images
CREATE TABLE images 
(
    id INT PRIMARY KEY,
    file_extension VARCHAR(50)
);

-- יצירת טבלת lessons
CREATE TABLE lessons 
(
    id INT PRIMARY KEY Identity,
    lessonDate DATE,
    lessonTime TIME,
    student_id INT,
    instructor_id INT,
    comments TEXT,
    FOREIGN KEY (student_id) REFERENCES students(id),
    FOREIGN KEY (instructor_id) REFERENCES teachers(id)
);

-- יצירת טבלת teacher_work_hours (ללא עמודת ID)
CREATE TABLE teacher_work_hours 
(
    teacher_id INT,
    dayDate DATE,
    start_time TIME,
    end_time TIME,
    FOREIGN KEY (teacher_id) REFERENCES teachers(id),
    PRIMARY KEY (teacher_id, date, start_time)  -- קביעת מפתח ראשי המורכב ממורה, תאריך ושעת התחלה
);

Insert Into users Values('admin', 'admin', '1234', N'הוד השרון', 1, 1)
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