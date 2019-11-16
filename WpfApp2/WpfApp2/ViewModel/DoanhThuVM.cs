using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.ViewModel
{
    public class DoanhThuVM
    {
        public int idDia { get; set; }
        public string tenTuaDeDT { get; set; }
        public string loaiTuaDeDT { get; set; }
        public float giaThue { get; set; }
        public int soLuongThue { get; set; }

        public float phiTre { get; set; }
        public float thanhTien { get; set; }
    }
}
