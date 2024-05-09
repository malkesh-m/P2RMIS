--Delete all old/irrelevant data in 1.0
--Application Assignments
DECLARE @LogNumber varchar(12),
@LegacyPartId int
DECLARE @SuppressOutput table (newvalue int)
DECLARE legacy_assignment_cursor CURSOR  
    FOR SELECT Assignments.Log_No, Assignments.PRG_Part_ID FROM [$(P2RMIS)].[dbo].PRG_Proposal_Assignments Assignments
	INNER JOIN [$(P2RMIS)].[dbo].PRG_Participants Participants ON Assignments.PRG_Part_ID = Participants.PRG_Part_ID 
	WHERE  (Participants.FY <= 2012) OR Participants.Program NOT IN ('CPRIT RES','CPRIT PRV','CPRIT REC', 'CPRIT PDEV', 'BCRP', 'PCRP', 'ARP', 'PRCRP')
OPEN legacy_assignment_cursor
FETCH NEXT FROM legacy_assignment_cursor INTO @LogNumber, @LegacyPartId; 
WHILE @@FETCH_STATUS = 0  
BEGIN
INSert intO @SuppressOutput
EXEC [$(P2RMIS)].[dbo].stpr_Rev_Assignments_Delete @Log = @LogNumber, @DeletePrgPartId = @LegacyPartId;
FETCH NEXT FROM legacy_assignment_cursor INTO @LogNumber, @LegacyPartId; 
END
CLOSE legacy_assignment_cursor;
DEALLOCATE legacy_assignment_cursor;
--Participation

--Application assignments


