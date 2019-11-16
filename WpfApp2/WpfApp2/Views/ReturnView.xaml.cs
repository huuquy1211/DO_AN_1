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
    /// Interaction logic for ReturnView.xaml
    /// </summary>
    public partial class ReturnView : UserControl
    {
        QLDiaEntities _db = new QLDiaEntities();

        public List<KhachHang> ListCus { get; set; }
        public List<DiaVM> ListCD { get; set; }

        public List<PhieuTraVM> ListReturn { get; set; }

        public List<DiaTraVM> ListCDHasRent { get; set; }

        public ReturnView()
        {
            InitialCDHasRent();
            InitialCustomer();
            InitialCD();
            InitializeComponent();
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

        void InitialCDHasRent()
        {
            ListCDHasRent = _db.Dia.Where(x => x.trangThai == 1)
                .Select(x => new DiaTraVM()
                {
                    maDia = x.maDia,
                    KhachHang = x.ChiTietPhieuThue.FirstOrDefault(s => s.maDia == x.maDia && s.trangThaiTra == false).PhieuThue.KhachHang,
                    donGia = (double)x.TuaDe.donGia,
                    tuaDe = x.TuaDe.tenTuaDe,
                    loai = x.loai,
                    ngayThue = (DateTime)x.ChiTietPhieuThue.FirstOrDefault(s => s.maDia == x.maDia).PhieuThue.ngayThue,
                    trangThai = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê"
                }).ToList();
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = DataGridPhieuTra.SelectedItems;
            if (selectedItems != null)
            {
                for (int i = DataGridPhieuTra.SelectedItems.Count - 1; i >= 0; i--)
                {
                    DataGridPhieuTra.Items.Remove(DataGridPhieuTra.SelectedItems[i]);
                };
            }
        }

        private void BtnThemCTPhieuTra_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbbReturn.SelectedIndex > -1)
                {

                    var diskid = Convert.ToInt32(cbbReturn.Text.Trim());
                    PhieuTraVM returnDiskVM = new PhieuTraVM();
                    returnDiskVM.maDia = diskid;
                    returnDiskVM.tenTuaDe = tbTuaDe.Text.Trim();
                    returnDiskVM.loai = tbLoaiDiaTra.Text.Trim();
                    returnDiskVM.ngayThue = (DateTime)_db.PhieuThue.FirstOrDefault(x => x.ChiTietPhieuThue.Any(s => s.maDia == diskid && s.Dia.trangThai == 1 && s.trangThaiTra == false)).ngayThue;
                    returnDiskVM.KhachHang = _db.PhieuThue.FirstOrDefault(x => x.ChiTietPhieuThue.Any(s => s.maDia == diskid && s.Dia.trangThai == 1 && s.trangThaiTra == false)).KhachHang;
                    returnDiskVM.isLate = _db.ChiTietPhieuThue.Any(x => x.maDia == diskid && x.Dia.trangThai == 1 && x.trangThaiTra == false && x.hanTra < DateTime.Now);
                    //returnDiskVM.soNgayThue = ((DateTime)_db.ChiTietPhieuThue.FirstOrDefault(y => y.maDia == diskid).hanTra).DayOfYear - ((DateTime)_db.ChiTietPhieuThue.FirstOrDefault(s => s.maDia == diskid).PhieuThue.ngayThue).DayOfYear;
                    int soNgayThue = (DateTime.Now).DayOfYear - (returnDiskVM.ngayThue).DayOfYear;
                    if (soNgayThue == 0)
                        returnDiskVM.soNgayThue = 1;
                    else returnDiskVM.soNgayThue = soNgayThue;
                    foreach (PhieuTraVM item in DataGridPhieuTra.Items)
                    {
                        if (item.maDia == returnDiskVM.maDia)
                        {
                            MessageBox.Show("Đã thêm đĩa này");
                            return;
                        }
                    }
                    DataGridPhieuTra.Items.Add(returnDiskVM);
                }
                else
                {
                    MessageBox.Show("Chọn đĩa để trả");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnLapPhieuTra_Click(object sender, RoutedEventArgs e)
        {
            using (System.Data.Entity.DbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (int.TryParse(cbbReturn.Text.Trim(), out int idcd))
                    {

                        foreach (PhieuTraVM item in DataGridPhieuTra.Items)
                        {
                            var diskedit = _db.Dia.FirstOrDefault(x => x.maDia == item.maDia);
                            diskedit.trangThai = 0;
                            var editTrangThaiTra = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.trangThaiTra == false);
                            editTrangThaiTra.trangThaiTra = true;
                            _db.Entry(diskedit).State = System.Data.Entity.EntityState.Modified;

                            var returnBill = new PhieuTra();
                            returnBill.ngayTra = DateTime.Now;
                            returnBill.maCTPhieuThue = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.Dia.trangThai == 1 && x.trangThaiTra == false).maCTPhieuThue;
                            returnBill.ChiTietPhieuThue = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.Dia.trangThai == 1 && x.trangThaiTra == false);
                            _db.PhieuTra.Add(returnBill);
                            //_db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.PhieuThue.maKhachHang == item.KhachHang.maKhachHang && x.Dia.trangThai == true && x.trangThaiTra == false).hanTra
                            var date = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.Dia.trangThai == 1 && x.trangThaiTra == false).hanTra;

                            if (returnBill.ngayTra > date)
                            {
                                //var editPhieuThue = thongTinCTPhieuThue.PhieuThue.trangThaiTraPhiTre = false;
                                var dayslate = ((DateTime)returnBill.ngayTra).DayOfYear - ((DateTime)_db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.Dia.trangThai == 1 && x.trangThaiTra == false).hanTra).DayOfYear;
                                var overdue = new PhiTre();
                                overdue.PhieuTra = returnBill;
                                overdue.maPhieuTra = returnBill.maPhieuTra;
                                overdue.tongTien = dayslate * 2000;
                                overdue.tinhTrangThanhToan = false;

                                _db.PhiTre.Add(overdue);
                            }
                            else
                            {
                                var thongTinCTPhieuThue = _db.ChiTietPhieuThue.FirstOrDefault(x => x.maDia == item.maDia && x.Dia.trangThai == 1 && x.trangThaiTra == false);
                                thongTinCTPhieuThue.PhieuThue.trangThaiTraPhiTre = true;
                            }

                        }

                        _db.SaveChanges();
                        dbTran.Commit();
                        MessageBox.Show("Trả thành công", "Thông báo");
                        cbbReturn.IsEnabled = true;
                        InitialCDHasRent();
                        cbbReturn.ItemsSource = ListCDHasRent;
                        cbbReturn.Text = "";
                        ListReturn = new List<PhieuTraVM>();
                        DataGridPhieuTra.Items.Clear();
                        cbbReturn.Focus();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(FormatException))
                    {
                        cbbReturn.Text = "";
                        cbbReturn.Focus();
                        return;
                    }
                    dbTran.Rollback();
                    cbbReturn.IsEnabled = true;
                    InitialCDHasRent();
                    cbbReturn.ItemsSource = ListCDHasRent;
                    cbbReturn.Text = "";
                    cbbReturn.Focus();
                    DataGridPhieuTra.Items.Clear();
                    MessageBox.Show("Trả thất bại", "Thông báo");
                }
            }
        }

        private void DataGridPhieuTra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPhieuTra.SelectedItems.Count > 0)
            {
                btnXoa.IsEnabled = true;
            }
            else
            {
                btnXoa.IsEnabled = false;
            }
        }
    }
}
