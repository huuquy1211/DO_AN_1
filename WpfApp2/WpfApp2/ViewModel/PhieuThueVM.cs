using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModel
{
    public class PhieuThueVM
    {
        public int maPhieuThue { get; set; }
        public float tienCoc { get; set; }
        public DateTime ngayThue { get; set; }

        public List<ChiTietPhieuThueVM> ListRentalDetail { get; set; }
        public DateTime RentalDate { get; set; }
        public KhachHangVM KhachHang { get; set; }
    }
}
