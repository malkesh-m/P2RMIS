--Need to use dynamic sql here in order to conditionally include
EXEC sp_executesql N'
CREATE TRIGGER [UserResumeLinkedServerTrigger]
ON UserResume
FOR INSERT, UPDATE
AS
BEGIN
DECLARE @UserResumeId int,
@ResumeFile varbinary(max),
@DocType varchar(20),
@DeletedFlag bit

SELECT @UserResumeId = UserResumeId, @ResumeFile = ResumeFile, @DocType = DocType, @DeletedFlag = DeletedFlag
FROM inserted;
--IF RECORD UPDATED
IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
BEGIN
EXEC [$(LinkedServerName)].[$(DatabaseName)].dbo.UpdateResume @UserResumeId, @ResumeFile, @DocType, @DeletedFlag;

END
--IF RECORD INSERTED
ELSE
BEGIN
EXEC [$(LinkedServerName)].[$(DatabaseName)].dbo.InsertResume @UserResumeId, @ResumeFile, @DocType;
END
END'