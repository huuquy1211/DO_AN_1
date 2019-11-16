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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = int.Parse(((Button)e.Source).Uid);
            }
            catch (Exception)
            {
                //index = int.Parse(((PackIcon)e.Source).Uid);
            }

            switch (index)
            {
                case 0:
                    Application.Current.Resources["isReprotTitle"] = Visibility.Visible;



                    break;

            }
        }

        private void Btn_ReportTile_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = int.Parse(((Button)e.Source).Uid);
            }
            catch (Exception)
            {
                //index = int.Parse(((PackIcon)e.Source).Uid);
            }

            switch (index)
            {
                case 0:
                    Application.Current.Resources["isReportTitle"] = Visibility.Visible;
                    Application.Current.Resources["isReportDisk"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDT"] = Visibility.Collapsed;


                    break;

            }
        }

        private void Btn_ReportDisk_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = int.Parse(((Button)e.Source).Uid);
            }
            catch (Exception)
            {
                //index = int.Parse(((PackIcon)e.Source).Uid);
            }

            switch (index)
            {
                case 0:
                    Application.Current.Resources["isReportTitle"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDisk"] = Visibility.Visible;
                    Application.Current.Resources["isReportDT"] = Visibility.Collapsed;



                    break;

            }
        }

        private void Btn_DoanhThu_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            try
            {
                index = int.Parse(((Button)e.Source).Uid);
            }
            catch (Exception)
            {
                //index = int.Parse(((PackIcon)e.Source).Uid);
            }

            switch (index)
            {
                case 0:
                    Application.Current.Resources["isReportTitle"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDisk"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDT"] = Visibility.Visible;



                    break;

            }
        }
    }
}
