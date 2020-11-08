use ChatDB;

/*
drop table [dbo].[Message]
drop table [dbo].[ChatsParticipants]
drop table [dbo].[Chats]
drop procedure [dbo].[CreateChat]
drop procedure [dbo].[SendMessage]
drop procedure  [dbo].[GetChatMessages]
*/

CREATE TABLE [dbo].[Chats]-- return chat model
(
ID int identity primary key clustered not null,
CreatedDate datetime default GETDATE(),
ChatTableName varchar(200) not null -- GUID - передается сюда системой
)

GO

CREATE TABLE [dbo].[ChatsParticipants]
(
ChatID int foreign key references [dbo].[Chats] (ID) not null,
UserParticipantId int foreign key references [dbo].[Users] (ID) not null
)

GO

CREATE TABLE [dbo].[Message]
(
ID int identity primary key clustered not null,
ChatId int foreign key references [dbo].[Chats] (ID) not null,
AuthorUserId int foreign key references [dbo].[Users] (ID) not null,
Text varchar(100) not null,
Date datetime default GETDATE(),
isNew bit default 1

)

-----------------------------------------------------------------------
GO

CREATE PROCEDURE [dbo].[CreateChat] -- return model
@UsrFrom int,
@UsrTo int,
@ChatTableName varchar(200)
AS

--users correct
IF NOT EXISTS(SELECT 1 FROM [dbo].[Users] NOLOCK WHERE ID = @UsrFrom OR ID = @UsrTo)
RAISERROR ('Illegal operation', 10, 1);

--check if chat already exists 
IF @ChatExistedId IS NULL
	BEGIN;
		INSERT INTO [dbo].[Chats] VALUES (GETDATE(), @ChatTableName)
		DECLARE  @ChatID  INT = SCOPE_IDENTITY()
		INSERT INTO [dbo].[ChatsParticipants] VALUES(@ChatID, @UsrFrom)
		INSERT INTO [dbo].[ChatsParticipants] VALUES(@ChatID, @UsrTo)
			--create chat table
		DECLARE @sql NVARCHAR(500) = 'CREATE TABLE ' + @ChatTableName 
		+ '( [ID] [int]  NOT NULL,
		[MessageId] [int] FOREIGN KEY REFERENCES [dbo].[Message] (ID) NOT NULL	 )'
		EXECUTE sp_executesql @sql
		SELECT * FROM [dbo].[Chats] WHERE ChatTableName = @ChatTableName;
	END;
ELSE
	SELECT * FROM [dbo].[Chats] WHERE ID = @ChatExistedId

-----------------------------------------------------------------------
GO

CREATE PROCEDURE [dbo].[SendMessage]
@ChatId int,
@UserFrom int,
@Text varchar(100),
@ChatTableName varchar(200)
AS
INSERT INTO [dbo].[Message] VALUES (@ChatId, @UserFrom, @Text, GETDATE(), 1);
DECLARE @MessageId INT = SCOPE_IDENTITY();

/*
DECLARE @sql NVARCHAR(500) = 
'INSERT INTO ' + @ChatTableName + ' VALUES (' + Convert(Varchar(100),@ChatId) + ', ' + Convert(Varchar(100),@MessageId) + ')'
EXECUTE sp_executesql @sql
*/

SELECT * FROM [dbo].[Message] WHERE ID = @MessageId
-----------------------------------------------------------------------
GO

CREATE PROCEDURE [dbo].[GetChatMessages]
@ChatId int,
@GetFullHistory bit = 0
AS

DECLARE @isNewMessage bit = 1;

	DECLARE @ResultTable table (ID int, Date datetime, Text varchar(100), UserFrom varchar(50), isNew bit)

	INSERT INTO @ResultTable
	SELECT 
		M.ID,
		Date,
		Text, 
		(SELECT Login FROM [dbo].[Users] U1 WHERE U1.ID = M.AuthorUserId) as UserFrom,
		isNew
		FROM [dbo].[Chats] as C
		JOIN [dbo].[Message] as M
			 ON C.ID = M.ChatId 
		WHERE 
			C.ID = @ChatId
		--	AND M.isNew = @isNewMessage
		ORDER BY Date DESC

		--update [dbo].[Message] - set new message as readed
		UPDATE [dbo].[Message] 
		SET isNew = 0
		WHERE ID IN (SELECT ID FROM @ResultTable)

		--result
		IF @GetFullHistory = 0 --only new
			SELECT Date,Text, UserFrom FROM @ResultTable WHERE isNew = @isNewMessage
		ELSE
			SELECT Date,Text, UserFrom FROM @ResultTable
			

-----------------------------------------------------------------------
GO

CREATE PROCEDURE [dbo].[GetChatId] -- find chat already exists
@UserFrom int,
@UserTO int
AS
DECLARE @ChatID INT = (
	SELECT P1.ChatID 
	FROM [dbo].[ChatsParticipants]  P1
	JOIN [dbo].[ChatsParticipants]  P2 ON P1.ChatID = P2.ChatID
	WHERE P1.UserParticipantId = @UserFrom
		AND P2.UserParticipantId = @UserTO
);

SELECT * FROM [dbo].[Chats] C WHERE C.ID = @ChatID