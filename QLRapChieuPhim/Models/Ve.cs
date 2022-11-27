using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class Ve
    {
        public class VeBase
        {
            public int Id { get; set; }
            public DateTime NgayBanVe { get; set; }
            public string SoGhe { get; set; }
            public int GiaVe { get; set; }
            public int LichChieuId { get; set; }
            public int NhanVienId { get; set; }
            public int TinhTrang { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinVe : VeBase
            {
                public LichChieu.LichChieuBase LichChieu { get; set; }
                public NhanVien.NhanVienBase NhanVien { get; set; }

                public ThongTinVe()
                {
                    LichChieu = new LichChieu.LichChieuBase();
                    NhanVien = new NhanVien.NhanVienBase();
                }
            }
        }
    }
}
