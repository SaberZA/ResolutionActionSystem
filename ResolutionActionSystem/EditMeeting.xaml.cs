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
using ResolutionActionSystem.EditMeetingPopups;

namespace ResolutionActionSystem
{
    /// <summary>
    /// Interaction logic for EditMeeting.xaml
    /// </summary>
    public partial class EditMeeting : UserControl, IControllable
    {
        protected EditMeetingController<EditMeeting> Controller { get; set; }

        public EditMeeting()
        {
            InitializeComponent();
            InitController();

            LoadEvents();
        }

        public void InitController()
        {
            this.Controller = new EditMeetingController<EditMeeting>(this);
        }
        
        public Controller GetController()
        {
            return this.Controller;
        }

        private void LoadEvents()
        {
            Controller.UIEventRaised += UIEventRaised;
        }

        private void UIEventRaised(object sender, UIEventHandlerArgs args)
        {
            if (args == UIEventHandlerArgs.EditStatus)
            {
                var editItemStatusWindow = new EditItemStatus(Controller.MeetingUseCase.MeetingItemStatusLus,Controller.CurrentMeetingItem);
                //editItemStatusWindow.Owner = ((Grid)(((TabControl)((TabItem)this.Parent).Parent)).Parent);
                editItemStatusWindow.Owner = (((((this.Parent as TabItem).Parent as TabControl).Parent as Grid).Parent as Grid).Parent as ResolutionActionSystem.Menu).Parent as Window;
                editItemStatusWindow.ShowDialog();

                if (editItemStatusWindow.StatusSubmitted)
                    Controller.SetItemStatus(editItemStatusWindow.SelectedItemStatusLu);
            }
        }
    }
}
