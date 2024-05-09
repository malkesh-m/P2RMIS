CREATE procedure [dbo].[uspReportReviewerList]
	(
		@ProgramList varchar(5000),
		@FiscalYearList varchar(5000),
		@PanelList varchar (5000)
	)
AS
BEGIN
	SET NOCOUNT ON;

		WITH Programs(ProgramID) AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
			Years(FY) AS (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
			Panel(PA)AS (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@PanelList))
		
		-- Insert statements for procedure here
		SELECT DISTINCT cp.ProgramDescription AS Program,
			vpy.[Year],
			vui.LastName,
			vui.FirstName, 
			vui.MiddleName,
			lp.PrefixName,
			vsp.PanelAbbreviation,
			vsp.PanelName,
			vui.SuffixText AS DegreeName,
			vui.Department,
			vui.Institution AS ReviewersOrgName,
			ua.Address1,
			ua.Address2,
			ua.City,
			st.StateAbbreviation,
			ua.Zip,
			ue.Email AS ReviewerEmail,
			up.Phone AS ReviewerPhone,
			uac.FirstName + ' ' + uac.LastName AS AlternateContactName,
			uacp.PhoneNumber AS AlternateContactNumber,
			uac.EmailAddress AS AlternateContactEmail,
			uv.VendorName AS W9Name,
			cpt.ParticipantTypeAbbreviation
		FROM dbo.ClientProgram cp
			INNER JOIN dbo.ViewProgramYear vpy ON vpy.ClientProgramId = cp.ClientProgramId 
			INNER JOIN dbo.ViewProgramPanel vpp ON vpp.ProgramYearId = vpy.ProgramYearId 
			INNER JOIN dbo.ViewSessionPanel vsp ON vsp.SessionPanelId = vpp.SessionPanelId 
			--Parameter List =========
			INNER JOIN Programs prg ON prg.ProgramID= cp.ClientProgramId 
			INNER JOIN Years yr ON yr.FY = vpy.[Year] 
			INNER JOIN Panel pnl ON pnl.PA = 0 OR pnl.PA = vsp.SessionPanelId
			--=========================
			INNER JOIN dbo.ViewPanelUserAssignment vpa ON vpa.SessionPanelId = vsp.SessionPanelId AND vpa.DeletedFlag = 0 --One2Many
			INNER JOIN dbo.ViewUserInfo vui ON vui.UserID = vpa.UserId
			INNER JOIN [dbo].[ClientParticipantType] cpt ON cpt.ClientParticipantTypeId = vpa.ClientParticipantTypeId
			INNER JOIN [dbo].[ViewUser] vwu ON vwu.UserId = vui.UserID
			LEFT JOIN [dbo].[Prefix] lp ON lp.PrefixID = vui.PrefixId
			LEFT JOIN [dbo].[UserAddress] ua ON ua.UserInfoID = vui.UserInfoID AND ua.PrimaryFlag = 1 AND ua.DeletedFlag = 0
			LEFT JOIN [dbo].[State] st ON st.StateId = ua.StateId
			LEFT JOIN [dbo].[UserAlternateContact] uac ON uac.UserInfoId = vui.UserInfoID AND uac.PrimaryFlag = 1 AND uac.DeletedFlag = 0
			LEFT JOIN [dbo].[UserEmail] ue ON ue.UserInfoID = vui.UserInfoID AND ue.PrimaryFlag = 1 AND ue.DeletedFlag = 0
			LEFT JOIN [dbo].[UserPhone] up ON up.UserInfoID = vui.UserInfoID AND up.PrimaryFlag = 1 AND up.DeletedFlag = 0
			LEFT JOIN [dbo].[UserAlternateContactPhone] uacp ON uacp.UserAlternateContactId = uac.UserAlternateContactId AND uacp.PrimaryFlag = 1 AND uacp.DeletedFlag = 0
			LEFT JOIN [dbo].[UserVendor] uv ON uv.UserInfoId = vui.UserInfoID AND uv.ActiveFlag = 1 AND uv.DeletedFlag = 0
		WHERE (cpt.ReviewerFlag = 1 OR (cpt.RTAFlag = 1 OR cpt.SROFlag = 1)) 
		ORDER BY vui.LastName, vui.FirstName
		

	END

Go
GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportReviewerList] TO [NetSqlAzMan_Users]
    AS [dbo];

