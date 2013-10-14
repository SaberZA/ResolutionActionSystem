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
            var db = new Context();
            var mancoMeetingType = CreateMancoMeetingType();

            if (!db.MeetingTypes.Any(p=>p.MeetingTypeName.ToUpper() == "MANCO".ToUpper()))
            {
                db.MeetingTypes.Add(mancoMeetingType);
                db.SaveChanges();
            }

            var hasMancoMeetingType = db.MeetingTypes.Count(p => p.MeetingTypeName.ToUpper() == "MANCO") > 0;

            Assert.IsTrue(hasMancoMeetingType);
        }
        
        [TestMethod]
        public void ConstructMeeting_GivenTypeNumberDate_ShouldReturnTrue()
        {
            var db = new Context();
            var uc = new MeetingUseCase(db);
            var mancoMeetingType = db.MeetingTypes.FirstOrDefault(p => p.MeetingTypeName.ToUpper() == "MANCO");
            uc.CreateNewMeeting();
            var meeting = uc.Current;
            
            meeting.MeetingNumber = 1;
            meeting.MeetingDate = DateTime.Now;
            meeting.MeetingType = mancoMeetingType;

            if (!db.Meetings.Any(p=>p.MeetingNumber == 1))
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
            }
           

            var hasMeeting = db.Meetings.Count(p => p.MeetingNumber == 1) > 0;

            Assert.IsTrue(hasMeeting);
        } 

        [TestMethod]
        public void ConstructMeetingItemStatus_GivenStatus_ShouldReturnTrue()
        {
            var db = new Context();
            var meeting = db.Meetings.FirstOrDefault();
            var meetingItem = db.MeetingItems.FirstOrDefault();

            if (!db.MeetingItemStatuses.Any(p => p.Meeting.MeetingNumber == meeting.MeetingId && p.MeetingItem.MeetingItemId == meetingItem.MeetingItemId))
            {
                var meetingItemStatus = new MeetingItemStatus();

                var meetingItemStatusLu = new MeetingItemStatusLu();
                meetingItemStatusLu.MeetingItemStatusDesc = "CREATED";
                db.MeetingItemStatusLus.Add(meetingItemStatusLu);

                meetingItemStatus.MeetingItemStatusLu = meetingItemStatusLu;
                meetingItemStatus.MeetingItemStatusDate = DateTime.Now;
                meetingItemStatus.Meeting = meeting;
                meetingItemStatus.MeetingItem = meetingItem;

                meetingItem.MeetingItemStatuses.Add(meetingItemStatus);
                meeting.MeetingItemStatuses.Add(meetingItemStatus);
                
                db.SaveChanges();
            }

            
            var meetingItemStatusTest =
                db.MeetingItemStatuses.FirstOrDefault(
                    p =>
                    p.Meeting.MeetingId == meeting.MeetingId &&
                    p.MeetingItem.MeetingItemId == meetingItem.MeetingItemId);

            if (meetingItemStatusTest.MeetingItemStatusLu == null)
            {
                meetingItemStatusTest.MeetingItemStatusLu =
                    db.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc == "CREATED");
                db.SaveChanges();
            }

            var meetingItemStatusOfCreated = db.MeetingItemStatuses.FirstOrDefault(p => p.Meeting.MeetingId == meeting.MeetingId && p.MeetingItem.MeetingItemId == meetingItem.MeetingItemId);

            bool hasMeetingItemStatusOfCreated = meetingItemStatusOfCreated != null;
            Assert.IsTrue(hasMeetingItemStatusOfCreated);
        }

        [TestMethod]
        public void AddMeetingItem_GivenMeeting_ShouldReturnMeeting_MeetingStatuses_MeetingItems()
        {
            var db = new Context();
            var meeting = db.Meetings.FirstOrDefault(p => p.MeetingNumber == 1);

            var uc = new MeetingUseCase(db) {Current = meeting};

            if (!meeting.MeetingItemStatuses.Any(p=>p.MeetingItem.MeetingItemDesc.ToUpper() == "EFv2".ToUpper()))
            {
                uc.AddMeetingItem("EFv2", DateTime.Now);
                uc.Save();
            }
            
            var meetingItemExists =
                 uc.Current.MeetingItemStatuses.Count(p => p.MeetingItem.MeetingItemDesc.ToUpper() == "EFV2") > 0;

            Assert.IsTrue(meetingItemExists);
        }

        [TestMethod]
        public void PrintMeetingMinutes_GivenMeetingId1_ShouldReturnMinutes()
        {
            var db = new Context();
            var uc = new MeetingUseCase(db);
            int meetingId = db.Meetings.FirstOrDefault().MeetingId;
            uc.Current = uc.GetMeetingById(meetingId);

            List<MeetingMinute> meetingMinutes = uc.GetCurrentMeetingMinutes();

            var hasMeetingMinutes = meetingMinutes.Count > 0;
            string allMinutes = "";// meetingMinutes.Select(p => p.ToString() + "\r\n").ToString();
            foreach (var meetingMinute in meetingMinutes)
            {
                allMinutes += meetingMinute.ToString() + "\r\n";
            }
            
            Assert.IsTrue(hasMeetingMinutes);
            Debug.Print(allMinutes);
        }

        [TestMethod]
        public void UpdateCurrentMeetingItemPerson_GivenMeetingAndMeetingItem_ShouldReturnUpdatedItemName()
        {
            var db= new Context();
            var uc = new MeetingUseCase(db);
            int meetingId = 2;
            
            uc.Current = db.Meetings.FirstOrDefault();
            //int meetingItemStatusId = 3;
            int meetingItemStatusId = db.MeetingItemStatuses.FirstOrDefault().MeetingItemInstanceId;
            //uc.Current = uc.GetMeetingById(meetingId);
            uc.CurrentMeetingItem = uc.GetMeetingMinute(meetingItemStatusId);

            var personResponsible = new Person();
            personResponsible.FirstName = "Bob";
            personResponsible.LastName = "Smith";

            if (!db.Persons.Any(p=>p.FirstName == "Bob" && p.LastName == "Smith"))
            {
                uc.AddPerson(personResponsible);
            }

            uc.UpdateCurrentMeetingItem_PersonResponsible(personResponsible);
            db.SaveChanges();

            var updatedMeetingMinute = uc.GetMeetingMinute(meetingItemStatusId);
            var hasUpdatedItemToBob = updatedMeetingMinute.PersonResponsibleName == "Bob Smith";

            Assert.IsTrue(hasUpdatedItemToBob);
        }

        [TestMethod]
        public void UpdateCurrentMeetingItemStatus_GivenMeetingAndMeetingItem_ShouldReturnUpdatedStatus()
        {
            var db = new Context();
            var uc = new MeetingUseCase(db);

            uc.Current = db.Meetings.FirstOrDefault();
            int meetingItemStatusId = db.MeetingItemStatuses.FirstOrDefault().MeetingItemInstanceId;
            uc.CurrentMeetingItem = uc.GetMeetingMinute(meetingItemStatusId);

            var meetingItemStatusLu =
                db.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc == "WIP");

            uc.UpdateCurrentMeetingItem_Status(meetingItemStatusLu);

            uc.Save();

            var updatedMeetingMinute = uc.GetMeetingMinute(meetingItemStatusId);
            var hasUpdatedItemStatusToWip = updatedMeetingMinute.Status == "WIP";

            Assert.IsTrue(hasUpdatedItemStatusToWip);
        }

        [TestMethod]
        public void ConstructMeetingItem_GivenDescription_ShouldReturnMeetingItem()
        {
            var meetingItem = new MeetingItem();
            meetingItem.MeetingItemDesc = "Learn WPF";
            meetingItem.MeetingItemDueDate = DateTime.Now.AddDays(2);

            var db = new Context();

            if (!db.MeetingItems.Any(p => p.MeetingItemDesc.ToUpper() == "Learn WPF".ToUpper())) ;
            {
                db.MeetingItems.Add(meetingItem);
                db.SaveChanges();
            }

            var hasMeetingItemWPF = db.MeetingItems.Count(p => p.MeetingItemDesc.ToUpper() == "Learn WPF".ToUpper()) > 0;

            Assert.IsTrue(hasMeetingItemWPF);
        }

        [TestMethod]
        public void ConstructMeetingAction_GivenMeetingItemStatus_ShouldReturnMeetingAction()
        {
            var db = new Context();
            var meetingItemStatus =
                db.MeetingItemStatuses.FirstOrDefault();

            var meetingAction = new MeetingAction();
            meetingAction.ActionDescription = "Learn TDD";
            meetingAction.MeetingItemStatus = meetingItemStatus;

            if (!db.MeetingActions.Any(p => p.ActionDescription.ToUpper() == "Learn TDD".ToUpper()))
            {
                db.MeetingActions.Add(meetingAction);
                db.SaveChanges();
            }

            meetingItemStatus =
                db.MeetingItemStatuses.FirstOrDefault();

            var hasMeetingAction =
                meetingItemStatus.MeetingActions.Count(p => p.ActionDescription.ToUpper() == "Learn TDD".ToUpper()) > 0;

            Assert.IsTrue(hasMeetingAction);
        }

        [TestMethod]
        public void NewMeeting_ShouldSetCurrentMeeting()
        {
            var uc = new MeetingUseCase(new Context());
            uc.CreateNewMeeting();

            var newCurrentMeeting = uc.Current != null;

            Assert.IsTrue(newCurrentMeeting);
        }

        [TestMethod]
        public void ConstructMeetingStatusLu_ShouldReturnMeetingStatusLu()
        {
            var db = new Context();
            var meetingStatusLu = new MeetingItemStatusLu();
            meetingStatusLu.MeetingItemStatusDesc = "CREATED";
            if (!db.MeetingItemStatusLus.Any(p => p.MeetingItemStatusDesc == "CREATED"))
            {
                db.MeetingItemStatusLus.Add(meetingStatusLu);
                db.SaveChanges();
            }

            var hasMeetingItemStatusLuWIP = db.MeetingItemStatusLus.Count(p => p.MeetingItemStatusDesc.ToUpper() == "CREATED".ToUpper()) > 0;

            Assert.IsTrue(hasMeetingItemStatusLuWIP);
        }

        [TestMethod]
        public void ConstructPerson_GivenName_ShouldReturnPerson()
        {
            var db = new Context();
            var person = new Person();
            person.FirstName = "Steven";
            person.LastName = "van der Merwe";

            if (!db.Persons.Any(p=>p.FirstName == "Steven" && p.LastName == "van der Merwe"))
            {
                db.Persons.Add(person);
                db.SaveChanges();
            }

            var hasPersonStevenVanderMerwe =
                db.Persons.Any(p => p.FirstName == "Steven" && p.LastName == "van der Merwe");

            Assert.IsTrue(hasPersonStevenVanderMerwe);
        }


        #region Duplicate Test
        [Ignore]
        [TestMethod]
        public void ConstructMeetingItem_GivenStatusAndMeeting_ShouldReturnTrue()
        {
            var db = new Context();
            //var meeting = db.Meetings.FirstOrDefault(p => p.MeetingNumber == 1);
            var meetingItem = new MeetingItem();
            meetingItem.MeetingItemDesc = "LEARN EF";
            meetingItem.MeetingItemDueDate = DateTime.Now;

            db.MeetingItems.Add(meetingItem);
            //db.SaveChanges();

            var hasMeetingItem = db.MeetingItems.Count(p => p.MeetingItemDesc.ToUpper() == "LEARN EF") > 0;

            Assert.IsTrue(hasMeetingItem);
        }
        #endregion


        private static MeetingType CreateMancoMeetingType()
        {
            var mancoMeetingType = new MeetingType();
            //mancoMeetingType.MeetingTypeId = 1;
            mancoMeetingType.MeetingTypeName = "MANCO";
            return mancoMeetingType;
        }
    }
}
