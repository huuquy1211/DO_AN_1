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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : UserControl
    {
        public Customer()
        {
            InitializeComponent();
        }
        void isReadOnly(bool e)
        {
           
            txtSDT.IsReadOnly =
            txtTen.IsReadOnly =
            txtCMND.IsReadOnly=
            txtDiaChi.IsReadOnly = e;
        }
        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            isReadOnly(false);
            btnAddCustomer.Visibility = Visibility.Collapsed;
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
                    Application.Current.Resources["isAddKh"] = Visibility.Visible;
                  


                    break;

            }
        }
    }
}
