using System.Windows;
using System.Windows.Controls;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {


        public UserMainWindow(int userId)
        {

            InitializeComponent();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void VisibleElement(object sender, RoutedEventArgs e)
        {
            if (this.DataGridProposition.Visibility != Visibility.Hidden)
            {
                this.DataGridProposition.Visibility = Visibility.Hidden;
                this.TabControlProposition.Visibility = Visibility.Visible;
            }
            else
            {
                this.DataGridProposition.Visibility = Visibility.Visible;
                this.TabControlProposition.Visibility = Visibility.Hidden;
            }

         }
    }
}
