using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ResolutionActionSystemContext;
using ResolutionActionSystemData.Annotations;
using ResolutionActionSystemLogic;

namespace ResolutionActionSystem
{
    public class CaptureMeetingController<T> :Controller, INotifyPropertyChanged where T: CaptureMeeting
    {
        protected MeetingUseCase MeetingUseCase { get; set; }

        public CaptureMeetingController(T userControl) 
            : base(userControl)
        {
            var meetingUseCase = new MeetingUseCase();
            this.MeetingUseCase = meetingUseCase;
            this.PrimaryControl.DataContext = this;
            MeetingUseCase.CreateNewMeeting();
        }

        public void RegisterPropertyControl<K>(K control)
        {
            if (control is ComboBox)
            {
                var incomingControl = control as ComboBox;
                incomingControl.SelectionChanged += PropertyChanged2;
            }
            else if (control is ListBox)
            {
                var incomingControl = control as ListBox;
                ((INotifyCollectionChanged)incomingControl.Items).CollectionChanged += PropertyChanged2;
            }
            else if (control is DatePicker)
            {
                var incomingControl = control as DatePicker;
                incomingControl.SelectedDateChanged += PropertyChanged2;
            }

        }

        private void PropertyChanged2(object sender, NotifyCollectionChangedEventArgs e)
        {
            MeetingUseCase.PropertyChanged(((Control)sender).Tag.ToString());
        }

        void PropertyChanged2(object sender, TextChangedEventArgs e)
        {
            MeetingUseCase.PropertyChanged(((Control) sender).Tag.ToString());
        }

        void PropertyChanged2(object sender, SelectionChangedEventArgs e)
        {
            MeetingUseCase.PropertyChanged(((Control)sender).Tag.ToString());
        }

        public MeetingType CurrentMeetingType
        {
            get { return MeetingUseCase.Current.MeetingType; }
            set
            {
                if (MeetingUseCase.Current.MeetingType == value) return;

                MeetingUseCase.Current.MeetingType = value;
                OnPropertyChanged("MeetingType");
            }
        }

        public List<MeetingType> MeetingTypes { get { return MeetingUseCase.MeetingTypes; } }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
