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

        public Menu()
        {
            InitializeComponent();

            LoadTabUserControls();

            CreateContext();

            InitController();

            RegisterPropertyControls();
        }


        public void InitController()
        {
            this.Controller = new MenuController<Menu>(this);
        }

        public Controller GetController()
        {
            return this.Controller;
        }

        public void RegisterPropertyControls()
        {
            Controller.RegisterButton(btnCaptureNewMeeting);
            Controller.RegisterButton(btnEditMeeting);
            Controller.RegisterButton(btnPrintMeetingMinutes);
            Controller.RegisterButton(btnViewMinutesHistory);
        }

        private void LoadTabUserControls()
        {
            var editMeetingUserControl = new EditMeeting();
            tabEditMeeting.Content = editMeetingUserControl;

            var captureMeetingUserControl = new CaptureMeeting();
            tabCaptureNewMeeting.Content = captureMeetingUserControl;
        }

        private void CreateContext()
        {
            var db = new Context();
        }

        private void btnEditMeeting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCaptureNewMeeting_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
