
USE master;
GO

-- Declare the database name
DECLARE @DatabaseName NVARCHAR(255) = 'LicenseTrackDB';

-- Generate and execute the kill commands for all active connections
DECLARE @KillCommand NVARCHAR(MAX);

SET @KillCommand = (
    SELECT STRING_AGG('KILL ' + CAST(session_id AS NVARCHAR), '; ')
    FROM sys.dm_exec_sessions
    WHERE database_id = DB_ID(@DatabaseName)
);

IF @KillCommand IS NOT NULL
BEGIN
    EXEC sp_executesql @KillCommand;
    PRINT 'All connections to the database have been terminated.';
END
ELSE
BEGIN
    PRINT 'No active connections to the database.';
END
Go

IF EXISTS (SELECT * FROM sys.databases WHERE name = N'LicenseTrackDB')
BEGIN
    DROP DATABASE LicenseTrackDB;
END
Go
-- Create a login for the admin user
CREATE LOGIN [LicenseTrackAdminLogin] WITH PASSWORD = 'admin123';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [LicenseTrackAdminLogin];
Go

CREATE Database LicenseTrackDB;
Go


               --USE master;
               --DECLARE @latestBackupSet INT;
               --SELECT *
               --FROM msdb.dbo.backupset
               --WHERE database_name = 'LicenseTrackDB'
               --AND backup_set_id IN (
               --      SELECT backup_set_id
               --      FROM msdb.dbo.backupmediafamily
               --      WHERE physical_device_name = 'C:\Users\User\Source\Repos\LicenseTrackServer\LicenseTrackServer\wwwroot\..\DBScripts\backup.bak'
               --  )
               --ORDER BY backup_start_date DESC;
               -- ALTER DATABASE LicenseTrackDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
               -- RESTORE DATABASE LicenseTrackDB FROM DISK = 'C:\Users\User\Source\Repos\LicenseTrackServer\LicenseTrackServer\wwwroot\..\DBScripts\backup.bak' 
               -- WITH FILE=@latestBackupSet,
               -- REPLACE;
               -- ALTER DATABASE LicenseTrackDB SET MULTI_USER;