using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Models
{
    public class Rap
    {
        public class RapBase
        {
            public int Id { get; set; }
            public string TenRap { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public string Email { get; set; }
            public int NguoiQuanLyId { get; set; }            
        }
        public class Intput
        {

        }
        public class Output
        {
            public class ThongTinRap:RapBase
            {
                public List<NhanVien.NhanVienBase> DanhSachNhanVien { get; set; }
                public List<Phong.PhongBase> DanhSachPhong { get; set; }

                public ThongTinRap()
                {
                    DanhSachNhanVien = new List<NhanVien.NhanVienBase>();
                    DanhSachPhong = new List<Phong.PhongBase>();
                }
            }
        }
    }
}
