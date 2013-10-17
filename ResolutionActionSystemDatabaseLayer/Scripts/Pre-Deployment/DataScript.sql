SET IDENTITY_INSERT Person ON;
--Person Inserts
INSERT INTO Person(PersonId, FirstName, LastName) VALUES (1,	'Steven'	,'VDM');
INSERT INTO Person(PersonId, FirstName, LastName) VALUES (2,	'Bob'		,'Smith');
INSERT INTO Person(PersonId, FirstName, LastName) VALUES (3,	'Tim'		,'Richards');
SET IDENTITY_INSERT Person OFF;

SET IDENTITY_INSERT MeetingType ON;
--Meeting Type Inserts
INSERT INTO MeetingType(MeetingTypeId, MeetingTypeName) VALUES (1,	'MANCO');			
INSERT INTO MeetingType(MeetingTypeId, MeetingTypeName) VALUES (2,	'Financial');
INSERT INTO MeetingType(MeetingTypeId, MeetingTypeName) VALUES (3,	'Project Team Leader');
SET IDENTITY_INSERT MeetingType OFF;

SET IDENTITY_INSERT MeetingItemStatusLu ON;
--Meeting Item Status Lu Inserts
INSERT INTO MeetingItemStatusLu(MeetingItemStatusLuId, MeetingItemStatusDesc) VALUES (1,	'Created');
INSERT INTO MeetingItemStatusLu(MeetingItemStatusLuId, MeetingItemStatusDesc) VALUES (2,	'Work In Progress');
INSERT INTO MeetingItemStatusLu(MeetingItemStatusLuId, MeetingItemStatusDesc) VALUES (3,	'Halted');
INSERT INTO MeetingItemStatusLu(MeetingItemStatusLuId, MeetingItemStatusDesc) VALUES (4,	'Resolved');
SET IDENTITY_INSERT MeetingItemStatusLu OFF;

SET IDENTITY_INSERT MeetingItem ON;
--Meeting Item Inserts
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (1,	'Analyse Spec 1'						,'25-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (2,	'Investigate new Development strategy'	,'25-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (3,	'Create reusable components	'			,'30-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (4,	'Analyse ROI on new project'			,'23-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (5,	'Recruit more developers'				,'27-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (6,	'Meeting Item 6'						,'27-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (7,	'Meeting Item 7'						,'27-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (8,	'Meeting Item 8'						,'27-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (9,	'Meeting Item 9' 						,'27-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (10,	'Meeting Item 10'						,'27-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (11,	'Meeting Item 11'						,'27-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (12,	'Meeting Item 12'						,'27-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (13,	'Analyse ROI on new project'			,'23-Oct-13 00:00:00'	,1);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (14,	'Meeting Item 6'						,'27-Oct-13 00:00:00'	,2);
INSERT INTO MeetingItem(MeetingItemId, MeetingItemDesc, MeetingItemDueDate, PersonResponsible_PersonId) VALUES (15,	'Meeting Item 7' 						,'27-Oct-13 00:00:00'	,1);
SET IDENTITY_INSERT MeetingItem OFF;

SET IDENTITY_INSERT Meeting ON;
--Meeting Inserts
INSERT INTO Meeting(MeetingId, MeetingNumber, MeetingDate, MeetingType_MeetingTypeId, PreviousMeeting_MeetingId) VALUES (1,	1	,'16-Oct-13 00:00:00'	,1	,NULL);
INSERT INTO Meeting(MeetingId, MeetingNumber, MeetingDate, MeetingType_MeetingTypeId, PreviousMeeting_MeetingId) VALUES (2,	1	,'17-Oct-13 00:00:00'	,2	,NULL);
INSERT INTO Meeting(MeetingId, MeetingNumber, MeetingDate, MeetingType_MeetingTypeId, PreviousMeeting_MeetingId) VALUES (3,	1	,'17-Oct-13 00:00:00'	,3	,NULL);
INSERT INTO Meeting(MeetingId, MeetingNumber, MeetingDate, MeetingType_MeetingTypeId, PreviousMeeting_MeetingId) VALUES (4,	2	,'19-Oct-13 00:00:00'	,2	,2  );
SET IDENTITY_INSERT Meeting OFF;

SET IDENTITY_INSERT MeetingItemStatus ON;
--Meeting Item Status Inserts
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (1,	'10-Oct-13 00:00:00'	,1	,1	,1);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (2,	'10-Oct-13 00:00:00'	,1	,1	,2);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (3,	'10-Oct-13 00:00:00'	,1	,1	,3);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (4,	'10-Oct-13 00:00:00'	,1	,1	,4);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (5,	'10-Oct-13 00:00:00'	,1	,1	,5);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (6,	'10-Oct-13 00:00:00'	,1	,1	,6);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (7,	'10-Oct-13 00:00:00'	,1	,1	,7);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (8,	'10-Oct-13 00:00:00'	,1	,1	,8);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (9,	'10-Oct-13 00:00:00'	,1	,2	,4);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (10,	'10-Oct-13 00:00:00'	,1	,2	,6);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (11,	'10-Oct-13 00:00:00'	,1	,2	,7);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (12,	'16-Oct-13 00:40:11'	,1	,3	,9);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (13,	'16-Oct-13 00:40:11'	,1	,3	,10);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (14,	'16-Oct-13 00:40:11'	,1	,3	,11);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (15,	'17-Oct-13 09:06:59'	,1	,4	,12);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (16,	'17-Oct-13 09:06:59'	,1	,4	,13);
INSERT INTO MeetingItemStatus(MeetingItemStatusId, MeetingItemStatusDate, MeetingItemStatusLu_MeetingItemStatusLuId, Meeting_MeetingId, MeetingItem_MeetingItemId) VALUES (17,	'17-Oct-13 09:06:59'	,1	,4	,14);
SET IDENTITY_INSERT MeetingItemStatus OFF;

SET IDENTITY_INSERT MeetingAction ON;
--Meeting Action Inserts
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (1, 'Item Task 1',	1,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (2, 'Item Task 2',	1,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (3, 'Item Task 3',	1,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (4, 'Item Task 4',	1,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (5, 'Item Task 1',	2,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (6, 'Item Task 2',	2,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (7, 'Item Task 3',	2,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (8, 'Item Task 4',	2,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (9, 'Item Task 1',	3,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (10, 'Item Task 2',	3,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (11, 'Item Task 3',	3,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (12, 'Item Task 4',	3,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (13, 'Item Task 1',	4,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (14, 'Item Task 2',	4,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (15, 'Item Task 3',	4,	1);
INSERT INTO MeetingAction(MeetingActionId, ActionDescription, MeetingItemStatus_MeetingItemStatusId, PersonResponsible_PersonId) VALUES (16, 'Item Task 4',	4,	1);
SET IDENTITY_INSERT MeetingAction OFF;