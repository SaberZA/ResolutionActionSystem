using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ResolutionActionSystem.ViewModel;
using ResolutionActionSystemLogic;
using ResolutionActionSystemLogic.CustomClasses;

namespace ResolutionActionSystem.EditMeetingPopups
{
    /// <summary>
    /// Interaction logic for EditItemStatus.xaml
    /// </summary>
    public partial class EditItemStatus : Window
    {
        protected EditItemStatusViewModel ViewModel { get; set; }

        public EditItemStatus(List<MeetingItemStatusLu> meetingItemStatusLus, MeetingMinute meetingMinute)
        {
            InitializeComponent();
            this.ViewModel = new EditItemStatusViewModel(meetingItemStatusLus, meetingMinute);
            this.DataContext = ViewModel;
            ViewModel.UIEventRaised += ViewModel_UIEventRaised;
        }

        void ViewModel_UIEventRaised(object sender, UIEventHandlerArgs args)
        {
            if (args == UIEventHandlerArgs.StatusSubmitted)
            {
                StatusSubmitted = true;
                this.Close();
            }
        }

        public bool StatusSubmitted { get; set; }

        public MeetingItemStatusLu SelectedItemStatusLu
        {
            get { return ViewModel.SelectedMeetingItemStatusLu; }
        }

        
    }
}
