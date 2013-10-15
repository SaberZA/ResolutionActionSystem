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
        protected MenuController<Menu> Controller { get; set; }
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
            ((CaptureMeetingController<CaptureMeeting>)this.CaptureMeetingUserControl.GetController()).UIEventRaised += UIEventRaised;
        }

        private void UIEventRaised(object sender, UIEventHandlerArgs args)
        {
            if (args == UIEventHandlerArgs.MeetingCreated)
            {
                Controller.ActiveButtonTag = "1";
                Controller.ActiveTabIndex = 1;
                Controller.UpdateActiveTab();

                ISetMeeting meetingSetter = (EditMeetingController<EditMeeting>)EditMeetingUserControl.GetController();
                meetingSetter.SetMeeting(((IGetMeeting)this.CaptureMeetingUserControl.GetController()).GetMeeting());
            }
        }


        public void InitController()
        {
            this.Controller = new MenuController<Menu>(this);
        }

        public Controller GetController()
        {
            return this.Controller;
        }

        public void RegisterMenuControls()
        {
            Controller.RegisterButton(btnCaptureNewMeeting);
            Controller.RegisterButton(btnEditMeeting);
            Controller.RegisterButton(btnPrintMeetingMinutes);
            Controller.RegisterButton(btnViewMinutesHistory);
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
