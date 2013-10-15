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

namespace ResolutionActionSystem
{
    /// <summary>
    /// Interaction logic for CaptureMeeting.xaml
    /// </summary>
    public partial class CaptureMeeting : UserControl, IControllable
    {
        protected CaptureMeetingController<CaptureMeeting> Controller { get; set; }

        public CaptureMeeting()
        {
            InitializeComponent();
            InitController();

        }

        public void InitController()
        {
            this.Controller = new CaptureMeetingController<CaptureMeeting>(this);
        }

        public Controller GetController()
        {
            return this.Controller;
        }

        
    }
}
