﻿using System.Data.Entity;
using ResolutionActionSystemLogic;

namespace ResolutionActionSystemContext.Context
{
    public class Context : DbContext
    {
        public Context()
              : base() 
        {
            
        }

        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingItemStatus> MeetingItemStatuses { get; set; }
        public DbSet<MeetingAction> MeetingActions { get; set; }
        public DbSet<MeetingItem> MeetingItems { get; set; }
        public DbSet<MeetingItemStatusLu> MeetingItemStatusLus { get; set; }
        public DbSet<Person> Persons { get; set; }

        public MeetingType CreateMeetingType()
        {
            return new MeetingType();
        }
    }
}
