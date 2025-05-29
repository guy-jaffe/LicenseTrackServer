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
    IsManager BIT NOT NULL DEFAULT 0,
    PhoneNum NVARCHAR(15) null
);

-- יצירת טבלת teachers
CREATE TABLE teachers 
(
    Id INT PRIMARY KEY REFERENCES users(Id) ,
    School_name NVARCHAR(50),
    Manual_car BIT,
    Vehicle_type NVARCHAR(50),
    Teaching_area NVARCHAR(50),
    ConfirmationStatus INT default(0)
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
    Comments NVARCHAR(1000),
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


Insert Into users Values('guy.jaffe@gmail.com','guy', 'jaffe', 'guy123', N'הוד השרון', 'png', 0, '0526344450')
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 10', null, 0)
Go

Insert Into users Values('ori.erlichman@gmail.com','ori', 'erlichman', 'ori123', N'הוד השרון', 'png', 0, '0526344450')
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 8', null, 1)
Go

Insert Into users Values('dan.bentov@gmail.com','dan', 'bentov', 'dan123', N'הוד השרון', 'png', 0, '0526344450')
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 6', '2025-05-19', 2)
Go

Insert Into users Values('ran.nurieli@gmail.com','ran', 'nurieli', 'ran123', N'הוד השרון', 'png', 0, '0526344450')
INSERT into teachers values(@@IDENTITY, N'בית ספר רמון', 0, N'טויוטה' ,N'הוד השרון',1)
Go

Insert Into users Values('noam.haner@gmail.com','noam', 'haner', 'noam123', N'הוד השרון', 'png', 1, '0526344450')
Go



Insert Into users Values('elad.jaffe@gmail.com','elad', 'jaffe', 'elad123', N'הוד השרון', 'png', 0, '0526344450')
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 5', null, 1)
Go

Insert Into users Values('amit.chacham@gmail.com','amit', 'chacham', 'amit123', N'הוד השרון', 'png', 0, '0526344450')
INSERT INTO students values(@@IDENTITY, N'גולדה מאיר 4', null, 1)
Go

Insert Into users Values('roee.naim@gmail.com','roee', 'naim', 'roee123', N'הוד השרון', 'png', 0, '0526344450')
INSERT into teachers values(@@IDENTITY, N'בית ספר מוסינזון', 1, N'מרצדס' ,N'הוד השרון',1)
Go

Insert Into users Values('liam.tartazki@gmail.com','liam', 'tartazki', 'liam123', N'הוד השרון', 'png', 0, '0526344450')
INSERT into teachers values(@@IDENTITY, N'בית ספר עתידים', 0, N'סוזוקי' ,N'הוד השרון',1)
Go

Insert Into users Values('alon.nahum@gmail.com','alon', 'nahum', 'alon123', N'הוד השרון', 'png', 0, '0526344450')
INSERT into teachers values(@@IDENTITY, N'בית ספר בגין', 1, N'ב.מ.ו' ,N'הוד השרון',1)
Go

Insert Into lessons Values('2025-05-25','09:00:00.0000000', N'שיעור רגיל', 2, '4', N'כל הכבוד')
Go

Insert Into lessons Values('2025-05-24','10:00:00.0000000', N'שיעור רגיל', 6, '4', N'מעולה')
Go

Insert Into lessons Values('2025-05-23','10:00:00.0000000', N'שיעור רגיל', 7, '4', N'מעולה')
Go

Insert Into lessons Values('2025-05-22','11:00:00.0000000', N'שיעור רגיל', 2, '4', N'אלוף')
Go

Insert Into lessons Values('2025-05-19','10:00:00.0000000', N'שיעור רגיל', 2, '4', N'מעולה')
Go

Insert Into lessons Values('2025-06-23','09:00:00.0000000', N'שיעור רגיל', 2, '4', N'כל הכבוד')
Go

Insert Into lessons Values('2025-06-22','10:00:00.0000000', N'שיעור רגיל', 6, '4', N'מעולה')
Go

Insert Into lessons Values('2025-06-21','10:00:00.0000000', N'שיעור רגיל', 7, '4', N'מעולה')
Go

Insert Into lessons Values('2025-06-20','11:00:00.0000000', N'שיעור רגיל', 2, '4', N'אלוף')
Go

Insert Into lessons Values('2025-06-19','10:00:00.0000000', N'שיעור רגיל', 2, '4', N'מעולה')
Go

UPDATE lessons
SET Comments=null
WHERE Id='6';

UPDATE lessons
SET Comments=null
WHERE Id='7';

UPDATE lessons
SET Comments=null
WHERE Id='8';

UPDATE lessons
SET Comments=null
WHERE Id='9';

UPDATE lessons
SET Comments=null
WHERE Id='10';










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
select * from lessons
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=admin123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LicenseTrackDbContext -DataAnnotations –force

*/