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
    id INT PRIMARY KEY Identity,
    email NVARCHAR(250) NOT NULL,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    pass NVARCHAR(50) NOT NULL,
    city NVARCHAR(50),
    file_extension NVARCHAR(50),
    IsManager BIT NOT NULL DEFAULT 0
);

-- יצירת טבלת teachers
CREATE TABLE teachers 
(
    id INT PRIMARY KEY,
    school_name NVARCHAR(50),
    manual_car BIT,
    vehicle_type NVARCHAR(50),
    teaching_area NVARCHAR(50),
    ConfirmationStatus BIT
);

-- יצירת טבלת students
CREATE TABLE students 
(
    id INT PRIMARY KEY,
    lesson_count INT,
    street NVARCHAR(50),
    license_acquisition_date DATE,
    license_status INT -- 'בתיאוריה', 'בשיעורים', 'עם רישיון'
);

-- יצירת טבלת lessons
CREATE TABLE lessons 
(
    id INT PRIMARY KEY Identity,
    lessonDate DATE,
    lessonTime TIME,
    lessonType NVARCHAR(50),
    student_id INT,
    instructor_id INT,
    comments TEXT,
    FOREIGN KEY (student_id) REFERENCES students(id),
    FOREIGN KEY (instructor_id) REFERENCES teachers(id)
);

-- יצירת טבלת teacher_work_hours
CREATE TABLE teacher_work_hours 
(
    teacher_id INT,
    dayDate DATE,
    start_time TIME,
    end_time TIME,
    PRIMARY KEY (teacher_id, dayDate, start_time),
    FOREIGN KEY (teacher_id) REFERENCES teachers(id)
);


Insert Into users Values('email@12.com','admin', 'admin', '1234', N'הוד השרון', 'png', 1)
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

/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=admin123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LicenseTrackDbContext -DataAnnotations –force

*/