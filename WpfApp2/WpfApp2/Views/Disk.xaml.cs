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
using WpfApp2.ViewModel;

namespace WpfApp2.Views
{
    /// <summary>
    /// Interaction logic for Disk.xaml
    /// </summary>
    public partial class Disk : UserControl
    {
        QLDiaEntities _db = new QLDiaEntities();
        RegExr re = new RegExr();
        public List<TuaDe> Title { get; set; }
        public List<DiskVM> CDs { get; set; }
        public List<Dia> ListDisk { get; set; }
        public Disk()
        {
            Title = _db.TuaDe.GroupBy(x => x.tenTuaDe).Select(s => s.FirstOrDefault()).Where(d => d.trangThai == true).ToList();
            CDs = _db.Dia.Where(x => x.trangThai != 3 && x.TuaDe.trangThai == true).Select(x => new DiskVM()
            {
                Id = x.maDia,

                State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                TitleName = x.TuaDe.tenTuaDe,
                TypeCD = x.loai,
                TypeTitle = x.TuaDe.moTa
            }).ToList();

            InitializeComponent();
        }
        void Clear()
        {
            cbxLoaiDia.SelectedIndex = -1;
            txtLoaiTua.Text = "";
            cbxTenTua.SelectedIndex = -1;
            grvTitle.Items.Clear();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_db.TuaDe.Any(x => x.tenTuaDe == cbxTenTua.Text && x.moTa == txtLoaiTua.Text
                            && x.trangThai == true) && cbxLoaiDia.SelectedIndex > -1)

                {
                    var countitle = new List<DiskAddVM>();
                   
                        foreach (DiskAddVM item in grvTitle.Items)
                        {
                            countitle.Add(item);
                        }
                    var aa = _db.Dia.Where(x => x.TuaDe.tenTuaDe.ToUpper().Trim() == cbxTenTua.Text.ToUpper().Trim() && x.TuaDe.moTa == txtLoaiTua.Text
                    && x.TuaDe.trangThai == true && (x.trangThai == 0 || x.trangThai == 1)).ToList();
                    var bb = _db.Dia.Where(x => x.TuaDe.tenTuaDe.ToUpper().Trim() == cbxTenTua.Text.ToUpper().Trim() && x.TuaDe.moTa == txtLoaiTua.Text
                    && x.TuaDe.trangThai == true).Count();
                    var qq = _db.TuaDe.FirstOrDefault(x => x.tenTuaDe == cbxTenTua.Text && x.moTa == txtLoaiTua.Text && x.trangThai == true).soLuong;
                    var ee = countitle.Where(x => x.IdTitle == _db.TuaDe.FirstOrDefault(a => a.tenTuaDe.ToUpper().Trim() == cbxTenTua.Text.Trim().ToUpper() && a.moTa == txtLoaiTua.Text && a.trangThai == true).maTuaDe).Count();
                    if (qq - (ee + bb) > 0)
                    {

                        var disc = new DiskAddVM();
                        var title = cbxTenTua.SelectedItem as TuaDe;
                        disc.IdTitle = title.maTuaDe;
                        disc.Title = title.tenTuaDe;
                        disc.Type = cbxLoaiDia.SelectedIndex == 0 ? "DVD" : "CD";
                        disc.TypeTitle = txtLoaiTua.Text;
                        grvTitle.Items.Add(disc);
                    }
                    else
                    {
                        MessageBox.Show("Tựa đề đã đủ số lượng đĩa");
                    }
                }


