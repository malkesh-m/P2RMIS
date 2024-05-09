CREATE procedure [dbo].[uspReportMultipleSubmission_LowestReceiptCycle]
(@ProgramList varchar(5000),
@FiscalYearList varchar(5000),
@CycleList varchar (5000))

as


Begin

with Programs(ProgramID) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@ProgramList)),
Years(FY) as (SELECT ParameterValue FROM dbo.SplitReportParameterString(@FiscalYearList)),
Cycles(CY) as (SELECT ParameterValue FROM dbo.SplitReportParameterInt(@CycleList))

select ReceiptCycle from (
select ViewProgramMechanism.ReceiptCycle, ROW_NUMBER() over (ORDER BY ViewProgramMechanism.ReceiptCycle) AS RowNo
	FROM         dbo.ClientProgram INNER JOIN
				 dbo.ViewProgramYear ON ClientProgram.ClientProgramId = ViewProgramYear.ClientProgramId INNER JOIN
				 dbo.ViewProgramMechanism ON dbo.ViewProgramYear.ProgramYearId = dbo.ViewProgramMechanism.ProgramYearId 
				 INNER JOIN
				 programs on programs.ProgramID= clientprogram.ClientProgramId join
				 Years on Years.FY =ViewProgramYear.Year join
				 Cycles on Cycles.CY =0 or Cycles.CY=ViewProgramMechanism.ReceiptCycle
group by ViewProgramMechanism.ReceiptCycle

--order by ViewProgramMechanism.ReceiptCycle
) a where RowNo = 1

end


GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspReportMultipleSubmission_LowestReceiptCycle] TO [NetSqlAzMan_Users]
    AS [dbo];

