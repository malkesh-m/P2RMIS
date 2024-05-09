/*Some explanation, with the meeting management release on 3/26/2019 the HotelRequiredFlag was flipped and began being treated as HotelNotRequiredFlag however legacy DB
values were not updated nor was the column name updated.  For least LOE, we are leaving logic the same (as it has been fully tested working) and updating the column name and historical 
data.  Data prior to 8/25/2003 was found to be NULL in the old database causing it to all come inadvertantly as HotelRequired = false which is not how it should have been mapped (99% of records do require hotel).
Anyway we can leave this oldest data as is as it wasn't captured in 1.0 and HotelNotRequired = false is more accurate.
*/

UPDATE MeetingRegistrationHotel SET HotelNotRequiredFlag = CASE WHEN HotelNotRequiredFlag = 1 THEN 0 ELSE 1 END
WHERE ModifiedDate > '8/25/2003' AND ModifiedDate < '3/14/2019'