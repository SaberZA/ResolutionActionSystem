using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;
using Context = ResolutionActionSystemContext.Context;

namespace ResolutionActionSystemContext
{
    public class MeetingUseCase
    {
        public MeetingUseCase(Context context)
        {
            this.Context = context;
            LoadLookups();
        }

        private void LoadLookups()
        {
            MeetingTypes = Context.MeetingTypes.ToList();
        }

        public MeetingUseCase()
        {
            this.Context = new Context();
            LoadLookups();
        }

        protected Context Context { get; private set; }
        
        public Meeting Current { get; set; }
        
        public MeetingMinute CurrentMeetingItem { get; set; }

        protected bool CurrentHasChanges { get; set; }

        #region Business Methods

        public void AddMeetingItem(string meetingItemDesc, DateTime meetingItemDueDate)
        {
            
            
        }


        #endregion

        #region Lookups
        public List<MeetingType> MeetingTypes { get; set; }
        #endregion

        

        public void Save()
        {
            Context.SaveChanges();
        }

        public Meeting GetMeetingById(int meetingId)
        {
            return Context.Meetings.FirstOrDefault(p => p.MeetingId == meetingId);
        }

        public List<MeetingMinute> GetCurrentMeetingMinutes()
        {
            return Current.MeetingItemStatuses.Select(currentMeetingStatus => new MeetingMinute(currentMeetingStatus)).ToList();
        }

        public List<MeetingMinute> GetPreviousMeetingMinutes()
        {
            if (Current.PreviousMeeting == null) return new List<MeetingMinute>();
            
            return Current.PreviousMeeting.MeetingItemStatuses.Select(currentMeetingStatus => new MeetingMinute(currentMeetingStatus)).ToList();
        }

        public MeetingMinute GetMeetingMinute(int meetingItemStatusId)
        {
            return new MeetingMinute(Current.MeetingItemStatuses.FirstOrDefault(p=>p.MeetingItemInstanceId == meetingItemStatusId));
        }

        public void AddPerson(Person personResponsible)
        {
            Context.Persons.Add(personResponsible);
            Context.SaveChanges();
        }

        public void UpdateCurrentMeetingItem_PersonResponsible(Person personResponsible)
        {
            CurrentMeetingItem.UpdatePersonResponsible(personResponsible);
        }

        public void UpdateCurrentMeetingItem_Status(MeetingItemStatusLu meetingItemStatusLu)
        {
            CurrentMeetingItem.UpdateMeetingItemStatus(meetingItemStatusLu);
        }

        public void CreateNewMeeting()
        {
            Current = new Meeting();
        }

        public void PropertyChanged(string propertyName)
        {
            CurrentHasChanges = true;
        }

        public void Cancel()
        {
            CurrentHasChanges = false;
            Current = Context.Meetings.FirstOrDefault(p => p.MeetingId == Current.MeetingId);
        }


        public void UpdateCurrentMeeting_MeetingType(MeetingType meetingType)
        {
            var previousMeetings =
                Context.Meetings.Where(p => p.MeetingType.MeetingTypeName == meetingType.MeetingTypeName);

            Meeting previousMeeting = null;
            int mostRecentMeetingNumber = previousMeetings.Max(k => k.MeetingNumber);
            if (previousMeetings.Any())
                previousMeeting =
                    previousMeetings.FirstOrDefault(p => p.MeetingNumber == mostRecentMeetingNumber);

            if (previousMeeting != null)
            {
                Current.PreviousMeeting = previousMeeting;
                Current.MeetingNumber = ++mostRecentMeetingNumber;
            }
            else
            {
                Current.MeetingNumber = 1;
            }

            Current.MeetingType = meetingType;
        }

        public void AddNewMeetingType(MeetingType meetingType)
        {
            Context.MeetingTypes.Add(meetingType);
        }

        public void UpdateCurrentMeeting_MeetingDate(DateTime meetingDate)
        {
            Current.MeetingDate = meetingDate;
        }

        public void AddMeetingItem(MeetingMinute currentMeetingItem)
        {
            var meetingItem = new MeetingItem
            {
                MeetingItemDesc = currentMeetingItem.MeetingItemDescription,
                MeetingItemDueDate = currentMeetingItem.MeetingItemStatus.MeetingItem.MeetingItemDueDate,
                PersonResponsible = currentMeetingItem.MeetingItemStatus.MeetingItem.PersonResponsible
            };
            Context.MeetingItems.Add(meetingItem);

            var meetingItemStatus = new MeetingItemStatus();
            meetingItemStatus.Meeting = Current;
            meetingItemStatus.MeetingItem = meetingItem;
            meetingItemStatus.MeetingItemStatusDate = DateTime.Now;
            meetingItemStatus.MeetingItemStatusLu =
                Context.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc.ToUpper() == currentMeetingItem.Status);
            Context.MeetingItemStatuses.Add(meetingItemStatus);

            meetingItem.MeetingItemStatuses.Add(meetingItemStatus);

            Current.MeetingItemStatuses.Add(meetingItemStatus);
        }


        public void RemoveMeetingItem(MeetingMinute scheduledMeetingItem)
        {
            Context.MeetingItems.Remove(scheduledMeetingItem.MeetingItemStatus.MeetingItem);
            Current.MeetingItemStatuses.Remove(scheduledMeetingItem.MeetingItemStatus);
            Context.MeetingItemStatuses.Remove(scheduledMeetingItem.MeetingItemStatus);
            
        }
    }
}