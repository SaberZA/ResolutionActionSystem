using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using ResolutionActionSystemData.Annotations;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;

namespace ResolutionActionSystem.ViewModel
{
    public class EditItemStatusViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private MeetingMinute _currentMeetingItem;
        private MeetingItemStatusLu _selectedMeetingItemStatusLu;

        public EditItemStatusViewModel(IEnumerable<MeetingItemStatusLu> meetingItemStatusLus, MeetingMinute meetingItem)
        {
            MeetingItemStatusLus = Common.ToObservableCollection(meetingItemStatusLus);
            CurrentMeetingItem = meetingItem;

            SubmitItemStatus = new RelayCommand(SubmitItemStatus_Execute,SubmitItemStatus_CanExecute);
        }

       

        

        public ObservableCollection<MeetingItemStatusLu> MeetingItemStatusLus { get; set; }

        public MeetingItemStatusLu SelectedMeetingItemStatusLu
        {
            get { return _selectedMeetingItemStatusLu; }
            set
            {
                if (Equals(value, _selectedMeetingItemStatusLu)) return;
                _selectedMeetingItemStatusLu = value;
                OnPropertyChanged("SelectedMeetingItemStatusLu");
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
            }
        }

        #region Events
        public event UIEventHandler UIEventRaised;
        public delegate void UIEventHandler(object sender, UIEventHandlerArgs args);

        private void OnUIEventRaised(UIEventHandlerArgs args)
        {
            var handler = UIEventRaised;
            if (handler != null) handler(this, args);
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

        #region ICommand
        public ICommand SubmitItemStatus { get; set; }
        private void SubmitItemStatus_Execute()
        {
            OnUIEventRaised(UIEventHandlerArgs.StatusSubmitted);
        }

        private bool SubmitItemStatus_CanExecute()
        {
            return SelectedMeetingItemStatusLu != null;
        }
        #endregion

        public string this[string name]
        {
            get
            {
                string result = null;
                if (name == "SelectedMeetingItemStatusLu")
                {
                    if (SelectedMeetingItemStatusLu == null)
                    {
                        result = "The new Item Status must not be empty.";
                    }
                }
                return result;
            }
        }

        public string Error { get; private set; }
    }
}
