using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ResolutionActionSystem
{
    public class EditMeetingController<T> : Controller where T: EditMeeting
    {
        public EditMeetingController(T userControl) 
            : base(userControl)
        {

        }
    }
}
