using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace WpfApp2.ViewModel
{
    public class NhanVienVM
    {
        public int maNhanVien { get; set; }

        public int maTaiKhoan { get; set; }

        public string tenTaiKhoan { get; set; }

        public string hoTen { get; set; }

        public Nullable<System.DateTime> ngayKyHopDong { get; set; }

        public string diaChi { get; set; }

        public string soDienThoai { get; set; }

        public string loaiNhanVien { get; set; }

        public System.Windows.Controls.Button btn { get; set; }
        public PackIcon Kind { get; set; }
    }
}
