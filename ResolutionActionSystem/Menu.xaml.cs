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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ResolutionActionSystem.Controllers;
using ResolutionActionSystemContext;

namespace ResolutionActionSystem
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl, IControllable
    {
        protected MenuViewModel<Menu> ViewModel { get; set; }
        protected EditMeeting EditMeetingUserControl { get; set; }
        protected CaptureMeeting CaptureMeetingUserControl { get; set; }

        public Menu()
        {
            InitializeComponent();

            LoadTabUserControls();

            InitController();

            RegisterMenuControls();

            LoadEvents();
        }

        private void LoadEvents()
        {
            ((CaptureMeetingViewModel<CaptureMeeting>)this.CaptureMeetingUserControl.GetController()).UIEventRaised += UIEventRaised;
        }

        private void UIEventRaised(object sender, UIEventHandlerArgs args)
        {
            if (args == UIEventHandlerArgs.MeetingCreated)
            {
                ViewModel.ActiveButtonTag = "1";
                ViewModel.ActiveTabIndex = 1;
                ViewModel.UpdateActiveTab();

                ISetMeeting meetingSetter = (EditMeetingViewModel<EditMeeting>)EditMeetingUserControl.GetController();
                meetingSetter.SetMeeting(((IGetMeeting)this.CaptureMeetingUserControl.GetController()).GetMeeting());
            }
        }


        public void InitController()
        {
            this.ViewModel = new MenuViewModel<Menu>(this);
        }

        public Controller GetController()
        {
            return this.ViewModel;
        }

        public void RegisterMenuControls()
        {
            ViewModel.RegisterButton(btnCaptureNewMeeting);
            ViewModel.RegisterButton(btnEditMeeting);
            ViewModel.RegisterButton(btnPrintMeetingMinutes);
            ViewModel.RegisterButton(btnViewMinutesHistory);
        }

        private void LoadTabUserControls()
        {
            EditMeetingUserControl = new EditMeeting();
            tabEditMeeting.Content = EditMeetingUserControl;

            CaptureMeetingUserControl = new CaptureMeeting();
            tabCaptureNewMeeting.Content = CaptureMeetingUserControl;
        }
        
        private void btnEditMeeting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCaptureNewMeeting_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
