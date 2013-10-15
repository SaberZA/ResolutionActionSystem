using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace ResolutionActionSystem
{
    public class MenuController<T> : Controller where T: Menu
    {
        public MenuController(T menuControl)
            : base(menuControl)
        {
            MenuButtons = new List<Button>();
        }

        public List<Button> MenuButtons { get; set; }

        public string ActiveButtonTag { get; set; }

        public int ActiveTabIndex { get; set; }

        public void RegisterButton(Button button)
        {
            if (!MenuButtons.Contains(button))
            {
                MenuButtons.Add(button);
                button.Click += updateMenuSelection;
            }
        }

        void updateMenuSelection(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                ActiveButtonTag = (string)button.Tag;
                SetActiveButton();

                ActiveTabIndex = Convert.ToInt32(ActiveButtonTag);
                SetActiveTab();
            }
        }

        private void SetActiveTab()
        {
            ((T) PrimaryControl).tabControl1.SelectedIndex = ActiveTabIndex;
        }

        private void SetActiveButton()
        {
            foreach (var menuButton in MenuButtons)
            {
                menuButton.Background = menuButton.Tag.Equals(ActiveButtonTag) ? Brushes.LightGray : Brushes.White;
                menuButton.BorderBrush = menuButton.Tag.Equals(ActiveButtonTag) ? Brushes.LightGray : Brushes.White;
            }
        }
    }
}
