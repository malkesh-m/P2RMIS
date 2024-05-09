SET IDENTITY_INSERT [SystemConfiguration] ON

MERGE INTO [SystemConfiguration] AS Target
USING (VALUES
  (1,'Reset Contract on Update','Whether a user''s registration contract should be reset when their participant type has been updated.')
) AS Source ([SystemConfigurationId],[ConfigurationName],[ConfigurationDescription])
ON (Target.[SystemConfigurationId] = Source.[SystemConfigurationId])
WHEN MATCHED AND (
	NULLIF(Source.[ConfigurationName], Target.[ConfigurationName]) IS NOT NULL OR NULLIF(Target.[ConfigurationName], Source.[ConfigurationName]) IS NOT NULL OR 
	NULLIF(Source.[ConfigurationDescription], Target.[ConfigurationDescription]) IS NOT NULL OR NULLIF(Target.[ConfigurationDescription], Source.[ConfigurationDescription]) IS NOT NULL) THEN
 UPDATE SET
  [ConfigurationName] = Source.[ConfigurationName], 
  [ConfigurationDescription] = Source.[ConfigurationDescription]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([SystemConfigurationId],[ConfigurationName],[ConfigurationDescription])
 VALUES(Source.[SystemConfigurationId],Source.[ConfigurationName],Source.[ConfigurationDescription])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [SystemConfiguration]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[SystemConfiguration] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [SystemConfiguration] OFF
GO