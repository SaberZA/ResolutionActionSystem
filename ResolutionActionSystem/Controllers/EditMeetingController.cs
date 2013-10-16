using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ResolutionActionSystemContext;
using ResolutionActionSystemData.Annotations;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;

namespace ResolutionActionSystem
{
    public class EditMeetingController<T> : Controller, INotifyPropertyChanged, ISetMeeting, ISetStatus where T: EditMeeting
    {
        public MeetingUseCase MeetingUseCase { get; set; }

        public EditMeetingController(T userControl) 
            : base(userControl)
        {
            InitModel();

            EditItemStatusCommand = new RelayCommand(EditItemStatus_Execute, EditItemStatus_CanExecute);
            SaveItemCommand = new RelayCommand(SaveItem_Execute, SaveItem_CanExecute);
            CancelItemCommand = new RelayCommand(CancelItem_Execute, CancelItem_CanExecute);
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
                if (_currentMeeting == value && !MeetingIsNew) return;
                MeetingIsNew = false;
                _currentMeeting = value;
                this.MeetingUseCase.Current = value;
                ScheduledMeetingMinutes = OriginalMeetingMinutes;
                
                OnPropertyChanged("CurrentMeeting");
                OnPropertyChanged("");
            }
        }

        private bool MeetingIsNew { get; set; }

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
                    : Common.ToObservableCollection(CurrentMeetingItem.MeetingItemStatus.MeetingActions);
            }
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

        private void Save()
        {
            MeetingUseCase.Current = CurrentMeeting;
            MeetingUseCase.Save();
            MeetingIsNew = true;
            CurrentMeeting = MeetingUseCase.Current;
            OnPropertyChanged("");
        }

        private void Cancel()
        {
            MeetingUseCase.Cancel();
            MeetingIsNew = true;
            CurrentMeeting = MeetingUseCase.Current;
            OnPropertyChanged("");
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

        #region Events
        public event UIEventHandler UIEventRaised;
        public delegate void UIEventHandler(object sender, UIEventHandlerArgs args);

        private void OnUIEventRaised(UIEventHandlerArgs args)
        {
            var handler = UIEventRaised;
            if (handler != null) handler(this, args);
        }
        #endregion

        #region ICommands
        public ICommand EditItemStatusCommand { get; set; }
        private void EditItemStatus_Execute()
        {
            OnUIEventRaised(UIEventHandlerArgs.EditStatus);
        }

        private bool EditItemStatus_CanExecute()
        {
            return CurrentMeetingItem != null;
        }

        public ICommand CancelItemCommand { get; set; }
        private bool CancelItem_CanExecute()
        {
            if (CurrentMeetingItem == null) return false;
            return true;
        }

        private void CancelItem_Execute()
        {
            Cancel();
        }

        public ICommand SaveItemCommand { get; set; }
        private bool SaveItem_CanExecute()
        {
            if (CurrentMeetingItem == null) return false;
            return true;
        }

        private void SaveItem_Execute()
        {
            Save();
        }

        #endregion

        public void SetItemStatus(MeetingItemStatusLu meetingItemStatusLu)
        {
            if (CurrentMeetingItem == null) return;

            CurrentMeetingItem.MeetingItemStatus.MeetingItemStatusLu = meetingItemStatusLu;
            OnPropertyChanged("CurrentMeetingItem");
        }
    }

    public interface ISetStatus
    {
        void SetItemStatus(MeetingItemStatusLu meetingItemStatusLu);
    }

    public interface ISetMeeting
    {
        void SetMeeting(Meeting meeting);
    }
}
