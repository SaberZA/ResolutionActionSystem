using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResolutionActionSystemLogic.CustomClasses
{
    public class MeetingMinute
    {
        public string PersonResponsibleName 
        { 
            get
            {
                return String.Format("{0} {1}", PersonResponsible.FirstName,
                                     PersonResponsible.LastName);
            }
        }
        public string Status 
        { 
            get
            {
                return MeetingItemStatus.MeetingItemStatusLu.MeetingItemStatusDesc;
            }   
        }
        public string Comment
        {
            get
            {
                var meetingActions = "";

                foreach (var meetingAction in MeetingActions)
                {
                    meetingActions += meetingAction.ActionDescription + ". ";
                }

                return meetingActions;
            }
        }
        public string MeetingItemDescription
        {
            get
            {
                return MeetingItem.MeetingItemDesc;
            }
        }

        private MeetingItem MeetingItem
        {
            get { return this.MeetingItemStatus.MeetingItem; }
        }
        private Person PersonResponsible
        {
            get { return MeetingItem.PersonResponsible; }
        }
        private MeetingItemStatusLu MeetingItemStatusLu
        {
            get { return MeetingItemStatus.MeetingItemStatusLu; }
        }
        private ICollection<MeetingAction> MeetingActions
        {
            get { return MeetingItemStatus.MeetingActions; }
        }

        public MeetingItemStatus MeetingItemStatus { get; private set; }
        
        public MeetingMinute(MeetingItemStatus currentMeetingStatus)
        {
            MeetingItemStatus = currentMeetingStatus;
        }
        
        public void UpdatePersonResponsible(Person newPersonResponsible)
        {
            MeetingItem.PersonResponsible = newPersonResponsible;
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1,10}\t{2,10}\t{3,10}", this.MeetingItemDescription, this.Comment, this.Status, this.PersonResponsibleName);
        }

        public void UpdateMeetingItemStatus(MeetingItemStatusLu meetingItemStatus)
        {
            MeetingItemStatus.MeetingItemStatusLu = meetingItemStatus;
        }
    }
}
