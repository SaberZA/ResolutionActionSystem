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
        protected EditMeetingViewModel<EditMeeting> ViewModel { get; set; }

        public EditMeeting()
        {
            InitializeComponent();
            InitController();

            LoadEvents();
        }

        public void InitController()
        {
            this.ViewModel = new EditMeetingViewModel<EditMeeting>(this);
            this.DataContext = ViewModel;
        }
        
        public Controller GetController()
        {
            return this.ViewModel;
        }

        private void LoadEvents()
        {
            ViewModel.UIEventRaised += UIEventRaised;
            ViewModel.InformationEventRaised += ViewModel_InformationEventRaised;
        }

        void ViewModel_InformationEventRaised(object sender, string infoMessage)
        {
            MessageBox.Show(infoMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UIEventRaised(object sender, UIEventHandlerArgs args)
        {
            if (args == UIEventHandlerArgs.EditStatus)
            {
                var editItemStatusWindow = new EditItemStatus(ViewModel.MeetingUseCase.MeetingItemStatusLus,ViewModel.CurrentMeetingItem);
                
                //Get the main window that the calling user control resides in. This allows the popup to be centered in it.
                editItemStatusWindow.Owner = (((((this.Parent as TabItem).Parent as TabControl).Parent as Grid).Parent as Grid).Parent as ResolutionActionSystem.Menu).Parent as Window;
                editItemStatusWindow.ShowDialog();

                if (editItemStatusWindow.StatusSubmitted)
                    ViewModel.SetItemStatus(editItemStatusWindow.SelectedItemStatusLu);
            }
        }
    }
}
