﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public class CaptureMeetingController<T> :Controller, INotifyPropertyChanged, IGetMeeting where T: CaptureMeeting
    {
        protected MeetingUseCase MeetingUseCase { get; set; }

        

        public CaptureMeetingController(T userControl) 
            : base(userControl)
        {
            var meetingUseCase = new MeetingUseCase();
            this.MeetingUseCase = meetingUseCase;
            this.PrimaryControl.DataContext = this;
            MeetingUseCase.CreateNewMeeting();
            
            this.PropertyChanged += CaptureMeetingController_PropertyChanged;
            this.CurrentMeetingDate = DateTime.Today;
            
            TransferItemCommand = new RelayCommand(TransferItem_Executed,TransferItem_CanExecute);
            TransferAllItemsCommand = new RelayCommand(TransferAllItems_Execute,TransferAllItems_CanExecute);
            ReturnItemCommand = new RelayCommand(ReturnItem_Execute,ReturnItem_CanExecute);
            ReturnAllItemsCommand =new RelayCommand(ReturnAllItems_Execute,ReturnAllItems_CanExecute);
            CreateMeetingCommand = new RelayCommand(CreateMeeting_Execute,CreateMeeting_CanExecute);

            ScheduledMeetingMinutes = new ObservableCollection<MeetingMinute>();
        }

        

        void CaptureMeetingController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MeetingUseCase.PropertyChanged(e.PropertyName);
        }
        
        private Meeting CurrentMeeting
        {
            get { return MeetingUseCase.Current; }
        }

        public MeetingMinute CurrentMeetingItem { get; set; }

        public MeetingMinute ScheduledMeetingItem { get; set; }

        public MeetingType CurrentMeetingType
        {
            get { return CurrentMeeting.MeetingType; }
            set
            {
                if (CurrentMeeting.MeetingType == value) return;

                CurrentMeeting.MeetingType = value;
                OnPropertyChanged("MeetingType");

                //Custom Updates
                MeetingUseCase.UpdateCurrentMeeting_MeetingType(CurrentMeetingType);
                if (CurrentMeeting.PreviousMeeting !=null)
                {
                    AvailableMeetingMinutes = PreviousMeetingMinutes;
                    OnPropertyChanged("PreviousMeetingMinutes");
                    OnPropertyChanged("AvailableMeetingMinutes");
                }
                //------------
            }
        }

        public ObservableCollection<MeetingMinute> AvailableMeetingMinutes{get; set;}

        public ObservableCollection<MeetingMinute> ScheduledMeetingMinutes{get; set;}

        public ObservableCollection<MeetingMinute> PreviousMeetingMinutes
        {
            get
            {
                var minutes = new ObservableCollection<MeetingMinute>();
                foreach (MeetingMinute scheduledMeetingMinute in MeetingUseCase.GetPreviousMeetingMinutes())
                {
                    minutes.Add(scheduledMeetingMinute);
                }
                return minutes;
            }
        }

        public DateTime CurrentMeetingDate
        {
            get { return CurrentMeeting.MeetingDate; }
            set
            {
                if (CurrentMeeting.MeetingDate == value) return;
                CurrentMeeting.MeetingDate = value;
                OnPropertyChanged("MeetingDate");
            }
        }

        public List<MeetingType> MeetingTypes { get { return MeetingUseCase.MeetingTypes; } }

        #region Transfer Methods
        private void RemoveScheduledMeetingMinute(MeetingMinute scheduledMeetingItem)
        {
            ScheduledMeetingMinutes.Remove(scheduledMeetingItem);
            OnPropertyChanged("ScheduledMeetingMinutes");
        }

        private void AddAvailableMeetingMinute(MeetingMinute scheduledMeetingItem)
        {
            AvailableMeetingMinutes.Add(scheduledMeetingItem);
            OnPropertyChanged("AvailableMeetingMinutes");
        }

        private void AddScheduledMeetingMinute(MeetingMinute currentMeetingItem)
        {
            ScheduledMeetingMinutes.Add(currentMeetingItem);
            OnPropertyChanged("ScheduledMeetingMinutes");
        }

        private void RemoveAvailableMeetingMinute(MeetingMinute currentMeetingItem)
        {
            AvailableMeetingMinutes.Remove(currentMeetingItem);
            OnPropertyChanged("AvailableMeetingMinutes");
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

        public event InformationEventHandler InformationEventRaised;
        public delegate void InformationEventHandler(object sender, string infoMessage);

        public virtual void OnInformationEventRaised(string infoMessage)
        {
            var handler = InformationEventRaised;
            if (handler != null) handler(this, infoMessage);
        }

        public event UIEventHandler UIEventRaised;
        public delegate void UIEventHandler(object sender, UIEventHandlerArgs args);

        public virtual void OnUIEventRaised(UIEventHandlerArgs args)
        {
            var handler = UIEventRaised;
            if (handler != null) handler(this, args);
        }

        #region ICommands
        public ICommand ReturnAllItemsCommand { get; set; }
        private void ReturnAllItems_Execute()
        {
            foreach (MeetingMinute scheduledMeetingMinute in ScheduledMeetingMinutes)
            {
                if (!AvailableMeetingMinutes.Contains(scheduledMeetingMinute))
                {
                    AvailableMeetingMinutes.Add(scheduledMeetingMinute);
                }
            }
            ScheduledMeetingMinutes.Clear();
        }

        private bool ReturnAllItems_CanExecute()
        {
            return true;
        }

        public ICommand ReturnItemCommand { get; set; }
        private bool ReturnItem_CanExecute()
        {
            return true;
        }

        private void ReturnItem_Execute()
        {
            if (ScheduledMeetingMinutes.Count == 0) return;

            AddAvailableMeetingMinute(ScheduledMeetingItem);

            RemoveScheduledMeetingMinute(ScheduledMeetingItem);
        }

        public ICommand TransferAllItemsCommand { get; set; }
        private bool TransferAllItems_CanExecute()
        {
            return true;
        }

        private void TransferAllItems_Execute()
        {
            foreach (MeetingMinute availableMeetingMinute in AvailableMeetingMinutes)
            {
                if (!ScheduledMeetingMinutes.Contains(availableMeetingMinute))
                {
                    ScheduledMeetingMinutes.Add(availableMeetingMinute);
                }
            }
            AvailableMeetingMinutes.Clear();
        }

        public ICommand TransferItemCommand { get; set; }
        public bool TransferItem_CanExecute()
        {
            return true;
        }

        void TransferItem_Executed()
        {
            if (AvailableMeetingMinutes.Count == 0) return;

            AddScheduledMeetingMinute(CurrentMeetingItem);

            RemoveAvailableMeetingMinute(CurrentMeetingItem);
        }

        public ICommand CreateMeetingCommand { get; set; }
        private bool CreateMeeting_CanExecute()
        {
            return true;
        }

        private void CreateMeeting_Execute()
        {
            MeetingUseCase.LinkMeetingItems(ScheduledMeetingMinutes);
            MeetingUseCase.Save();
            InformationEventRaised(this, "Meeting Created.\r\nYou can now proceed to edit the Meeting further.");
            UIEventRaised(this,UIEventHandlerArgs.MeetingCreated);
        }
        #endregion

        public Meeting GetMeeting()
        {
            return CurrentMeeting;
        }
    }

    public interface IGetMeeting
    {
        Meeting GetMeeting();
    }
}
