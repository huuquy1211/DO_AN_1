using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using WpfApp2.Model;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for ClerkView.xaml
    /// </summary>
    public partial class ClerkView : UserControl
    {
        RegExr re = new RegExr();
        QLDiaEntities _db = new QLDiaEntities();

        static Regex ConvertToUnsign_rg = null;
        public List<NhanVien> NhanViens { get; set; }
        public static string ConvertToUnsign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static string VietHoa(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;

            string result = "";

            //lấy danh sách các từ  

            string[] words = s.Split(' ');

            foreach (string word in words)
            {
                // từ nào là các khoảng trắng thừa thì bỏ  
                if (word.Trim() != "")
                {
                    if (word.Length > 1)
                        result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    else
                        result += word.ToUpper() + " ";
                }

            }
            return result.Trim();
        }
        private void userClerk_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        public ClerkView()
        {
            NhanViens = _db.NhanVien.ToList();
            DataContext = this;
            InitializeComponent();
        }
        void isReadOnly(bool e)
        {

            txtTenNV.IsReadOnly =
            txtSDTNV.IsReadOnly =
            txtDiaChiNV.IsReadOnly = e;
        }

        void Clear()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtSDTNV.Text = "";
            txtDiaChiNV.Text = "";
            txtMaNV.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            btnAddClerk.Visibility = Visibility.Visible;

        }
        private void BtnAddClerk_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            isReadOnly(false);
            btnAddClerk.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
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

        private void GrvClerk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (grvResever.SelectedItem is NhanVien)
            {
                var cus = grvResever.SelectedItem as NhanVien;

                txtMaNV.Text = cus.maNhanVien.ToString();
                txtSDTNV.Text = cus.soDienThoai;
                txtDiaChiNV.Text = cus.diaChi;
                txtTenNV.Text = cus.hoTen;
                txtSearchNV.Text = "";
            }
            isReadOnly(true);
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnAddClerk.Visibility = Visibility.Visible;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            isReadOnly(true);
            txtMaNV.Visibility = Visibility.Visible;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"^[0-9]{10}$");
                Regex regexName = new Regex(@"^[a-z -']+$");


                if (txtMaNV.Text == "")
                {
                    if (txtSDTNV.Text == "" || txtDiaChiNV.Text == "" || txtTenNV.Text == "")
                    {
                        MessageBox.Show("Không bỏ trống bất kì trường nhập nào");
                    }
                    else
                    {
                        if (!re.SoDienThoai(txtSDTNV.Text))
                        {
                            MessageBox.Show("SDT không hợp lệ");
                            txtSDTNV.Clear();
                            txtSDTNV.Focus();
                            return;
                        }
                        if (!re.HoTen(txtTenNV.Text.TrimEnd()))
                        {
                            MessageBox.Show("Tên không hợp lệ");
                            txtTenNV.Clear();
                            txtTenNV.Focus();
                            return;
                        }
                        if (_db.NhanVien.Any(x => x.soDienThoai == txtSDTNV.Text.Trim()))
                        {
                            MessageBox.Show("SDT đã tồn tại");
                            txtSDTNV.Clear();
                            txtSDTNV.Focus();
                            return;
                        }
                        using (var tran = _db.Database.BeginTransaction())
                        {
                            NhanVien cus = new NhanVien();
                            TaiKhoan t = new TaiKhoan();
                            t.tenDangNhap = "nv" + txtSDTNV.Text;
                            t.quyenTruyCap = true;
                            t.matKhau = txtSDTNV.Text;
                            cus.hoTen = VietHoa(txtTenNV.Text);
                            cus.soDienThoai = txtSDTNV.Text;
                            cus.TaiKhoan = t;
                            cus.diaChi = txtDiaChiNV.Text;
                            t.maTaiKhoan = cus.maTaiKhoan;
                            _db.TaiKhoan.Add(t);
                            _db.NhanVien.Add(cus);

                            _db.SaveChanges();

                            tran.Commit();
                            NhanViens = _db.NhanVien.ToList();
                            grvResever.ItemsSource = NhanViens;
                            isReadOnly(true);
                            Clear();
                        }
                    }

                }
                else
                {
                    NhanVien nv = grvResever.SelectedItem as NhanVien;
                    if (nv != null)
                    {
                        nv.hoTen = txtTenNV.Text;
                        nv.soDienThoai = txtSDTNV.Text;
                        nv.diaChi = txtDiaChiNV.Text;
                        _db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                        NhanViens = _db.NhanVien.ToList();
                        grvResever.ItemsSource = NhanViens;
                        isReadOnly(true);
                        Clear();
                    }
                    MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDeleteNV_Click(object sender, RoutedEventArgs e)
        {
            NhanVien nv = grvResever.SelectedItem as NhanVien;
            if (MessageBox.Show("Bạn có thực sự muốn xóa?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (nv != null)
                {
                    var tk = _db.TaiKhoan.FirstOrDefault(x => x.maTaiKhoan == nv.maTaiKhoan);
                    _db.NhanVien.Remove(nv);
                    _db.TaiKhoan.Remove(tk);
                    _db.SaveChanges();
                    NhanViens = _db.NhanVien.ToList();
                    grvResever.ItemsSource = NhanViens;
                }
            }
        }



        private void btnUpdateNV_Click(object sender, RoutedEventArgs e)
        {
            btnAddClerk.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            isReadOnly(false);
            Application.Current.Resources["isAddClerk"] = Visibility.Visible;
        }

        private void txtSearchNV_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(grvResever.ItemsSource);


            if (!string.IsNullOrEmpty(filterText))
            {
                cv.Filter = o =>
                {
                    NhanVien p = o as NhanVien;
                    return (ConvertToUnsign(p.soDienThoai.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) || ConvertToUnsign(p.hoTen.ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) || ConvertToUnsign(p.soDienThoai.ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())));
                };
            }
            if (txtSearchNV.Text == "")
            {
                NhanViens = _db.NhanVien.ToList();
                grvResever.ItemsSource = NhanViens;
            }
        }
    }
}
