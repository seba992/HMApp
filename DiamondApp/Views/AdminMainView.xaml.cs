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
            //MessageBox.Show(((MenuItem)sender).Header.ToString());
            switch (((MenuItem)sender).Header.ToString())
            {
                case "Edytuj":
                case "Dodaj":
                    this.AdminProposition.Visibility = Visibility.Hidden;
                    this.TabControlProposition.Visibility = Visibility.Visible;
                    this.SavePropositionButton.Visibility = Visibility.Visible;
                    break;
                default:
                    this.AdminProposition.Visibility = Visibility.Visible;
                    this.TabControlProposition.Visibility = Visibility.Hidden;
                    this.SavePropositionButton.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void VisibleElementAfterSavePropClick(object sender, RoutedEventArgs e)
        {
            AdminProposition.Visibility = Visibility.Visible;
            TabControlProposition.Visibility = Visibility.Hidden;
            SavePropositionButton.Visibility = Visibility.Hidden;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserView addUserView = new AddUserView();
            addUserView.Show();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            EditUserView editUserView = new EditUserView();
            editUserView.Show();
        }
    }
}
