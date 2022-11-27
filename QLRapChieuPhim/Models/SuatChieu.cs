using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class SuatChieu
    {
        public class SuatChieuBase
        {
            public int Id { get; set; }
            public string TenSuatChieu { get; set; }
            public string GioBatDau { get; set; }
            public string GioKetThuc { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinSuatChieu : SuatChieuBase
            {
                public List<LichChieu.LichChieuBase> DanhSachLichChieu { get; set; }

                public ThongTinSuatChieu()
                {
                    DanhSachLichChieu = new List<LichChieu.LichChieuBase>();
                }
            }
        }
    }
}
