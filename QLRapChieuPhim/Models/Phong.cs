using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class Phong
    {
        public class PhongBase
        {
            public int Id { get; set; }
            public string TenPhong { get; set; }
            public int SoLuongGhe { get; set; }
            public string GhiChu { get; set; }
            public int RapId { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinPhong:PhongBase
            {
                public Rap.RapBase Rap { get; set; }
                public List<Ghe.GheBase> DanhSachGhe { get; set; }
                public List<LichChieu.LichChieuBase> DanhSachLichChieu { get; set; }

                public ThongTinPhong()
                {
                    Rap = new Rap.RapBase();
                    DanhSachGhe = new List<Ghe.GheBase>();
                    DanhSachLichChieu = new List<LichChieu.LichChieuBase>();
                }
            }
        }
    }
}
