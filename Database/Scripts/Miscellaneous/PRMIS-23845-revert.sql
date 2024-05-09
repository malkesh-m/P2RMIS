UPDATE ProgramPanel SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10
FROM ProgramPanel INNER JOIN
ProgramYear ON ProgramPanel.ProgramYearId = ProgramYear.ProgramYearId
WHERE DateClosed = '1/1/2002';

UPDATE ProgramYear SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime(), DeletedBy = 10
WHERE DateClosed = '1/1/2002';

