use ChatDB;

--DROP TABLE [dbo].[Users]
--DROP PROCEDURE [dbo].[BlockUser]

CREATE TABLE [dbo].[Users]
(
	ID int identity primary key clustered not null,
	Login varchar(50) not null,
	Password varchar(50) not null,
	isBloked bit default 0,
	CreatedDate datetime default GETDATE()
)

--------------------------------------------
GO

CREATE PROCEDURE [dbo].[CreateUser]
@Login varchar(50),
@Password varchar(50)
AS
DECLARE @ID INT = (SELECT ID FROM [dbo].[Users] WHERE Login = @Login)

IF @ID IS NOT NULL
	RAISERROR ('User already exists', 15, 1);
ELSE
BEGIN
	INSERT INTO [dbo].[Users] VALUES (@Login, @Password, 0, GETDATE())
	SELECT * FROM [dbo].[Users] WHERE ID = SCOPE_IDENTITY()
END
--------------------------------------------
GO

CREATE PROCEDURE [dbo].[BlockUser]
@Login varchar(50),
@BlockValue bit
AS
UPDATE [dbo].[Users] 
SET isBloked = @BlockValue
WHERE Login = @Login