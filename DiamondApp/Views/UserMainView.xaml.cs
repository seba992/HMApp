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
using DiamondApp.Tools.Pdf;
using DiamondApp.ViewModels.UserViewModels;
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
            pdf.createPdf(value, path);
        }
    }
}
