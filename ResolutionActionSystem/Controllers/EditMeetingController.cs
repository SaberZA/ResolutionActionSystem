using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ResolutionActionSystemContext;
using ResolutionActionSystemData.Annotations;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;

namespace ResolutionActionSystem
{
    public class EditMeetingController<T> : Controller, INotifyPropertyChanged, ISetMeeting where T: EditMeeting
    {
        public MeetingUseCase MeetingUseCase { get; set; }

        public EditMeetingController(T userControl) 
            : base(userControl)
        {
            InitModel();
        }

        private void InitModel()
        {
            var meetingUseCase = new MeetingUseCase();
            this.MeetingUseCase = meetingUseCase;
            this.PrimaryControl.DataContext = this;
        }

        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                var meetings = new ObservableCollection<Meeting>();
                foreach (Meeting meeting in MeetingUseCase.Meetings)
                    meetings.Add(meeting);

                return meetings;
            }
        }
        
        private Meeting _currentMeeting;
        private MeetingMinute _currentMeetingItem;

        public Meeting CurrentMeeting
        {
            get { return _currentMeeting; }
            set
            {
                if (_currentMeeting == value) return;
                _currentMeeting = value;
                this.MeetingUseCase.Current = value;
                ScheduledMeetingMinutes = OriginalMeetingMinutes;
                
                OnPropertyChanged("CurrentMeeting");
                OnPropertyChanged("");
            }
        }
        public MeetingMinute CurrentMeetingItem
        {
            get { return _currentMeetingItem; }
            set
            {
                if (Equals(value, _currentMeetingItem)) return;
                _currentMeetingItem = value;
                OnPropertyChanged("CurrentMeetingItem");
                OnPropertyChanged("MeetingActions");
            }
        }

        public ObservableCollection<MeetingAction> MeetingActions
        {
            get
            {
                return CurrentMeetingItem == null 
                    ? new ObservableCollection<MeetingAction>() 
                    : ToObservableCollection(CurrentMeetingItem.MeetingItemStatus.MeetingActions);
            }
        }

        private ObservableCollection<K> ToObservableCollection<K>(IEnumerable<K> inputItems)
        {
            var items = new ObservableCollection<K>();
            foreach (K item in inputItems)
            {
                items.Add(item);
            }
            return items;
        }

        public ObservableCollection<MeetingMinute> ScheduledMeetingMinutes { get; set; }

        public ObservableCollection<MeetingMinute> OriginalMeetingMinutes
        {
            get
            {
                var minutes = new ObservableCollection<MeetingMinute>();
                foreach (MeetingMinute scheduledMeetingMinute in MeetingUseCase.GetCurrentMeetingMinutes())
                {
                    minutes.Add(scheduledMeetingMinute);
                }
                return minutes;
            }
        }


        public void SetMeeting(Meeting meeting)
        {
            InitModel();
            CurrentMeeting = Meetings.FirstOrDefault(p => p.MeetingId == meeting.MeetingId);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public interface ISetMeeting
    {
        void SetMeeting(Meeting meeting);
    }
}
