using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolutionActionSystemContext;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;

namespace ResolutionActionSystemTest
{
    [TestClass]
    public class ResolutionActionTest
    {
        [TestMethod]
        public void ConstructMeetingType_GivenMancoType_ShouldReturnTrue()
        {
            // Setup --------------------
            var db = new Context();

            // Run Test --------------------
            var mancoMeetingType = CreateMancoMeetingType();
            db.MeetingTypes.Add(mancoMeetingType);

            // Validate Test --------------------
            var hasMancoMeetingType = db.MeetingTypes.Local.Count(p => p.MeetingTypeName.ToUpper() == "MANCO") > 0;

            // Assert --------------------
            Assert.IsTrue(hasMancoMeetingType);
        }
        
        [TestMethod]
        public void ConstructMeeting_GivenTypeNumberDate_ShouldReturnTrue()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();

            // Run Test --------------------
            CreateMeeting(uc, mancoMeetingType, db);

            // Validate Test --------------------
            var hasMeeting = db.Meetings.Local.Count(p => p.MeetingNumber == 1) > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeeting);
        }

        private static void CreateMeeting(MeetingUseCase uc, MeetingType mancoMeetingType, Context db)
        {
            uc.CreateNewMeeting();
            var meeting = CreateMeeting(uc, mancoMeetingType);
            db.Meetings.Add(meeting);
        }

        [TestMethod]
        public void ConstructMeetingItemStatus_GivenStatus_ShouldReturnTrue()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var meetingItem = CreateMeetingItem();

            // Run Test --------------------
            MeetingItemStatus meetingItemStatus;
            CreateMeetingItemStatus(meeting, meetingItem, db, out meetingItemStatus);

            // Validate Test --------------------
            var meetingItemStatusOfCreated = db.MeetingItemStatuses.Local.FirstOrDefault(p => p.Meeting.MeetingId == meeting.MeetingId && p.MeetingItem.MeetingItemId == meetingItem.MeetingItemId);

            bool hasMeetingItemStatusOfCreated = meetingItemStatusOfCreated != null;

            // Assert --------------------
            Assert.IsTrue(hasMeetingItemStatusOfCreated);
        }

        

        [TestMethod]
        public void AddMeetingItem_GivenMeeting_ShouldReturnMeeting_MeetingStatuses_MeetingItems()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var person = new Person("Bob", "Smith");

            // Run Test --------------------
            uc.AddNewMeetingItem("EFv2", DateTime.Now,person);
            

            // Validate Test --------------------
            var meetingItemExists =
                 uc.Current.MeetingItemStatuses.Count(p => p.MeetingItem.MeetingItemDesc.ToUpper() == "EFv2".ToUpper()) > 0;

            // Assert --------------------
            Assert.IsTrue(meetingItemExists);
        }

        

        [TestMethod]
        public void PrintMeetingMinutes_GivenMeetingId1_ShouldReturnMinutes()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var meetingItem = CreateMeetingItem();

            MeetingItemStatus meetingItemStatus;
            CreateMeetingItemStatus(meeting, meetingItem, db, out meetingItemStatus);

            // Run Test --------------------
            List<MeetingMinute> meetingMinutes = uc.GetCurrentMeetingMinutes();

            // Validate Test --------------------
            var hasMeetingMinutes = meetingMinutes.Count > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeetingMinutes);

            // Debug Output --------------------
            string allMinutes = meetingMinutes.Aggregate("", (current, meetingMinute) => current + (meetingMinute.ToString() + "\r\n"));
            Debug.Print(allMinutes);
        }

        [TestMethod]
        public void UpdateCurrentMeetingItemPerson_GivenMeetingAndMeetingItem_ShouldReturnUpdatedItemName()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var meetingItem = CreateMeetingItem();

            MeetingItemStatus meetingItemStatus;
            CreateMeetingItemStatus(meeting, meetingItem, db, out meetingItemStatus);

            if (meetingItemStatus == null) Assert.Fail("No Meeting Item available");
            int meetingItemStatusId = meetingItemStatus.MeetingItemStatusId;
            uc.CurrentMeetingItem = uc.GetMeetingMinute(meetingItemStatusId);

            // Run Test --------------------
            var personResponsible = new Person("Bob","Smith");
            uc.UpdateCurrentMeetingItem_PersonResponsible(personResponsible);

            // Validate Test --------------------
            var updatedMeetingMinute = uc.GetMeetingMinute(meetingItemStatusId);
            var hasUpdatedItemToBob = updatedMeetingMinute.PersonResponsibleName == "Bob Smith";

            // Assert --------------------
            Assert.IsTrue(hasUpdatedItemToBob);
        }

        [TestMethod]
        public void UpdateCurrentMeetingItemStatus_GivenMeetingAndMeetingItem_ShouldReturnUpdatedStatus()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var meetingItem = CreateMeetingItem();

            MeetingItemStatus meetingItemStatus;
            CreateMeetingItemStatus(meeting, meetingItem, db, out meetingItemStatus);

            var meetingItemStatusLu = new MeetingItemStatusLu();
            meetingItemStatusLu.MeetingItemStatusDesc = "WIP";
            db.MeetingItemStatusLus.Add(meetingItemStatusLu);

            int meetingItemStatusId = meetingItemStatus.MeetingItemStatusId;
            uc.CurrentMeetingItem = new MeetingMinute(meetingItemStatus);

            // Run Test --------------------;
            uc.CurrentMeetingItem.UpdateMeetingItemStatus(meetingItemStatusLu);

            // Validate Test --------------------
            var updatedMeetingMinute = uc.GetMeetingMinute(meetingItemStatusId);
            var hasUpdatedItemStatusToWip = updatedMeetingMinute.Status == "WIP";

            // Assert --------------------
            Assert.IsTrue(hasUpdatedItemStatusToWip);
        }

        [TestMethod]
        public void ConstructMeetingItem_GivenDescription_ShouldReturnMeetingItem()
        {
            // Setup --------------------
            var db = new Context();
            var meetingItem = new MeetingItem();
            meetingItem.MeetingItemDesc = "Learn WPF";
            meetingItem.MeetingItemDueDate = DateTime.Now.AddDays(2);

            // Run Test --------------------
            db.MeetingItems.Add(meetingItem);

            // Validate Test --------------------
            var hasMeetingItemWPF = db.MeetingItems.Local.Count(p => p.MeetingItemDesc.ToUpper() == "Learn WPF".ToUpper()) > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeetingItemWPF);
        }

        [TestMethod]
        public void ConstructMeetingAction_GivenMeetingItemStatus_ShouldReturnMeetingAction()
        {
            // Setup --------------------
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = CreateMancoMeetingType();
            CreateMeeting(uc, mancoMeetingType, db);
            var meeting = uc.Current;
            var meetingItem = CreateMeetingItem();

            MeetingItemStatus meetingItemStatus;
            CreateMeetingItemStatus(meeting, meetingItem, db, out meetingItemStatus);

            // Run Test --------------------
            var meetingAction = new MeetingAction();
            meetingAction.ActionDescription = "Learn TDD2";
            meetingAction.MeetingItemStatus = meetingItemStatus;
            db.MeetingActions.Add(meetingAction);

            // Validate Test --------------------
            var hasMeetingAction =
                meetingItemStatus.MeetingActions.Count(p => p.ActionDescription.ToUpper() == "Learn TDD2".ToUpper()) > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeetingAction);
        }

        [TestMethod]
        public void NewMeeting_ShouldSetCurrentMeeting()
        {
            // Setup --------------------
            var uc = new MeetingUseCase(new Context());

            // Run Test --------------------
            uc.CreateNewMeeting();

            // Validate Test --------------------
            var newCurrentMeeting = uc.Current != null;

            // Assert --------------------
            Assert.IsTrue(newCurrentMeeting);
        }

        [TestMethod]
        public void ConstructMeetingStatusLu_ShouldReturnMeetingStatusLu()
        {
            // Setup --------------------
            var db = new Context();

            // Run Test --------------------
            var meetingStatusLu = new MeetingItemStatusLu();
            meetingStatusLu.MeetingItemStatusDesc = "CREATED";
            db.MeetingItemStatusLus.Add(meetingStatusLu);

            // Validate Test --------------------
            var hasMeetingItemStatusLuWIP = db.MeetingItemStatusLus.Local.Count(p => p.MeetingItemStatusDesc.ToUpper() == "CREATED".ToUpper()) > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeetingItemStatusLuWIP);
        }

        [TestMethod]
        public void ConstructPerson_GivenName_ShouldReturnPerson()
        {
            // Setup --------------------
            var db = new Context();

            // Run Test --------------------
            var person = new Person("Steven","van der Merwe");
            db.Persons.Add(person);

            // Validate Test --------------------
            var hasPersonStevenVanderMerwe =
                db.Persons.Local.Any(p => p.FirstName == "Steven" && p.LastName == "van der Merwe");

            // Assert --------------------
            Assert.IsTrue(hasPersonStevenVanderMerwe);
        }


        #region Duplicate Test
        [Ignore]
        [TestMethod]
        public void ConstructMeetingItem_GivenStatusAndMeeting_ShouldReturnTrue()
        {
            // Setup --------------------
            var db = new Context();

            // Run Test --------------------
            var meetingItem = new MeetingItem();
            meetingItem.MeetingItemDesc = "LEARN EF";
            meetingItem.MeetingItemDueDate = DateTime.Now;
            db.MeetingItems.Add(meetingItem);

            // Validate Test --------------------
            var hasMeetingItem = db.MeetingItems.Local.Count(p => p.MeetingItemDesc.ToUpper() == "LEARN EF") > 0;

            // Assert --------------------
            Assert.IsTrue(hasMeetingItem);
        }
        #endregion

        #region Helper Methods
        private static Meeting CreateMeeting(MeetingUseCase uc, MeetingType mancoMeetingType)
        {
            var meeting = uc.Current;
            meeting.MeetingNumber = 1;
            meeting.MeetingDate = DateTime.Now;
            meeting.MeetingType = mancoMeetingType;
            return meeting;
        }

        private static MeetingType CreateMancoMeetingType()
        {
            var mancoMeetingType = new MeetingType();
            //mancoMeetingType.MeetingTypeId = 1;
            mancoMeetingType.MeetingTypeName = "MANCO";
            return mancoMeetingType;
        }

        private static MeetingItemStatusLu CreateMeetingItemStatusLu()
        {
            var meetingItemStatusLu = new MeetingItemStatusLu();
            meetingItemStatusLu.MeetingItemStatusDesc = "CREATED";
            return meetingItemStatusLu;
        }

        private static MeetingItem CreateMeetingItem()
        {
            var meetingItem = new MeetingItem();
            meetingItem.MeetingItemDesc = "Learn WPF";
            meetingItem.MeetingItemDueDate = DateTime.Now;
            meetingItem.PersonResponsible = new Person("Bob", "Smith");
            return meetingItem;
        }

        private static void CreateMeetingItemStatus(Meeting meeting, MeetingItem meetingItem, Context db,
                                                    out MeetingItemStatus meetingItemStatus)
        {
            meetingItemStatus = new MeetingItemStatus();
            var meetingItemStatusLu = CreateMeetingItemStatusLu();

            meetingItemStatus.MeetingItemStatusLu = meetingItemStatusLu;
            meetingItemStatus.MeetingItemStatusDate = DateTime.Now;
            meetingItemStatus.Meeting = meeting;
            meetingItemStatus.MeetingItem = meetingItem;

            db.MeetingItemStatusLus.Add(meetingItemStatusLu);
            db.MeetingItemStatuses.Add(meetingItemStatus);
        }

        #endregion
    }
}
