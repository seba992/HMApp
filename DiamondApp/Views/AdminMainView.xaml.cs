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
            AdminViewModel adminView = new AdminViewModel(userId);
            DataContext = adminView;  // utworzenie instakcji widoku admina przekazujac id obecnie zalogowanego usera
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void VisibleElement(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((MenuItem)sender).Header.ToString());
            switch (((MenuItem)sender).Header.ToString())
            {
                case "Edytuj":
                case "Dodaj":
                    this.DataGridProposition.Visibility = Visibility.Hidden;
                    this.TabControlProposition.Visibility = Visibility.Visible;
                    break;
                default:
                    this.DataGridProposition.Visibility = Visibility.Visible;
                    this.TabControlProposition.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
