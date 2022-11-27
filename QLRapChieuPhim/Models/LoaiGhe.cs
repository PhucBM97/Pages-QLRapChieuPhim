using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class LoaiGhe
    {
        public class LoaiGheBase
        {
            public int Id { get; set; }
            public string TenLoaiGhe { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinLoaiGhe:LoaiGheBase
            {
                public List<Ghe.GheBase> DanhSachGhe { get; set; }

                public ThongTinLoaiGhe()
                {
                    DanhSachGhe = new List<Ghe.GheBase>();
                }
            }
        }
    }
}
