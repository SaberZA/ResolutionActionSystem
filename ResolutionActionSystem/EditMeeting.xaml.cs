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
    /// Interaction logic for EditMeeting.xaml
    /// </summary>
    public partial class EditMeeting : UserControl, IControllable
    {
        protected EditMeetingController<EditMeeting> Controller { get; set; }

        public EditMeeting()
        {
            InitializeComponent();
            InitController();
        }

        public void InitController()
        {
            this.Controller = new EditMeetingController<EditMeeting>(this);
        }
        
        public Controller GetController()
        {
            return this.Controller;
        }

        public void RegisterPropertyControls()
        {
            
        }
    }
}
