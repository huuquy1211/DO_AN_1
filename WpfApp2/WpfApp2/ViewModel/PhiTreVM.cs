using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Model;

namespace WpfApp2.ViewModel
{
    public class PhiTreVM
    {

        public int maPhiTre { get; set; }
        public int ngayTre { get; set; }
        public DateTime ngayTra { get; set; }
        public DateTime hanTra { get; set; }
        public double phiTre { get; set; }
        public string tenTuaDe { get; set; }
        public PhieuTra PhieuTra { get; set; }
    }
}
