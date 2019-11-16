using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp2.Model;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for TitleView.xaml
    /// </summary>
    public partial class TitleView : UserControl
    {
        //static private TitleView instance = new TitleView();
        //static public TitleView getInstance
        //{
        //    get { return instance; }
        //}
        QLDiaEntities _db = new QLDiaEntities();
        RegExr re = new RegExr();
        public List<TuaDe> Titles { get; set; }

        public TitleView()
        {
            //Titles = _db.TuaDes.Where(x => x.soLuong == 10).Select(x => new TitleVM()

            //{
            //    Id = x.maTuaDe,
            //    Ten = x.tenTuaDe,
            //    SoLuong =(int) x.soLuong,
            //    NgayThue =(int) x.soNgaythue,
            //    Loai = x.moTa,
            //    DonGia =(float) x.donGia,
            //}).ToList();
            Titles = _db.TuaDe.Where(x => x.trangThai == true).ToList();
            //Application.Current.Resources["isManagerTitle"] = Visibility.Visible;
            DataContext = this;

            InitializeComponent();
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }

        void isReadOnly(bool e)
        {

            txtTenTD.IsReadOnly =
            txtGia.IsReadOnly =
            txtSL.IsReadOnly =
            txtSongay.IsReadOnly =
            txtLoaiTua.IsReadOnly = e;
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
        void Clear()
        {
            txtGia.Text = "";
            txtMaTD.Text = "";
            txtLoaiTua.Text = "";
            txtSL.Text = "";
            txtSongay.Text = "";
            txtTenTD.Text = "";

        }
        private void BtnAddTitle_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            isReadOnly(false);
            btnAddTitle.Visibility = Visibility.Hidden;


            txtTenTD.Focus();
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
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void GrvTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (grvTitle.SelectedItem is TuaDe)
            {
                var title = grvTitle.SelectedItem as TuaDe;
                txtMaTD.Text = title.maTuaDe.ToString();
                txtTenTD.Text = title.tenTuaDe.ToString();
                txtLoaiTua.Text = title.moTa;
                txtSongay.Text = title.ngayChoThue.ToString();
                txtSL.Text = title.soLuong.ToString();
                txtGia.Text = title.donGia.ToString();
            }
            isReadOnly(true);

            //btnSave.Visibility = Visibility.Hidden;
            //btnCancel.Visibility = Visibility.Hidden;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnAddTitle.Visibility = Visibility.Hidden;
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
            if (grvTitle.SelectedItem is TuaDe)
            {
                var title = grvTitle.SelectedItem as TuaDe;
                txtMaTD.Text = title.maTuaDe.ToString();
                txtTenTD.Text = title.tenTuaDe.ToString();
                txtLoaiTua.Text = title.moTa;
                txtSongay.Text = title.ngayChoThue.ToString();
                txtSL.Text = title.soLuong.ToString();
                txtGia.Text = title.donGia.ToString();
            }
            isReadOnly(false);
            txtTenTD.IsReadOnly = true;
            txtLoaiTua.IsReadOnly = true;
            txtGia.Focus();
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!re.DiaChi(txtTenTD.Text.Trim()))
            {
                MessageBox.Show("Tên tựa đề không hợp lệ");
                txtTenTD.Clear();
                txtTenTD.Focus();
                return;
            }
            if (!re.MaKhachHang(txtSL.Text.TrimEnd()))
            {
                MessageBox.Show("Nhập đúng số lượng");
                txtSL.Focus();
                return;
            }
            if (!re.MaKhachHang(txtGia.Text.TrimEnd()))
            {
                MessageBox.Show("Nhập đúng giá thuê");
                txtGia.Focus();
                return;
            }
            if (!re.MaKhachHang(txtSongay.Text.TrimEnd()))
            {
                MessageBox.Show("Nhập đúng số ngày");
                txtSongay.Focus();
                return;
            }
            if (!re.Type(txtLoaiTua.Text.TrimEnd()))
            {
                MessageBox.Show("Nhập loại là Phim hoặc Nhạc");
                txtLoaiTua.Focus();
                return;
            }
            if (txtMaTD.Text == "" && int.TryParse(txtSongay.Text.Trim(), out int ngaythue)
                    && float.TryParse(txtGia.Text.Trim(), out float price)
                     && int.TryParse(txtSL.Text.Trim(), out int quantity))
            {
                if (_db.TuaDe.Any(x => x.tenTuaDe == txtTenTD.Text.Trim() && x.moTa == txtLoaiTua.Text.Trim()))
                {
                    MessageBox.Show("Tựa đề đã tồn tại");
                    txtTenTD.Clear();
                    txtTenTD.Focus();
                    return;
                }


                TuaDe title = new TuaDe();
                title.donGia = price;
                title.ngayChoThue = (int)ngaythue;
                title.soLuong = (int)quantity;
                title.trangThai = true;
                title.tenTuaDe = txtTenTD.Text.Trim();
                title.moTa = txtLoaiTua.Text.Trim();

                _db.TuaDe.Add(title);
                _db.SaveChanges();

                Titles = _db.TuaDe.Where(x => x.trangThai == true).ToList();
                grvTitle.ItemsSource = Titles;
                isReadOnly(false);
                Clear();

            }
            else if (int.TryParse(txtMaTD.Text.Trim(), out int idTitle) && txtMaTD.Text != ""
                     && int.TryParse(txtSongay.Text.Trim(), out int ngaythueedit)
                     && float.TryParse(txtGia.Text.Trim(), out float priceedit)
                      && int.TryParse(txtSL.Text.Trim(), out int quantityedit))
            {
                var title = _db.TuaDe.FirstOrDefault(x => x.maTuaDe == idTitle);
                title.donGia = priceedit;
                title.ngayChoThue = ngaythueedit;
                title.soLuong = quantityedit;

                _db.Entry(title).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                Titles = _db.TuaDe.Where(x => x.trangThai == true).ToList();
                grvTitle.ItemsSource = Titles;
                isReadOnly(true);
                Clear();
                btnSave.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;
                btnAddTitle.Visibility = Visibility.Visible;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            isReadOnly(true);
            Clear();
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnAddTitle.Visibility = Visibility.Visible;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            

                if (MessageBox.Show("Bạn chắc chắn muốn xóa tựa đề này?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                MessageBox.Show("Tựa đề không được xóa");
            }
            else
            {
                if (int.TryParse(txtMaTD.Text.Trim(), out int idTitle))
                {
                    var title = _db.TuaDe.FirstOrDefault(x => x.maTuaDe == idTitle);
                    var disk = _db.Dia.FirstOrDefault(x => x.maTuaDe == idTitle);
                    if (disk.trangThai == 1)
                    {
                        MessageBox.Show("Tựa đề có đĩa đang thuê không được xoá");
                    }
                    else
                    {
                        title.trangThai = false;
                        _db.Entry(title).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                        MessageBox.Show("Đã xóa tựa đề");
                    }
                    Titles = _db.TuaDe.Where(x => x.trangThai == true).ToList();
                    grvTitle.ItemsSource = Titles;
                    Clear();
                    btnSave.Visibility = Visibility.Hidden;
                    btnCancel.Visibility = Visibility.Hidden;
                    btnAddTitle.Visibility = Visibility.Visible;
                }
            }

        }

        //private void BtnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    if (txtSearch.Text.ToLower().Trim() == "")
        //    {
        //        Titles = _db.TuaDe.Where(x => x.trangThai == true).ToList();
        //        grvTitle.ItemsSource = Titles;
        //    }
        //    //search theo tên hoặc theo loại
        //    else if (!re.SoCMND(txtSearch.Text.TrimEnd()))
        //    {

        //        Titles = _db.TuaDe.Where(x => x.tenTuaDe.Contains(txtSearch.Text) && x.trangThai == true || x.moTa.Contains(txtSearch.Text) && x.trangThai == true).ToList();
        //        grvTitle.ItemsSource = Titles;
        //    }
        //    //search theo mã
        //    else if (re.SoCMND(txtSearch.Text.TrimEnd()))
        //    {
        //        int m = Int32.Parse(txtSearch.Text);
        //        Titles = _db.TuaDe.Where(x => x.maTuaDe == m && x.trangThai == true /*|| x.donGia ==m &&x.trangThai==true*/).ToList();
        //        grvTitle.ItemsSource = Titles;
        //    }

        //}

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(grvTitle.ItemsSource);


            if (!re.SoCMND(filterText))
            {
                cv.Filter = o =>
                {
                    TuaDe kh = o as TuaDe;
                    return ( ConvertToUnsign(kh.tenTuaDe.ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) || ConvertToUnsign(kh.moTa.ToString().ToUpper()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) );
                };
            }
            else if(re.SoCMND(filterText))
            {
                cv.Filter = o =>
                {
                    TuaDe kh = o as TuaDe;
                    return (ConvertToUnsign(kh.maTuaDe.ToString()).Contains(ConvertToUnsign(filterText.ToUpper().Trim())) );
                };
            }
            else
            {
                Titles = _db.TuaDe.ToList();
                grvTitle.ItemsSource = Titles;
            }
        }
    }
}
