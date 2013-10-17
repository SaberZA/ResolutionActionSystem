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
    public class EditMeetingViewModel<T> : Controller, INotifyPropertyChanged, ISetMeeting, ISetStatus where T: EditMeeting
    {
        public MeetingUseCase MeetingUseCase { get; set; }
        private bool MeetingIsNew { get; set; }
        
        public EditMeetingViewModel(T userControl) 
            : base(userControl)
        {
            InitModel();

            EditItemStatusCommand = new RelayCommand(EditItemStatus_Execute, EditItemStatus_CanExecute);
            SaveItemCommand = new RelayCommand(SaveItem_Execute, SaveItem_CanExecute);
            CancelItemCommand = new RelayCommand(CancelItem_Execute, CancelItem_CanExecute);
            SelectMeetingCommand = new RelayCommand(SelectMeeting_Execute, SelectMeeting_CanExecute);
        }

        #region Methods
        private void InitModel()
        {
            var meetingUseCase = new MeetingUseCase();
            this.MeetingUseCase = meetingUseCase;
        }

        public void SetMeeting(Meeting meeting)
        {
            InitModel();
            CurrentMeeting = Meetings.FirstOrDefault(p => p.MeetingId == meeting.MeetingId);
            SelectedMeeting = CurrentMeeting;
        }

        private void Save()
        {
            MeetingUseCase.Current = CurrentMeeting;
            MeetingUseCase.Save();
            CurrentMeetingItemHasChanges = false;
            MeetingIsNew = true;
            CurrentMeeting = MeetingUseCase.Current;
            OnPropertyChanged("");
            OnInformationEventRaised("Meeting Item has been saved.");
        }

        private void Cancel()
        {
            MeetingUseCase.Cancel();

            CurrentMeetingItemHasChanges = false;
            CurrentMeetingItem = null;
            ScheduledMeetingMinutes = null;
            MeetingIsNew = true;
            CurrentMeeting = MeetingUseCase.Current;
            OnPropertyChanged("");
        }

        public void SetItemStatus(MeetingItemStatusLu meetingItemStatusLu)
        {
            if (CurrentMeetingItem == null) return;

            CurrentMeetingItemHasChanges = true;

            CurrentMeetingItem.UpdateMeetingItemStatus(meetingItemStatusLu);
            OnPropertyChanged("CurrentMeetingItem");
        }
        #endregion

        #region Current Properties
        private bool CurrentMeetingItemHasChanges { get; set; }
        private Meeting _currentMeeting;
        private MeetingMinute _currentMeetingItem;
        private Meeting _selectedMeeting;

        public Meeting SelectedMeeting
        {
            get { return _selectedMeeting; }
            set
            {
                if (Equals(value, _selectedMeeting)) return;
                _selectedMeeting = value;
                OnPropertyChanged("SelectedMeeting");
            }
        }

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

        public MeetingMinute CurrentMeetingItem
        {
            get { return _currentMeetingItem; }
            set
            {
                if (Equals(value, _currentMeetingItem)) return;
                _currentMeetingItem = value;
                MeetingUseCase.CurrentMeetingItem = _currentMeetingItem;
                OnPropertyChanged("CurrentMeetingItem");
                OnPropertyChanged("MeetingActions");
            }
        }
        #endregion

        #region Lists
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                var meetings = new ObservableCollection<Meeting>();
                foreach (Meeting meeting in MeetingUseCase.Meetings.OrderByDescending(p => p.MeetingNumber))
                    meetings.Add(meeting);

                return meetings;
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
        #endregion

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

        public event InformationEventHandler InformationEventRaised;
        public delegate void InformationEventHandler(object sender, string infoMessage);

        public virtual void OnInformationEventRaised(string infoMessage)
        {
            var handler = InformationEventRaised;
            if (handler != null) handler(this, infoMessage);
        }
        
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
            return CurrentMeetingItemHasChanges;
        }

        private void CancelItem_Execute()
        {
            Cancel();
        }

        public ICommand SaveItemCommand { get; set; }
        private bool SaveItem_CanExecute()
        {
            if (CurrentMeetingItem == null) return false;
            return CurrentMeetingItemHasChanges;
        }

        private void SaveItem_Execute()
        {
            Save();
        }

        public ICommand SelectMeetingCommand { get; set; }
        private bool SelectMeeting_CanExecute()
        {
            return SelectedMeeting != null;
        }

        private void SelectMeeting_Execute()
        {
            CurrentMeeting = SelectedMeeting;
        }
        #endregion

        
    }
}
