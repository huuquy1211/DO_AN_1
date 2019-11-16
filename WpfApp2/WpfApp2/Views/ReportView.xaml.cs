using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
using WpfApp2.ViewModel;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        QLDiaEntities _db = new QLDiaEntities();
        List<TuaDeVM> listTitle { get; set; }

        List<DiaQuaHanVM> listQH { get; set; }

        List<DoanhThuVM> listDT { get; set; }
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
        public ReportView()
        {
            InitializeComponent();


        }




        private void Btn_ReportTile_Click(object sender, RoutedEventArgs e)
        {
            dtp_DenNgay.SelectedDate = DateTime.Today;
            dtp_TuNgay.SelectedDate = new DateTime(2019, 9, 9);
            loadList((DateTime)dtp_TuNgay.SelectedDate, (DateTime)dtp_DenNgay.SelectedDate);
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
                    Application.Current.Resources["isReportTitle"] = Visibility.Visible;
                    Application.Current.Resources["isReportDisk"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDT"] = Visibility.Collapsed;


                    break;

            }
        }

        private void Btn_ReportDisk_Click(object sender, RoutedEventArgs e)
        {

            dtp_DenNgayQH.SelectedDate = DateTime.Today;
            dtp_TuNgayQH.SelectedDate = new DateTime(2019, 9, 9);
            loadListQH((DateTime)dtp_TuNgayQH.SelectedDate, (DateTime)dtp_DenNgayQH.SelectedDate);
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
                    Application.Current.Resources["isReportTitle"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDisk"] = Visibility.Visible;
                    Application.Current.Resources["isReportDT"] = Visibility.Collapsed;



                    break;

            }
        }

        private void Btn_DoanhThu_Click(object sender, RoutedEventArgs e)
        {
            dtp_DenNgayDT.SelectedDate = DateTime.Today;
            dtp_TuNgayDT.SelectedDate = new DateTime(2019, 9, 9);
            loadListDT((DateTime)dtp_TuNgayDT.SelectedDate, (DateTime)dtp_DenNgayDT.SelectedDate);

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
                    Application.Current.Resources["isReportTitle"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDisk"] = Visibility.Collapsed;
                    Application.Current.Resources["isReportDT"] = Visibility.Visible;

                    break;

            }
        }

        private void btnSearchYT_Click(object sender, RoutedEventArgs e)
        {
            loadList((DateTime)dtp_TuNgay.SelectedDate, (DateTime)dtp_DenNgay.SelectedDate);
        }

        public void loadList(DateTime t1, DateTime t2)
        {
            listTitle = _db.ChiTietPhieuThue.Where(a => a.PhieuThue.ngayThue >= t1).Select(x => new TuaDeVM()
            {
                maTuaDe = (int)x.Dia.maTuaDe,
                tenTuaDe = x.Dia.TuaDe.tenTuaDe,
                soLuongDaThue = _db.ChiTietPhieuThue.Count(s => s.Dia.maTuaDe == x.Dia.maTuaDe),
                donGia = (float)x.Dia.TuaDe.donGia,
                loaiTuaDe = x.Dia.loai,
            }).Distinct().ToList();
            dtgTitles.ItemsSource = listTitle;
        }

        private void btnSearchQH_Click(object sender, RoutedEventArgs e)
        {
            loadListQH((DateTime)dtp_TuNgayQH.SelectedDate, (DateTime)dtp_DenNgayQH.SelectedDate);
        }

        public void loadListQH(DateTime t1, DateTime t2)
        {
            listQH = _db.ChiTietPhieuThue.Where(a => a.PhieuThue.ngayThue >= t1 && a.PhieuThue.ngayThue <= t2 && a.trangThaiTra == false && t2.Day > a.hanTra.Value.Day).Select(x => new DiaQuaHanVM()
            {
                maTuaDeQH = (int)x.Dia.maTuaDe,
                tenTuaDeQH = x.Dia.TuaDe.tenTuaDe,
                tenKhachHangQH = x.PhieuThue.KhachHang.hoTen,
                soLuongThue = _db.Dia.Count(s => s.TuaDe.maTuaDe == x.Dia.maTuaDe),
                loaiTuaDeQH = x.Dia.loai,
                soDienThoai = x.PhieuThue.KhachHang.soDienThoai,
                soNgayTre = (int)DateTime.Now.Day - x.hanTra.Value.Day
            }).ToList();

            dtgdisk.ItemsSource = listQH;
        }

        private void btnSearchDT_Click(object sender, RoutedEventArgs e)
        {
            loadListDT((DateTime)dtp_TuNgayDT.SelectedDate, (DateTime)dtp_DenNgayDT.SelectedDate);
        }

        public void loadListDT(DateTime t1, DateTime t2)
        {


            listDT = _db.PhieuTra.Where(a => a.ngayTra >= t1 && a.ngayTra <= t2 && a.ChiTietPhieuThue.trangThaiTra == true).Select(x => new DoanhThuVM()
            {
                idDia = x.ChiTietPhieuThue.maDia,
                tenTuaDeDT = x.ChiTietPhieuThue.Dia.TuaDe.tenTuaDe,
                loaiTuaDeDT = x.ChiTietPhieuThue.Dia.loai,
                soLuongThue = _db.Dia.Count(s => s.TuaDe.maTuaDe == x.ChiTietPhieuThue.Dia.maTuaDe),
                giaThue = (float)x.ChiTietPhieuThue.Dia.TuaDe.donGia,
                phiTre = (float)x.PhiTre.FirstOrDefault(b => b.maPhieuTra == x.maPhieuTra).tongTien > 0 ? (float)x.PhiTre.FirstOrDefault(b => b.maPhieuTra == x.maPhieuTra).tongTien : 0,
                thanhTien = (((float)x.ChiTietPhieuThue.Dia.TuaDe.donGia) * _db.Dia.Count(s => s.TuaDe.maTuaDe == x.ChiTietPhieuThue.Dia.maTuaDe) + ((float)x.PhiTre.FirstOrDefault(b => b.maPhieuTra == x.maPhieuTra).tongTien > 0 ? (float)x.PhiTre.FirstOrDefault(b => b.maPhieuTra == x.maPhieuTra).tongTien : 0))
            }).Distinct().ToList();
            dtgdoanhthu.ItemsSource = listDT;
            float t = 0;
            foreach (DoanhThuVM item in dtgdoanhthu.Items)
            {
                t += item.thanhTien;
            }
            txt_Total.Text = t.ToString("#,###") + " VND";
        }

        private void btnPrintYT_Click(object sender, RoutedEventArgs e)
        {
            if (dtgTitles.Items.Count > 0)
            {

                this.dtgTitles.SelectAllCells();
                this.dtgTitles.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.dtgTitles);
                this.dtgTitles.UnselectAllCells();
                String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                try
                {
                    StreamWriter sw = new StreamWriter("Danh sách tựa đề yêu thích.csv", false, Encoding.Unicode);
                    sw.WriteLine(result.Replace(',',' '));
                    sw.Close();
                    Process.Start("Danh sách tựa đề yêu thích.csv");
                }
                catch (Exception ex)
                { }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất !", "Thông báo");
            }
        }

        private void btnPrintQH_Click(object sender, RoutedEventArgs e)
        {
            if (dtgdisk.Items.Count > 0)
            {

                this.dtgdisk.SelectAllCells();
                this.dtgdisk.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.dtgdisk);
                this.dtgdisk.UnselectAllCells();
                String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                try
                {
                    StreamWriter sw = new StreamWriter("Danh sách quá hạn.csv", false,Encoding.Unicode);
                    sw.WriteLine(result);
                    sw.Close();
                    Process.Start("Danh sách quá hạn.csv");
                }
                catch (Exception ex)
                { }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất !", "Thông báo");
            }
        }

        private void btnPrintDT_Click(object sender, RoutedEventArgs e)
        {
            if (dtgdoanhthu.Items.Count > 0)
            {

                this.dtgdoanhthu.SelectAllCells();
                this.dtgdoanhthu.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.dtgdoanhthu);
                this.dtgdoanhthu.UnselectAllCells();
                String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                try
                {
                    StreamWriter sw = new StreamWriter("Doanh thu.csv", false, Encoding.Unicode);
                    sw.WriteLine(result);
                    sw.Close();
                    Process.Start("Doanh thu.csv");
                }
                catch (Exception ex)
                { }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất !", "Thông báo");
            }
        }
    }
}