                else
                {
                    MessageBox.Show("Nhập dữ liệu để thêm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grvTitle.Items.Count > 0)
                {
                    ListDisk = new List<Dia>();
                    foreach (DiskAddVM item in grvTitle.Items)
                    {
                        var disk = new Dia();
                        var title = _db.TuaDe.FirstOrDefault(x => x.tenTuaDe == item.Title && x.moTa == item.TypeTitle && x.trangThai == true);
                        disk.TuaDe = title;
                        disk.maTuaDe = title.maTuaDe;
                        disk.trangThai = 0;

                        disk.loai = item.Type;
                        ListDisk.Add(disk);
                    }
                    _db.Dia.AddRange(ListDisk);
                    _db.SaveChanges();
                    CDs = CDs = _db.Dia.Where(x => x.trangThai != 3 && x.TuaDe.trangThai == true).Select(x => new DiskVM()
                    {
                        Id = x.maDia,

                        State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                        TitleName = x.TuaDe.tenTuaDe,
                        TypeCD = x.loai,
                        TypeTitle = x.TuaDe.moTa
                    }).ToList();
                    grvDisc.ItemsSource = CDs;
                    Clear();
                }
                else
                {
                    MessageBox.Show("Chưa có đĩa để lưu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var disc1 = grvDisc.SelectedItem as DiskVM;
            var discdelete1 = _db.Dia.FirstOrDefault(x => x.maDia == disc1.Id);
            if (discdelete1.trangThai == 1)
            {
                MessageBox.Show("Đĩa đang được thuê không thể xóa");
            }
            else
            {
                try
                {
                    if (MessageBox.Show("Chắc chắn xóa ?", "Xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (grvDisc.SelectedItems.Count > 0)
                        {
                            var disc = grvDisc.SelectedItem as DiskVM;
                            var discdelete = _db.Dia.FirstOrDefault(x => x.maDia == disc.Id);
                            discdelete.trangThai = 3;

                            _db.Entry(discdelete).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                            CDs = _db.Dia.Where(x => x.trangThai != 3 && x.TuaDe.trangThai == true).Select(x => new DiskVM()
                            {
                                Id = x.maDia,

                                State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                                TitleName = x.TuaDe.tenTuaDe,
                                TypeCD = x.loai,
                                TypeTitle = x.TuaDe.moTa
                            }).ToList();
                            grvDisc.ItemsSource = CDs;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnDeleteAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (grvTitle.SelectedItems.Count > 0)
                {
                    grvTitle.Items.Remove(grvTitle.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            if (txtSearch.Text.ToLower().Trim() == "")
            {
                CDs = _db.Dia.Where(x => x.trangThai != 3 && x.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,

                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            else if (txtSearch.Text.Contains("CD"))
            {
                CDs = _db.Dia.Where(a => a.loai.ToLower().Contains(txtSearch.Text.ToLower().Trim()) && a.trangThai != 3 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            else if (txtSearch.Text.Contains("DVD"))
            {
                CDs = _db.Dia.Where(a => a.loai.ToLower().Contains(txtSearch.Text.ToLower().Trim()) && a.trangThai != 3 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            //Search theo trạng thái
            else if (txtSearch.Text.Contains("Trên Kệ"))
            {
                CDs = _db.Dia.Where(a => a.trangThai == 0 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            else if (txtSearch.Text.Contains("Đang Thuê"))
            {
                CDs = _db.Dia.Where(a => a.trangThai == 1 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            //search theo tựa
            else if (!re.MaKhachHang(txtSearch.Text.TrimEnd()))
            {

                CDs = _db.Dia.Where(a => a.TuaDe.tenTuaDe.ToLower().Contains(txtSearch.Text.ToLower().Trim()) && a.trangThai != 3 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }
            //search theo mã
            else if (re.MaKhachHang(txtSearch.Text.TrimEnd()))
            {
                int m = Int32.Parse(txtSearch.Text);
                CDs = _db.Dia.Where(a => a.maDia == m && a.trangThai != 3 && a.TuaDe.trangThai == true).Select(x => new DiskVM()
                {
                    Id = x.maDia,
                    State = x.trangThai == 0 ? "Trên Kệ" : "Đang Thuê",
                    TitleName = x.TuaDe.tenTuaDe,
                    TypeCD = x.loai,
                    TypeTitle = x.TuaDe.moTa
                }).ToList();
                grvDisc.ItemsSource = CDs;
            }


        }


    }
}
