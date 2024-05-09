INSERT INTO [$(LinkedServerName)].[$(DatabaseName)].dbo.UserResume (UserResumeId, ResumeFile, DocType)
SELECT UserResumeId, ResumeFile, DocType
FROM UserResume
WHERE DeletedFlag = 0;
