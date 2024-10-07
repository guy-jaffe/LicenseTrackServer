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
Create Table AppUsers
(
	Id int Primary Key Identity,
	UserName nvarchar(50) Not Null,
	UserLastName nvarchar(50) Not Null,
	UserEmail nvarchar(50) Unique Not Null,
	UserPassword nvarchar(50) Not Null,
	IsManager bit Not Null Default 0
)
Insert Into AppUsers Values('admin', 'admin', 'kuku@kuku.com', '1234', 1)
Go
-- Create a login for the admin user
CREATE LOGIN [LicenseTrackAdminLogin] WITH PASSWORD = 'thePassword';
Go

-- Create a user in the YourProjectNameDB database for the login
CREATE USER [LicenseTrackAdminUser] FOR LOGIN [LicenseTrackAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [LicenseTrackAdminUser];
Go


-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=LicenseTrackDB;User ID=LicenseTrackAdminLogin;Password=thePassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context LicenseTrackDbContext -DataAnnotations –force