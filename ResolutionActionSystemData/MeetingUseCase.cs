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
        }

        public MeetingUseCase()
        {
            this.Context = new Context();
            MeetingTypes = Context.MeetingTypes.ToList();
        }

        protected Context Context { get; private set; }

        

        public Meeting Current { get; set; }

        public MeetingType MeetingType
        {
            get { return Current.MeetingType; }
            set { Current.MeetingType = value; }
        }

        public MeetingMinute CurrentMeetingItem { get; set; }

        protected bool CurrentHasChanges { get; set; }

        #region Business Methods

        public void AddMeetingItem(string meetingItemDesc, DateTime meetingItemDueDate)
        {
            var meetingItem = new MeetingItem
                {
                    MeetingItemDesc = meetingItemDesc,
                    MeetingItemDueDate = meetingItemDueDate
                };
            Context.MeetingItems.Add(meetingItem);

            var meetingItemStatus = new MeetingItemStatus();
            meetingItemStatus.Meeting = Current;
            meetingItemStatus.MeetingItem = meetingItem;
            meetingItemStatus.MeetingItemStatusDate = DateTime.Now;
            meetingItemStatus.MeetingItemStatusLu =
                Context.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc.ToUpper() == "CREATED");
            Context.MeetingItemStatuses.Add(meetingItemStatus);

            meetingItem.MeetingItemStatuses.Add(meetingItemStatus);

            Current.MeetingItemStatuses.Add(meetingItemStatus);
            
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

        
    }
}