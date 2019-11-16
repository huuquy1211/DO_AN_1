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
using WpfApp2.Model;
using WpfApp2.ViewModel;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for RentView.xaml
    /// </summary>
    public partial class RentView : UserControl
    {
        QLDiaEntities _db = new QLDiaEntities();
        public double TotalPrice { get; set; }
        public List<KhachHang> ListCus { get; set; }
        public List<PhiTreVM> ListPhiTre { get; set; }

        public List<DiaVM> ListCD { get; set; }

        public List<Dia> ListDisk { get; set; }
        public List<DiaTraVM> ListCDHasRent { get; set; }
        public List<PhieuTra> ListReturn { get; set; }




        public RentView()
        {
            InitialCD();
            InitialCustomer();
            InitializeComponent();
        }

        void InitialPhiTre()
        {
            try
            {
                string tenKHCbb = cbbKH.Text;
                var cus = _db.KhachHang.FirstOrDefault(x => x.hoTen == tenKHCbb && x.trangThaiXoa == false);
                var cusPhieuThue = _db.PhieuThue.Where(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false).ToList();
                //var cusPhieuThue = _db.KhachHang.FirstOrDefault(x => x.hoTen == tenKHCbb && x.trangThaiXoa == false).PhieuThue;
                //var cusCtPhieuThue = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maPhieuThue == cusPhieuThue.maPhieuThue && x.trangThaiTra == true);


                if (cusPhieuThue.Count > 0)
                {
                    foreach (PhieuThue DSPhieuThue in cusPhieuThue)
                    {

                        List<ChiTietPhieuThue> listCTPhieuThue = new List<ChiTietPhieuThue>();
                        listCTPhieuThue = _db.ChiTietPhieuThue.Where(x => x.trangThaiTra == true && x.maPhieuThue == DSPhieuThue.maPhieuThue).ToList();
                        if (listCTPhieuThue.Count > 0)
                        {
                            foreach (ChiTietPhieuThue item in listCTPhieuThue)
                            {
                                var returnBill = _db.PhieuTra.FirstOrDefault(x => x.maCTPhieuThue == item.maCTPhieuThue);

                                //List<ChiTietPhieuThue> listCTPhieuThueKH = new List<ChiTietPhieuThue>();
                                //listCTPhieuThueKH.Add(item);

                                //var hanTraKH = (DateTime)_db.ChiTietPhieuThue.FirstOrDefault(x => x.maPhieuThue == cusPhieuThue.maPhieuThue).hanTra;
                                var hanTraKH = (DateTime)item.hanTra;

                                if (cbbKH.SelectedIndex > -1)
                                {
                                    DgvPhieuTre.IsEnabled = true;
                                    if (returnBill.ngayTra > hanTraKH)
                                    {

                                        var dayslate = ((DateTime)returnBill.ngayTra).DayOfYear - ((DateTime)_db.ChiTietPhieuThue.FirstOrDefault(x => x.PhieuThue.maKhachHang == DSPhieuThue.maKhachHang && x.trangThaiTra == true).hanTra).DayOfYear;

                                        var overdue = new PhiTreVM();
                                        overdue.tenTuaDe = _db.PhieuTra.FirstOrDefault(y => y.maPhieuTra == returnBill.maPhieuTra).ChiTietPhieuThue.Dia.TuaDe.tenTuaDe;
                                        overdue.phiTre = (double)_db.PhiTre.FirstOrDefault(y => y.tinhTrangThanhToan == false).tongTien;
                                        overdue.ngayTre = dayslate;
                                        overdue.maPhiTre = _db.PhiTre.FirstOrDefault(x => x.maPhieuTra == returnBill.maPhieuTra).maPhiTre;

                                        foreach (PhiTreVM items in DgvPhieuTre.Items)
                                        {
                                            if (items.maPhiTre == overdue.maPhiTre)
                                            {
                                                MessageBox.Show("Đã có phí trễ khách hàng này!");
                                                return;
                                            }
                                        }
                                        DgvPhieuTre.Items.Add(overdue);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Khách hàng không có phí trễ");
                                        grbCD.IsEnabled = true;
                                        DgvPhieuTre.Items.Clear();
                                        return;
                                    }
                                }
                                else
                                {
                                    DgvPhieuTre.IsEnabled = false;
                                }

                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Khách hàng không có phí trễ");
                    grbCD.IsEnabled = true;
                    DgvPhieuTre.Items.Clear();
                    return;
                }

                if (DgvPhieuTre.Items.Count == 0)
                {
                    MessageBox.Show("Khách hàng không có phí trễ");
                    grbCD.IsEnabled = true;
                    DgvPhieuTre.Items.Clear();
                    return;
                }
                else grbCD.IsEnabled = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Khách hàng không có phí trễ");
                grbCD.IsEnabled = true;
                DgvPhieuTre.Items.Clear();
            }
        }

        void InitialCD()
        {
            ListCD = _db.Dia.Where(x => x.trangThai == 0).Select(x => new DiaVM()
            {
                maDia = x.maDia,
                tenTuaDe = x.TuaDe.tenTuaDe,
                loai = x.loai
            }).ToList();
        }
        void InitialCustomer()
        {
            ListCus = _db.KhachHang.Where(x => x.maKhachHang != 0).ToList();
        }



        private void BtnLapPhieuThue_Click(object sender, RoutedEventArgs e)
        {

            DgvPhieuTre.Items.Clear();
            InitialPhiTre();
            if (DgvPhieuTre.Items.Count > 0)
                btnThanhToanPhiTre.IsEnabled = true;
            else
                btnThanhToanPhiTre.IsEnabled = false;
        }

        private void CbbKH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DgvPhieuTre.Items.Clear();
            grbCD.IsEnabled = false;
            DataGridPThue.Items.Clear();
            btnXoaCd.IsEnabled = false;
            if (cbbKH.SelectedIndex > -1)
            {
                btnLapPhieuThue.IsEnabled = true;
            }
            else
            {
                btnLapPhieuThue.IsEnabled = false;
            }
        }

        private void CbbCD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbCD.SelectedIndex > -1)
            {
                btnThemPhieuThue.IsEnabled = true;
                btnHuyPhieuThue.IsEnabled = true;
            }
            else
            {
                btnThemPhieuThue.IsEnabled = false;
                btnHuyPhieuThue.IsEnabled = false;
            }
        }

        private void BtnThemPhieuThue_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (cbbCD.SelectedIndex > -1)
                {
                    var diskid = int.TryParse(cbbCD.Text.Trim(), out int iddisk) ? iddisk : -1;
                    if (diskid > -1)
                    {
                        foreach (ChiTietPhieuThueVM item in DataGridPThue.Items)
                        {
                            if (diskid == item.maDia)
                            {
                                MessageBox.Show("Đĩa này đã có trong danh sách");
                                return;
                            }
                        }
                        var disk = _db.Dia.FirstOrDefault(x => x.maDia == diskid);
                        var rentalDetail = new ChiTietPhieuThueVM();
                        //rentalDetail.Quantity = double.Parse(tbSoLuong.Text);

                        rentalDetail.maDia = diskid;
                        rentalDetail.tenTuaDe = txtTuaDe.Text;
                        rentalDetail.loai = txtLoaiDia.Text;
                        rentalDetail.donGia = disk.TuaDe.donGia.ToString();
                        DataGridPThue.Items.Add(rentalDetail);
                        TotalPrice = 0;
                        foreach (ChiTietPhieuThueVM item in DataGridPThue.Items)
                        {
                            TotalPrice += double.Parse(item.donGia);
                        }
                        txtTotalPrice.Text = TotalPrice.ToString("#,###") + " đ";
                        btnSave.IsEnabled = true;
                        cbbCD.SelectedIndex = -1;
                    }
                }
                else
                {

                    MessageBox.Show("Bạn chưa chọn CD");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            if (DataGridPThue.Items.Count > 0)
            {
                btnXoaCd.IsEnabled = true;
            }
            else
            {
                btnXoaCd.IsEnabled = false;
            }
        }

        private void BtnHuyPhieuThue_Click(object sender, RoutedEventArgs e)
        {
            cbbCD.SelectedIndex = -1;
            cbbKH.SelectedIndex = -1;
            DataGridPThue.Items.Clear();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPThue.Items.Count < 1)
            {
                MessageBox.Show("Vui lòng thêm đĩa");
            }

            using (System.Data.Entity.DbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    double total = 0;
                    List<ChiTietPhieuThueVM> listRentalDetailVM = new List<ChiTietPhieuThueVM>();
                    foreach (ChiTietPhieuThueVM item in DataGridPThue.Items)
                    {
                        listRentalDetailVM.Add(item);
                    }

                    var rentalbill = new PhieuThue();

                    var listRentalDetail = new List<ChiTietPhieuThue>();

                    foreach (ChiTietPhieuThueVM item in listRentalDetailVM)
                    {
                        var rentaldetail = new ChiTietPhieuThue();
                        var disk = _db.Dia.FirstOrDefault(x => x.maDia == item.maDia);
                        //---------------
                        disk.trangThai = 1;

                        var title = disk.TuaDe;
                        title.soLuong -= 1;

                        rentaldetail.maDia = item.maDia;
                        rentaldetail.Dia = _db.Dia.FirstOrDefault(x => x.maDia == item.maDia);
                        rentaldetail.PhieuThue = rentalbill;
                        rentaldetail.maPhieuThue = rentalbill.maPhieuThue;
                        rentaldetail.trangThaiTra = false;
                        rentaldetail.hanTra = DateTime.Now.AddDays(Convert.ToDouble(_db.TuaDe.FirstOrDefault(x => x.Dia.Any(s => s.maDia == item.maDia)).ngayChoThue));
                        total += Convert.ToDouble(item.donGia);
                        listRentalDetail.Add(rentaldetail);
                    }

                    if (listRentalDetail.Count > 0)
                    {
                        var customer = (KhachHang)cbbKH.SelectedValue;

                        rentalbill.ngayThue = DateTime.Now;
                        rentalbill.tienCoc = total;
                        rentalbill.maKhachHang = customer.maKhachHang;
                        rentalbill.KhachHang = customer;
                        rentalbill.trangThaiTraPhiTre = false;


                        _db.ChiTietPhieuThue.AddRange(listRentalDetail);

                        _db.PhieuThue.Add(rentalbill);

                        _db.SaveChanges();
                        dbTran.Commit();

                        cbbCD.Text = "";
                        cbbCD.IsEnabled = true;
                        grbCD.IsEnabled = true;
                        btnHuyPhieuThue.IsEnabled = false;
                        btnLapPhieuThue.IsEnabled = true;
                        btnThemPhieuThue.IsEnabled = false;
                        btnXoaCd.IsEnabled = false;
                        grbKH.IsEnabled = true;
                        btnSave.IsEnabled = false;
                        cbbKH.IsEnabled = true;
                        txtTotalPrice.Text = "0 đ";
                        InitialCD();
                        cbbCD.ItemsSource = ListCD;
                        DataGridPThue.Items.Clear();
                        MessageBox.Show("Thuê thành công", "Thông Báo");
                    }
                    else
                    {
                        dbTran.Rollback();
                        cbbCD.Text = "";
                        cbbCD.IsEnabled = true;
                        grbCD.IsEnabled = true;
                        btnHuyPhieuThue.IsEnabled = false;
                        btnLapPhieuThue.IsEnabled = true;
                        btnThemPhieuThue.IsEnabled = false;
                        btnXoaCd.IsEnabled = false;
                        grbKH.IsEnabled = true;
                        btnSave.IsEnabled = false;
                        cbbKH.IsEnabled = true;
                        txtTotalPrice.Text = "0 đ";
                        InitialCD();
                        cbbCD.ItemsSource = ListCD;
                        DataGridPThue.Items.Clear();
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    cbbCD.Text = "";
                    cbbCD.IsEnabled = true;
                    grbCD.IsEnabled = false;
                    btnHuyPhieuThue.IsEnabled = false;
                    btnLapPhieuThue.IsEnabled = true;
                    btnThemPhieuThue.IsEnabled = false;
                    btnXoaCd.IsEnabled = false;
                    grbKH.IsEnabled = true;
                    btnSave.IsEnabled = false;
                    cbbKH.IsEnabled = true;
                    txtTotalPrice.Text = "0 đ";
                    InitialCD();
                    cbbCD.ItemsSource = ListCD;
                    DataGridPThue.Items.Clear();
                    MessageBox.Show("Thuê thất bại" + ex.Message, "Thông Báo");
                }
            }
        }

        private void BtnXoaCd_Click(object sender, RoutedEventArgs e)
        {


            var selectedItems = DataGridPThue.SelectedItems;
            if (selectedItems != null)
            {
                for (int i = DataGridPThue.SelectedItems.Count - 1; i >= 0; i--)
                {
                    DataGridPThue.Items.Remove(DataGridPThue.SelectedItems[i]);
                };
            }
            TotalPrice = 0;
            foreach (ChiTietPhieuThueVM item in DataGridPThue.Items)
            {
                TotalPrice += double.Parse(item.donGia);
            }
            txtTotalPrice.Text = TotalPrice.ToString("#,###") + " đ";
            if (TotalPrice == 0)
            {
                txtTotalPrice.Text = "0 đ";
                btnSave.IsEnabled = false;
            }
            else
            {
                btnSave.IsEnabled = true;
            }
        }

        private void DataGridPThue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (DataGridPThue.SelectedItems.Count > 0 || DataGridPThue.Items.Count > 0)
            {
                btnXoaCd.IsEnabled = true;
            }
            else
            {
                btnXoaCd.IsEnabled = false;
            }
        }


        private void BtnThanhToanPhiTre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tenKHCbb = cbbKH.Text;
                var cus = _db.KhachHang.FirstOrDefault(x => x.hoTen == tenKHCbb && x.trangThaiXoa == false);
                var cusPhieuThue = _db.PhieuThue.Where(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false).ToList();


                List<ChiTietPhieuThue> listCTPhieuThue = new List<ChiTietPhieuThue>();
                foreach (PhieuThue DSPhieuThue in cusPhieuThue)
                {
                    listCTPhieuThue = _db.ChiTietPhieuThue.Where(x => x.trangThaiTra == true && x.maPhieuThue == DSPhieuThue.maPhieuThue).ToList();

                    foreach (ChiTietPhieuThue item in listCTPhieuThue)
                    {
                        var returnBill = _db.PhieuTra.FirstOrDefault(x => x.maCTPhieuThue == item.maCTPhieuThue);
                        var editPhiTre = _db.PhiTre.FirstOrDefault(x => x.maPhieuTra == returnBill.maPhieuTra && x.tinhTrangThanhToan == false);
                        editPhiTre.tinhTrangThanhToan = true;
                        DSPhieuThue.trangThaiTraPhiTre = true;
                    }
                }

                //var editphieuThue = _db.PhieuThue.FirstOrDefault(x => x.maKhachHang == cus.maKhachHang && x.trangThaiTraPhiTre == false);
                //editphieuThue.trangThaiTraPhiTre = true;

                _db.SaveChanges();
                grbCD.IsEnabled = true;
                MessageBox.Show("Thanh toán thành công!");
                DgvPhieuTre.Items.Clear();
                btnThanhToanPhiTre.IsEnabled = false;
            }
            catch (Exception ex)
            {
                grbCD.IsEnabled = false;
                DgvPhieuTre.Items.Clear();

            }

        }

        private void DgvPhieuTre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
