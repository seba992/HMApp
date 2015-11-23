using DiamondApp.EntityModel;
using DiamondApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for EditUserView.xaml
    /// </summary>
    public partial class EditUserView : Window
    {
        public EditUserView()
        {
            InitializeComponent();
            EditUserViewModel editUserView = new EditUserViewModel();
            DataContext = editUserView;
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
                int tmp = 0;
                if (userRow.UserAccountType == "Administrator")
                {
                    tmp = 1;
                }
                else
                {
                    tmp = 2;
                }

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
                userUpdate.AccountType = tmp;
                userUpdate.Login = userRow.UserLogin;

                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
