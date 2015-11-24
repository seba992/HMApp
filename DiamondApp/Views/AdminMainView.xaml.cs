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

        private void VisibleElement2(object sender, RoutedEventArgs e)
        {
            AdminProposition.Visibility = Visibility.Hidden;
            
            TabControlProposition.Visibility = Visibility.Hidden;
            SavePropositionButton.Visibility = Visibility.Hidden;
        }

        private void SRElementySali2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
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
