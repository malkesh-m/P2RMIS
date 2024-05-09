-- This is a preparation script so we can enforce a unique filtered index over 
--   [PanelApplicationId, ReviewStatusId] WHERE DeletedFlag=0 at a later time
-- Run this multiple times till there are no more affected rows
UPDATE A SET
A.DeletedDate=DateAdd(s, 1, A.DeletedDate)
from ApplicationReviewStatus A where A.DeletedFlag=1 and 
exists (select * from ApplicationReviewStatus B where B.DeletedFlag=1 and 
A.PanelApplicationId=B.PanelApplicationId and A.ReviewStatusId=B.ReviewStatusId and 
A.DeletedDate = B.DeletedDate and A.ApplicationReviewStatusId > B.ApplicationReviewStatusId)

-- Run this to verify there are no duplicate deleted records
/*
select * from ApplicationReviewStatus A where A.DeletedFlag=1 and 
exists (select * from ApplicationReviewStatus B where B.DeletedFlag=1 and 
A.PanelApplicationId=B.PanelApplicationId and A.ReviewStatusId=B.ReviewStatusId and 
A.DeletedDate = B.DeletedDate and A.ApplicationReviewStatusId > B.ApplicationReviewStatusId)
*/