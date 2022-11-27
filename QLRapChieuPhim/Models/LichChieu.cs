using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class LichChieu
    {
        public class LichChieuBase
        {
            public int Id { get; set; }
            public int SuatChieuId { get; set; }
            public int PhongId { get; set; }
            public int PhimId { get; set; }
            public DateTime NgayChieu { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinLichChieu : LichChieuBase
            {
                public Phim.PhimBase Phim { get; set; }
                public Phong.PhongBase Phong { get; set; }
                public SuatChieu.SuatChieuBase SuatChieu { get; set; }
                public List<Ve.VeBase> DanhSachVe { get; set; }

                public ThongTinLichChieu()
                {
                    Phim = new Phim.PhimBase();
                    Phong = new Phong.PhongBase();
                    SuatChieu = new SuatChieu.SuatChieuBase();
                    DanhSachVe = new List<Ve.VeBase>();
                }
            }
        }
    }
}
