using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class Ghe
    {
        public class GheBase
        {
            public int Id { get; set; }
            public string SoGhe { get; set; }
            public int LoaiGheId { get; set; }
            public int PhongId { get; set; }
            public string Day { get; set; }
            public int Hang { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinGhe:GheBase
            {
                public LoaiGhe.LoaiGheBase LoaiGhe { get; set; }
                public Phong.PhongBase Phong { get; set; }

                public ThongTinGhe()
                {
                    LoaiGhe = new LoaiGhe.LoaiGheBase();
                    Phong = new Phong.PhongBase();
                }
            }
        }
    }
}
