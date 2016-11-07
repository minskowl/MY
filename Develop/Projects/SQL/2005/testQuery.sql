DBCC DROPCLEANBUFFERS WITH NO_INFOMSGS 
DBCC FREEPROCCACHE  WITH NO_INFOMSGS 
DBCC FREESESSIONCACHE  WITH NO_INFOMSGS
DBCC FREESYSTEMCACHE  ( 'ALL' ) WITH NO_INFOMSGS
GO
DECLARE @START datetime
DECLARE @END datetime
SET @START =GETDATE()

select distinct 
Contacts.ContactId ,Email as [Email],FirstName as [First Name], ContactStatuses.Name as ContactStatus,LastName as [Last Name],
(select  StringValue from ContactFieldValues where ContactId = Contacts.ContactId and ContactFieldId = 25) as [Company Name]
from Contacts 
inner join ContactStatuses on Contacts.ContactStatusId = ContactStatuses.ContactStatusId
left join ContactListContact  on  Contacts.ContactId= ContactListContact.ContactId
left join ContactFieldValues on  Contacts.ContactId  = ContactFieldValues.ContactId 
where CustomerId=222 AND Contacts.ContactStatusId IN (1) AND ContactListContact.ContactListId IN (155)

SET @END =GETDATE()
SELECT DATEDIFF(millisecond,@START,@END)
GO