using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Model;

namespace WpfApp2.ViewModel
{
    public class DiaTraVM
    {
        public int maDia { get; set; }
        public string tuaDe { get; set; }
        public string tenTuaDe { get; set; }
        public string trangThai { get; set; }
        public string loai { get; set; }
        public double donGia { get; set; }
        public DateTime? ngayThue { get; set; }
       
        public KhachHang KhachHang { get; set; }
    }
}
