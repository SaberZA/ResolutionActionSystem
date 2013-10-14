using System.Windows.Controls;

namespace ResolutionActionSystem
{
    public abstract class Controller
    {
        protected UserControl PrimaryControl;

        protected Controller(UserControl userControl)
        {
            PrimaryControl = userControl;
        }
    }
}