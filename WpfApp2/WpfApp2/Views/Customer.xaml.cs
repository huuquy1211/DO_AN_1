using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WpfApp2.ViewModel;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : UserControl
    {
        RegExr re = new RegExr();
        QLDiaEntities _db = new QLDiaEntities();

        public List<KhachHang> KhachHangs { get; set; }
        public List<PhiTreVM> ListPhiTres { get; set; }


        public Customer()
        {

            KhachHangs = _db.KhachHang.Where(x => x.trangThaiXoa == false).ToList();
            DataContext = this;
            InitializeComponent();
            btnAddCustomer.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            int index = 0;
            try
            {
                index = 0;
            }
            catch (Exception)
            {
            }
            switch (index)
            {
                case 0:
                    Application.Current.Resources["isAddKh"] = Visibility.Visible;
                    break;
            }
        }
        void isReadOnly(bool e)
        {

            txtSDT.IsReadOnly =
            txtTen.IsReadOnly =
            txtCMND.IsReadOnly =
            txtDiaChi.IsReadOnly = e;
        }

        void Clear()
        {
            txtTen.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtMaKH.Text = "";
            txtCMND.Text = "";

            btnCancel.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            btnAddCustomer.Visibility = Visibility.Visible;
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            isReadOnly(false);

            btnAddCustomer.Visibility = Visibility.Collapsed;

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
                    Application.Current.Resources["isAddKh"] = Visibility.Visible;
                    break;
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"^0{1}\d{9}$");
                Regex regexName = new Regex(@"(^(\w){2,8}\s+(\w){1,8})$");

                if (!re.HoTen(txtTen.Text.TrimEnd()))
                {
                    MessageBox.Show("Tên không hợp lệ");
                    txtTen.Focus();
                    return;
                }
                if (!re.SoCMND(txtCMND.Text.Trim()))
                {
                    MessageBox.Show("Số CMND phải là số và từ 10 đến 12 chữ số");
                    txtCMND.Clear();
                    txtCMND.Focus();
                    return;
                }
                if (!re.SoDienThoai(txtSDT.Text.Trim()))
                {
                    MessageBox.Show("SDT phải bắt đầu bằng số 0 và tối đa 10 chứ số");
                    txtSDT.Clear();
                    txtSDT.Focus();
                    return;
                }
                if (!re.DiaChi(txtDiaChi.Text.Trim()))
                {
                    MessageBox.Show("Địa chỉ không hợp lệ");
                    txtDiaChi.Clear();
                    txtDiaChi.Focus();
                    return;
                }
                if (txtMaKH.Text == "")
                {
                    if (_db.KhachHang.Any(x => x.soDienThoai == txtSDT.Text.Trim()))
                    {
                        MessageBox.Show("SDT đã tồn tại");
                        txtSDT.Clear();
                        txtSDT.Focus();
                        return;
                    }

                    KhachHang cus = new KhachHang();
                    cus.hoTen = txtTen.Text;
                    cus.soDienThoai = txtSDT.Text;
                    cus.diaChi = txtDiaChi.Text;
                    cus.soCMND = txtCMND.Text;

                    _db.KhachHang.Add(cus);
                    _db.SaveChanges();

                    KhachHangs = _db.KhachHang.ToList();
                    grvCustomer.ItemsSource = KhachHangs;
                    isReadOnly(true);
                    Clear();
                    MessageBox.Show("Thêm khách hàng thành công!");
                }
                else
                {
                    if (int.TryParse(txtMaKH.Text.Trim(), out int idKH))
                    {
                        var khachhang = _db.KhachHang.FirstOrDefault(x => x.maKhachHang == idKH);
                        if (_db.KhachHang.Any(x => x.soDienThoai == txtSDT.Text.Trim()))
                        {
                            MessageBox.Show("SDT đã tồn tại");
                            txtSDT.Clear();
                            txtSDT.Focus();
                            return;
                        }
                        khachhang.diaChi = txtDiaChi.Text.Trim();
                        khachhang.soDienThoai = txtSDT.Text.Trim();
                        khachhang.hoTen = txtTen.Text.Trim();
                        khachhang.soCMND = txtCMND.Text.Trim();

                        _db.Entry(khachhang).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        KhachHangs = _db.KhachHang.ToList();
                        grvCustomer.ItemsSource = KhachHangs;
                        isReadOnly(true);
                        Clear();
                        btnSave.Visibility = Visibility.Hidden;
                        btnCancel.Visibility = Visibility.Hidden;
                        btnAddCustomer.Visibility = Visibility.Visible;
                        MessageBox.Show("Sửa khách hàng thành công!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            isReadOnly(true);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa khách hàng này?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                MessageBox.Show("Khách hàng không được xóa");
            }
            else
            {
                if (int.TryParse(txtMaKH.Text.Trim(), out int idKH))
                {
                    try
                    {
                        bool trangThaiTraPhiTre = (bool)_db.PhieuThue.FirstOrDefault(x => x.maKhachHang == idKH && x.trangThaiTraPhiTre == false).trangThaiTraPhiTre;
                        if (trangThaiTraPhiTre == false)
                        {
                            MessageBox.Show("Khách hàng chưa trả phí trễ, không được xóa!");
                        }
                        else
                        {
                            var dsXoaKhachHang = _db.KhachHang.FirstOrDefault(x => x.maKhachHang == idKH);
                            dsXoaKhachHang.trangThaiXoa = true;
                            _db.Entry(dsXoaKhachHang).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();

                            MessageBox.Show("Đã xóa khách hàng");
                            KhachHangs = _db.KhachHang.Where(x => x.trangThaiXoa == false).ToList();
                            grvCustomer.ItemsSource = KhachHangs;
                            Clear();
                            btnSave.Visibility = Visibility.Hidden;
                            btnCancel.Visibility = Visibility.Hidden;
                            btnAddCustomer.Visibility = Visibility.Visible;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnAddCustomer.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            int index = 0;
            try
            {
                index = 0;
            }
            catch (Exception)
            {
            }
            switch (index)
            {
                case 0:
                    Application.Current.Resources["isAddKh"] = Visibility.Visible;
                    break;
            }
            if (grvCustomer.SelectedItem is KhachHang)
            {
                var cus = grvCustomer.SelectedItem as KhachHang;

                txtMaKH.Text = cus.maKhachHang.ToString();
                txtSDT.Text = cus.soDienThoai;
                txtDiaChi.Text = cus.diaChi;
                txtCMND.Text = cus.soCMND;
                txtTen.Text = cus.hoTen;
                txtSearch.Text = "";
            }

            isReadOnly(false);
            txtTen.Focus();
        }

        private void GrvCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddCustomer.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            int index = 0;
            try
            {
                index = 0;
            }
            catch (Exception)
            {
            }
            switch (index)
            {
                case 0:
                    Application.Current.Resources["isAddKh"] = Visibility.Visible;
                    break;
            }

            if (grvCustomer.SelectedItem is KhachHang)
            {
                var cus = grvCustomer.SelectedItem as KhachHang;

                txtMaKH.Text = cus.maKhachHang.ToString();
                txtSDT.Text = cus.soDienThoai;
                txtDiaChi.Text = cus.diaChi;
                txtCMND.Text = cus.soCMND;
                txtTen.Text = cus.hoTen;
                txtSearch.Text = "";
                isReadOnly(true);
            }
        }

        private void BtnShowLateBill_Click(object sender, RoutedEventArgs e)
        {
            popupLateBill.IsOpen = true;
            if (dgvLateBill.Items.Count > 0)
                btnThanhToan.IsEnabled = true;
            else
                btnThanhToan.IsEnabled = false;
            try
            {
                int maKHCbb = Int32.Parse(txtMaKH.Text);
                var cus = _db.KhachHang.FirstOrDefault(x => x.maKhachHang == maKHCbb && x.trangThaiXoa == false);
                var cusPhieuThue = _db.PhieuThue.Where(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false).ToList();

                List<ChiTietPhieuThue> listCTPhieuThue = new List<ChiTietPhieuThue>();
                foreach (PhieuThue DSPhieuThue in cusPhieuThue)
                {
                    listCTPhieuThue = _db.ChiTietPhieuThue.Where(x => x.trangThaiTra == true && x.maPhieuThue == DSPhieuThue.maPhieuThue).ToList();


                    foreach (ChiTietPhieuThue item in listCTPhieuThue)
                    {
                        var returnBill = _db.PhieuTra.FirstOrDefault(x => x.maCTPhieuThue == item.maCTPhieuThue);
                        var editPhiTre = _db.PhiTre.FirstOrDefault(x => x.maPhieuTra == returnBill.maPhieuTra && x.tinhTrangThanhToan == false);
                        var dayslate = ((DateTime)returnBill.ngayTra).DayOfYear - ((DateTime)_db.ChiTietPhieuThue.FirstOrDefault(x => x.PhieuThue.maKhachHang == DSPhieuThue.maKhachHang && x.trangThaiTra == true).hanTra).DayOfYear;

                        ListPhiTres = _db.PhiTre.Where(x => x.maPhiTre == editPhiTre.maPhiTre).Select(x => new PhiTreVM()
                        {

                            ngayTra = (DateTime)returnBill.ngayTra,
                            tenTuaDe = x.PhieuTra.ChiTietPhieuThue.Dia.TuaDe.tenTuaDe.ToString(),
                            phiTre = (double)x.tongTien,
                            ngayTre = dayslate,
                            hanTra = (DateTime)x.PhieuTra.ChiTietPhieuThue.hanTra,

                        }).ToList();
                        foreach (PhiTreVM Items in ListPhiTres)
                        {
                            dgvLateBill.Items.Add(Items);
                        }
                    }
                    if (dgvLateBill.Items.Count > 0)
                        btnThanhToan.IsEnabled = true;
                }
            }
            catch (Exception Ex)
            {
            }


        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            popupLateBill.IsOpen = false;
            dgvLateBill.Items.Clear();
        }

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
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(grvCustomer.ItemsSource);


            if (re.SoCMND(filterText))
            {
                cv.Filter = o =>
                {
                    KhachHang kh = o as KhachHang;
                    return (ConvertToUnsign(kh.maKhachHang.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) || ConvertToUnsign(kh.soCMND.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) || ConvertToUnsign(kh.soDienThoai.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())));
                };
            }
            //Tìm sdt
            if (re.SoDienThoai(filterText))
            {
                cv.Filter = o =>
                {
                    KhachHang kh = o as KhachHang;
                    return (ConvertToUnsign(kh.soDienThoai.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())));
                };
            }

            ////Tim mã kh
            //if (re.MaKhachHang(filterText))
            //{
            //    cv.Filter = o =>
            //    {
            //        KhachHang kh = o as KhachHang;
            //        return (ConvertToUnsign(kh.maKhachHang.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())));
            //    };
            //}

            else if (!re.SoCMND(filterText))
            {
                cv.Filter = o =>
                {
                    KhachHang kh = o as KhachHang;
                    return (ConvertToUnsign(kh.hoTen.ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())));
                };
            }
          
            else
            {
                KhachHangs = _db.KhachHang.ToList();
                grvCustomer.ItemsSource = KhachHangs;
            }
        }

        private void BtnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            popupLateBill.IsOpen = false;
            try
            {
                int maKHCbb = Int32.Parse(txtMaKH.Text);
                var cus = _db.KhachHang.FirstOrDefault(x => x.maKhachHang == maKHCbb && x.trangThaiXoa == false);
                var cusPhieuThue = _db.PhieuThue.FirstOrDefault(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false);
                //var cusCtPhieuThue = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maPhieuThue == cusPhieuThue.maPhieuThue && x.trangThaiTra == true);


                List<ChiTietPhieuThue> listCTPhieuThue = new List<ChiTietPhieuThue>();
                listCTPhieuThue = _db.ChiTietPhieuThue.Where(x => x.trangThaiTra == true && x.maPhieuThue == cusPhieuThue.maPhieuThue).ToList();

                foreach (ChiTietPhieuThue item in listCTPhieuThue)
                {
                    var returnBill = _db.PhieuTra.FirstOrDefault(x => x.maCTPhieuThue == item.maCTPhieuThue);
                    var editPhiTre = _db.PhiTre.FirstOrDefault(x => x.maPhieuTra == returnBill.maPhieuTra && x.tinhTrangThanhToan == false);
                    editPhiTre.tinhTrangThanhToan = true;
                }
                var editphieuThue = _db.PhieuThue.FirstOrDefault(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false);
                editphieuThue.trangThaiTraPhiTre = true;

                _db.SaveChanges();
               
                MessageBox.Show("Thanh toán thành công!");
                dgvLateBill.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không có phí trể!");
                dgvLateBill.Items.Clear();
            }
        }
    }
}
