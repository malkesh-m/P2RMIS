-- Soft delete invalid peer review documents
UPDATE PeerReviewDocument SET DeletedFlag=1, DeletedBy=10, DeletedDate=dbo.GetP2rmisDateTime() WHERE
DeletedFlag=0 AND PeerReviewContentTypeId=1 AND ContentFilelocation IS NULL