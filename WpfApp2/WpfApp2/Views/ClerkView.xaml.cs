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
    /// Interaction logic for ClerkView.xaml
    /// </summary>
    public partial class ClerkView : UserControl
    {
        public ClerkView()
        {
            InitializeComponent();
        }
        void isReadOnly(bool e)
        {

            txtTenNV.IsReadOnly =
            txtSDTNV.IsReadOnly =
            
            txtDiaChiNV.IsReadOnly = e;
        }
        private void BtnAddClerk_Click(object sender, RoutedEventArgs e)
        {
            isReadOnly(false);
            btnAddClerk.Visibility = Visibility.Collapsed;
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
                    Application.Current.Resources["isAddClerk"] = Visibility.Visible;



                    break;

            }
        }
    }
}
