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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RentView();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ReturnView();
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

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            
            DataContext = new Customer();
        }

        private void Disk_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new Disk();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ClerkView();
        }

        private void Title_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TitleView();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ReportView();
        }
    }
}
