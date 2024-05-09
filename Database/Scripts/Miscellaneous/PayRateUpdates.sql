ALTER TABLE PRG_Part_Pay_Rate
  ADD SRA_Managers VARCHAR(1000) NULL,
      Work_Description VARCHAR(40) NULL;

CREATE TABLE [$(P2RMIS)].[dbo].[PRG_Part_Pay_Rate_Session](
	[Part_Type] [nvarchar](10) NOT NULL,
	[Session_ID] [nvarchar](10) NOT NULL,
	[EC_ID] [int] NOT NULL,
	[Period_Start] [smalldatetime] NOT NULL,
	[Period_End] [smalldatetime] NOT NULL,
	[Max_Days] [smallint] NULL,
	[Max_Docs] [smallint] NULL,
	[Fixed_Amount] [money] NULL,
	[Rate_Day] [money] NULL,
	[Rate_Doc] [money] NULL,
	[Consultant_Fee] [nvarchar](500) NULL,
	[Task_Code] [nvarchar](20) NULL,
	[WO_Filename] [nvarchar](100) NULL,
	[SRA_Managers] [nvarchar] (1000) Null,
	[Work_Description] [nvarchar] (500) null,
	[LAST_UPDATE_DATE] [smalldatetime] NULL,
	[LAST_UPDATED_BY] [nvarchar](30) NULL,
 CONSTRAINT [PK_PRG_Part_Pay_Rate_Session] PRIMARY KEY CLUSTERED 
(
	[Part_Type] ASC,
	[Session_ID] ASC,
	[EC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
