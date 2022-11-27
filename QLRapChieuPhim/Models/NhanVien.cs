using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class NhanVien
    {
        public class NhanVienBase
        {
            public int Id { get; set; }
            public string HoTen { get; set; }
            public bool? GioiTinh { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string DiaChi { get; set; }
            public string Cmnd { get; set; }
            public string MatKhau { get; set; }
            public int RapId { get; set; }
        }
        public class Input
        {

        }
        public class Output
        {
            public class ThongTinNhanVien:NhanVienBase
            {
                public Rap.RapBase Rap { get; set; }
                public List<Ve.VeBase> DanhSachVe { get; set; }

                public ThongTinNhanVien()
                {
                    Rap = new Rap.RapBase();
                    DanhSachVe = new List<Ve.VeBase>();
                }
            }
        }
    }
}
