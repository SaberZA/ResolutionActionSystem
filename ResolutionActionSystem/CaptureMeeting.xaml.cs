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
using ResolutionActionSystem;
using ResolutionActionSystem.Controllers;

namespace ResolutionActionSystem
{
    /// <summary>
    /// Interaction logic for CaptureMeeting.xaml
    /// </summary>
    public partial class CaptureMeeting : UserControl, IControllable
    {
        protected CaptureMeetingViewModel<CaptureMeeting> ViewModel { get; set; }

        public CaptureMeeting()
        {
            InitializeComponent();
            InitController();
            this.ViewModel.InformationEventRaised += Controller_InformationEventRaised;
        }

        void Controller_InformationEventRaised(object sender, string infoMessage)
        {
            MessageBox.Show(infoMessage, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void InitController()
        {
            this.ViewModel = new CaptureMeetingViewModel<CaptureMeeting>(this);
            this.DataContext = ViewModel;
        }

        public Controller GetController()
        {
            return this.ViewModel;
        }

        
    }
}
