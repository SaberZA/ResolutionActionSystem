using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
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
            Meetings = Context.Meetings.ToList();
            MeetingItemStatusLus = Context.MeetingItemStatusLus.ToList();
        }

        public MeetingUseCase()
        {
            this.Context = new Context();
            LoadLookups();
        }

        protected Context Context { get; private set; }
        
        public Meeting Current { get; set; }
        
        public MeetingMinute CurrentMeetingItem { get; set; }

        public bool CurrentHasChanges { get; set; }

        #region Business Methods

        


        #endregion

        #region Lookups
        public List<MeetingType> MeetingTypes { get; set; }
        public List<Meeting> Meetings { get; set; }
        public List<MeetingItemStatusLu> MeetingItemStatusLus { get; set; }
        #endregion

        

        public void Save()
        {
            Context.SaveChanges();
            CurrentHasChanges = false;
        }

        public void Cancel()
        {
            // Change Tracker not tracking changes made to MeetingItemStatusLu association 
            // Not ideal : Reloading Context
            // RollBack();
            int originalMeetingId = Current.MeetingId;
            Context = new Context();
            CurrentHasChanges = false;
            Current = Context.Meetings.FirstOrDefault(p => p.MeetingId == originalMeetingId);
        }

        public void RollBack()
        {
            var context = Context;
            var changedEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }

        }

        public void UndoUpdates(DbContext dbContext)
        {
            //Get list of entities that are marked as modified
            List<DbEntityEntry> modifiedEntityList =
                dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();

            foreach (DbEntityEntry entity in modifiedEntityList)
            {
                DbPropertyValues propertyValues = entity.OriginalValues;
                foreach (String propertyName in propertyValues.PropertyNames)
                {
                    //Replace current values with original values
                    PropertyInfo property = entity.Entity.GetType().GetProperty(propertyName);
                    property.SetValue(entity.Entity, propertyValues[propertyName],null);
                    //property.SetValue(entity.Entity, propertyValues[propertyName]);
                }
            }
        }
        
        public Meeting GetMeetingById(int meetingId)
        {
            return Context.Meetings.FirstOrDefault(p => p.MeetingId == meetingId);
        }

        public List<MeetingMinute> GetCurrentMeetingMinutes()
        {
            if (Current == null) return new List<MeetingMinute>();
            return Current.MeetingItemStatuses.Select(currentMeetingStatus => new MeetingMinute(currentMeetingStatus)).ToList();
        }

        public List<MeetingMinute> GetPreviousMeetingMinutes()
        {
            if (Current.PreviousMeeting == null) return new List<MeetingMinute>();
            
            return Current.PreviousMeeting.MeetingItemStatuses.Select(currentMeetingStatus => new MeetingMinute(currentMeetingStatus)).ToList();
        }

        public MeetingMinute GetMeetingMinute(int meetingItemStatusId)
        {
            return new MeetingMinute(Current.MeetingItemStatuses.FirstOrDefault(p=>p.MeetingItemStatusId == meetingItemStatusId));
        }

        public void AddPerson(Person personResponsible)
        {
            Context.Persons.Add(personResponsible);
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
            Context.Meetings.Add(Current);
        }

        public void PropertyChanged(string propertyName)
        {
            CurrentHasChanges = true;
        }

        


        public void UpdateCurrentMeeting_MeetingType(MeetingType meetingType)
        {
            var previousMeetings =
                Context.Meetings.Where(p => p.MeetingType.MeetingTypeName == meetingType.MeetingTypeName);

            if (!previousMeetings.Any())
            {
                Current.PreviousMeeting = null;
                Current.MeetingNumber = 1;
                Current.MeetingType = meetingType;
                return;
            }

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
                Context.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc.ToUpper() == currentMeetingItem.Status.ToUpper());
            Context.MeetingItemStatuses.Add(meetingItemStatus);

            meetingItem.MeetingItemStatuses.Add(meetingItemStatus);

            Current.MeetingItemStatuses.Add(meetingItemStatus);
        }

        public void AddNewMeetingItem(string meetingItemDesc, DateTime meetingItemDueDate, Person personResponsible)
        {
            var meetingItem = new MeetingItem
            {
                MeetingItemDesc = meetingItemDesc,
                MeetingItemDueDate = meetingItemDueDate,
                PersonResponsible = personResponsible
            };
            Context.MeetingItems.Add(meetingItem);

            var meetingItemStatus = new MeetingItemStatus();
            meetingItemStatus.Meeting = Current;
            meetingItemStatus.MeetingItem = meetingItem;
            meetingItemStatus.MeetingItemStatusDate = DateTime.Now;
            meetingItemStatus.MeetingItemStatusLu =
                Context.MeetingItemStatusLus.FirstOrDefault(p => p.MeetingItemStatusDesc.ToUpper() == "Created".ToUpper());
            Context.MeetingItemStatuses.Add(meetingItemStatus);

            meetingItem.MeetingItemStatuses.Add(meetingItemStatus);

            Current.MeetingItemStatuses.Add(meetingItemStatus);
        }

        public void LinkMeetingItems(IEnumerable<MeetingMinute> scheduledMeetingMinutes)
        {
            foreach (MeetingMinute scheduledMeetingMinute in scheduledMeetingMinutes)
            {
                AddMeetingItem(scheduledMeetingMinute);
            }
        }
    }
}