using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Linq;
using DiamondApp.ViewModels;
using System.Data;
using DiamondApp.EntityModel;
using System;
using DiamondApp.DataGridObjectClasses;
using System.Collections;
using DiamondApp.Tools;
using Microsoft.Win32;

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

        public void refreshGridView()
        {
            //UserList.ItemsSource = null;
            //UserList.ItemsSource = AdminViewModel._userListGrid;
            //UserList.Items.Refresh();
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

        private void SRElementySali2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void UserList_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                // Approach: When user finish changes values of cells in row, trigger update
                // Get database Id of user from selected row in datagrid
                object item = UserList.SelectedItem;
                string ID = (UserList.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                //MessageBox.Show(ID);

                //convert ID to int
                int selected = Int32.Parse(ID);

                //object which stores all deta from selected row
                dynamic userRow = UserList.SelectedItem;

                //MessageBox.Show(userRow.UserName);
                //MessageBox.Show(userRow.UserEmail);
                DiamondDBEntities _ctx = new DiamondDBEntities();
                Users userUpdate = (from user in _ctx.Users
                                    where user.Id == selected // int selected!!! you know what i want up to date
                                    select user).First();

                // update values in database by select from 'dynamic userRow' properties 
                // UserName UserEmail etc. are in xaml in line:
                // <TextBox Text="{Binding UserLogin, UpdateSourceTrigger=PropertyChanged}"/>
                // need to update 'dynamic userRow' with Trigger, when user write text into cell

                userUpdate.Name = userRow.UserName;
                userUpdate.Surname = userRow.UserSurname;
                userUpdate.PhoneNum = userRow.UserPhoneNumber;
                userUpdate.Email = userRow.UserEmail;
                userUpdate.Position = userRow.UserPosition;
                userUpdate.AccountType = userRow.UserAccountType;
                userUpdate.Login = userRow.UserLogin;

                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button _myButton = (Button)sender;
            string value = _myButton.CommandParameter.ToString();

            string path = "";

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Pliki PDF | *.pdf";
            openFileDialog.DefaultExt = "pdf";

            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;

            PdfMaker pdf = new PdfMaker();
            pdf.createPdf(value,path);
        }
    }
}
