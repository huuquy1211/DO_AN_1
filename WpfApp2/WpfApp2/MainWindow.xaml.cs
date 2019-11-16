using WpfApp2.Views;
using WpfApp2.ViewModel;
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
using MaterialDesignThemes.Wpf;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Disk d1;
        public MainWindow()
        {
            InitializeComponent();
          


        }


        
        private void Logout_Selected(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            this.Visibility = Visibility.Hidden;
            l.Show();
            this.Close();
        }

        private void Exit_Selected(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_MenuCus_Click(object sender, RoutedEventArgs e)
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
                    Application.Current.Resources["isMenuCus"] = Visibility.Visible;
                    Application.Current.Resources["isMenuClerk"] = Visibility.Collapsed;
                    Application.Current.Resources["isMenuDisk"] = Visibility.Collapsed;



                    break;

            }
        }

        private void Btn_MenuClerk_Click(object sender, RoutedEventArgs e)
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
                    Application.Current.Resources["isMenuCus"] = Visibility.Collapsed;
                    Application.Current.Resources["isMenuClerk"] = Visibility.Visible;
                    Application.Current.Resources["isMenuDisk"] = Visibility.Collapsed;
                    break;
            }
        }

        private void Btn_MenuDisk_Click(object sender, RoutedEventArgs e)
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
                    Application.Current.Resources["isMenuCus"] = Visibility.Collapsed;
                    Application.Current.Resources["isMenuClerk"] = Visibility.Collapsed;
                    Application.Current.Resources["isMenuDisk"] = Visibility.Visible;
                    break;

            }
        }

        private void Btn_Disk_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Disk();
        }

        private void Btn_Rent_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new RentView();
        }

        private void Btn_Return_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ReturnView();
        }

        private void Btn_Customer_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Customer();

        }

        private void Btn_Clerk_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ClerkView();
        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new ReportView();
        }

        private void Btn_Title_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = new TitleView();
        }
    }
}
