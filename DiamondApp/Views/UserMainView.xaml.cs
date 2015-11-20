using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DiamondApp.ViewModels;
using DiamondApp.Tools;
using Microsoft.Win32;

namespace DiamondApp.Views
{
    /// <summary>
    /// Interaction logic for UserMainView.xaml
    /// </summary>
    public partial class UserMainView : Window
    {
        public UserMainView(int userId)
        {
            InitializeComponent();

            // Set the binding source here.
            UserViewModel userView = new UserViewModel(userId);
            DataContext = userView;  // utworzenie instakcji widoku admina przekazujac id obecnie zalogowanego usera
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);  // potrzebne do zmiany wyswietlanej waluty $ -> zl
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void VisibleElement(object sender, RoutedEventArgs e)
        {
            switch (((MenuItem)sender).Header.ToString())
            {
                case "Edytuj":
                case "Dodaj":
                    this.UserProposition.Visibility = Visibility.Hidden;
                    this.TabControlProposition.Visibility = Visibility.Visible;
                    this.SavePropositionButton.Visibility = Visibility.Visible;
                    break;
                default:
                    this.UserProposition.Visibility = Visibility.Visible;
                    this.TabControlProposition.Visibility = Visibility.Hidden;
                    this.SavePropositionButton.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void VisibleElementAfterSavePropClick(object sender, RoutedEventArgs e)
        {
            UserProposition.Visibility = Visibility.Visible;
            TabControlProposition.Visibility = Visibility.Hidden;
            SavePropositionButton.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button _myButton = (Button)sender;
            string value = _myButton.CommandParameter.ToString();

            string path = "";

            SaveFileDialog openFileDialog = new SaveFileDialog();

            if (openFileDialog.ShowDialog() == true)
                path = openFileDialog.FileName;

            PdfMaker pdf = new PdfMaker();
            pdf.createPdf(value, path);
        }
    }
}
