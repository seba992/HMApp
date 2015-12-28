using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DiamondApp.ViewModels;
using DiamondApp.Tools;
using DiamondApp.Tools.Pdf;
using DiamondApp.ViewModels.AdminViewModels;
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
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);  // potrzebne do zmiany wyswietlanej waluty $ -> zl

        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;
            string value = myButton.CommandParameter.ToString();

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
