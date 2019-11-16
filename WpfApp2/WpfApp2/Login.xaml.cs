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
using System.Windows.Shapes;
using WpfApp2.Model;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        QLDiaEntities _db = new QLDiaEntities();
        public Login()
        {

            InitializeComponent();
#if DEBUG
            txtUserName.Text = "admin";
            txtPassWord.Password = "admin";
#endif
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            string userName = txtUserName.Text.Trim();
            string passWord = txtPassWord.Password.ToString().Trim();
            try
            {
                var quyenTruyCapTaiKhoan = _db.TaiKhoan.FirstOrDefault(x => x.tenDangNhap == userName && x.matKhau == passWord).quyenTruyCap;
                if (quyenTruyCapTaiKhoan == true)
                {
                    
                    w.btn_Report.Visibility = Visibility.Visible;
                    this.Visibility = Visibility.Visible;
                    w.Show();
                    this.Close();
                }
                else
                {
                    w.btn_Clerk.Visibility = Visibility.Hidden;
                    this.Visibility = Visibility.Visible;
                    w.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập lại!");
            }



        }
    }
}
