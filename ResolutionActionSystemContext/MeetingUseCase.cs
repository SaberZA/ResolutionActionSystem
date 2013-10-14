using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;
using Context = ResolutionActionSystemContext.Context.Context;

namespace ResolutionActionSystemLogic
{
    public class MeetingUseCase
    {
        public MeetingUseCase(Context context)
        {
            this.Context = context;
        }

        protected Context Context { get; private set; }

        public Meeting Current { get; set; }

        public MeetingMinute CurrentMeetingItem { get; set; }

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
    }
}