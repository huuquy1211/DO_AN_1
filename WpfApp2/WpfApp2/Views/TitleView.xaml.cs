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
    /// Interaction logic for TitleView.xaml
    /// </summary>
    public partial class TitleView : UserControl
    {
        public TitleView()
        {
            InitializeComponent();
        }
        void isReadOnly(bool e)
        {

            txtTenTD.IsReadOnly =
            txtGia.IsReadOnly =
            txtSL.IsReadOnly =
            txtMota.IsReadOnly = e;
        }
        private void BtnAddTitle_Click(object sender, RoutedEventArgs e)
        {
            isReadOnly(false);
            btnAddTitle.Visibility = Visibility.Collapsed;
            
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
                    Application.Current.Resources["isAddTitle"] = Visibility.Visible;



                    break;

            }
            txtTenTD.Focus();
        }
    }
}
