using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);  // potrzebne do zmiany wyswietlanej waluty $ -> zl

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
                    this.UserList.Visibility = Visibility.Hidden;
                    this.TabControlProposition.Visibility = Visibility.Visible;
                    this.SavePropositionButton.Visibility = Visibility.Visible;
                    break;
                default:
                    this.AdminProposition.Visibility = Visibility.Visible;
                    this.UserList.Visibility = Visibility.Hidden;
                    this.TabControlProposition.Visibility = Visibility.Hidden;
                    this.SavePropositionButton.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void VisibleElementAfterSavePropClick(object sender, RoutedEventArgs e)
        {
            AdminProposition.Visibility = Visibility.Visible;
            UserList.Visibility = Visibility.Hidden;
            TabControlProposition.Visibility = Visibility.Hidden;
            SavePropositionButton.Visibility = Visibility.Hidden;
        }

        private void VisibleElement2(object sender, RoutedEventArgs e)
        {
            AdminProposition.Visibility = Visibility.Hidden;
            UserList.Visibility = Visibility.Visible;
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
          
        }
    }
}
