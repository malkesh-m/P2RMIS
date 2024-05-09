CREATE TRIGGER [PplDegreesSyncTrigger]
ON [$(P2RMIS)].[dbo].[PPL_Degrees]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[UserDegree]
	SET DegreeId = Degree.DegreeId, Major = inserted.Major,
	ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[UserDegree] UserDegree ON inserted.Degree_ID = UserDegree.LegacyDegreeId INNER JOIN
		[$(DatabaseName)].[dbo].[Degree] Degree ON CASE WHEN inserted.Degree = 'M.D.,Ph.D.' THEN 'M.D./Ph.D.' ELSE inserted.Degree END = Degree.DegreeName LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	WHERE UserDegree.DeletedFlag = 0


	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].UserDegree
	([UserInfoId]
           ,[DegreeId]
           ,[LegacyDegreeId]
           ,[Major]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT UserInfo.UserInfoID, Degree.DegreeId, inserted.Degree_ID, inserted.Major, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonID INNER JOIN
		[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserId = UserInfo.UserID INNER JOIN
		[$(DatabaseName)].[dbo].[Degree] Degree ON CASE WHEN inserted.Degree = 'M.D.,Ph.D.' THEN 'M.D./Ph.D.' ELSE inserted.Degree END = Degree.DegreeName LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
	
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[UserDegree]
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].[dbo].[UserDegree] UserDegree ON deleted.Degree_ID = UserDegree.LegacyDegreeId
	WHERE UserDegree.DeletedFlag = 0
END
