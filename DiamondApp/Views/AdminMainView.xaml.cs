using System.Windows;
using System.Windows.Controls;
using DiamondApp.ViewModels;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : Window
    {


        public AdminMainView(int userId)
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel(userId);  // utworzenie instakcji widoku admina przekazujac id obecnie zalogowanego usera
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
