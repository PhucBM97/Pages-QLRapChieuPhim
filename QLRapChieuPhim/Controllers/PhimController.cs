using Grpc.Net.Client;
using gRPCRapChieuPhim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Controllers
{
    public class PhimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PhimDangChieu()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var client = new RapChieuPhim.RapChieuPhimClient(channel);

            Output.Types.Phims phimDangChieus = client.DanhSachPhimDangChieu(new Input.Types.Empty());
            var DSPhim = phimDangChieus.Items.ToList();
            var theLoais = client.DanhSachTheLoai(new Input.Types.Empty());
            var xepHangPhims = client.DanhSachXepHangPhim(new Input.Types.Empty());
            var dsTheLoai = theLoais.Items.ToList();
            List<PhimModel.Output.ThongTinPhim> model = new List<PhimModel.Output.ThongTinPhim>();
            foreach (var phim in DSPhim) {
                var thongTin = new PhimModel.Output.ThongTinPhim {
                    Id = phim.Id,
                    TenPhim = phim.TenPhim,
                    TenGoc = phim.TenGoc,
                    DienVien = phim.DienVien,
                    DaoDien = phim.DaoDien,
                    NoiDung = phim.NoiDung,
                    NgayKhoiChieu = phim.NgayKhoiChieu.ToDateTime(),
                    NgonNgu = phim.NgonNgu,
                    NhaSanXuat = phim.NhaSanXuat,
                    NuocSanXuat = phim.NuocSanXuat,
                    Poster = phim.Poster,
                    ThoiLuong = phim.ThoiLuong,
                    Trailer = phim.Trailer,
                    DanhSachTheLoaiId = phim.DanhSachTheLoaiId,
                    XepHangPhimId = phim.XepHangPhimId
                };

                var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim != null)
                    foreach (var tl in theloai_phim)
                    {
                        thongTin.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                        {
                            Id = tl.Id,
                            Ten = tl.Ten
                        });                        
                    }
                var xepHang = xepHangPhims.Items
                                .FirstOrDefault(xh => xh.Id.Equals(phim.XepHangPhimId));
                if (xepHang != null)
                    thongTin.XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                    {
                        Id = xepHang.Id,
                        Ten = xepHang.Ten,
                        KyHieu = xepHang.KyHieu
                    };
                model.Add(thongTin);
            }
            ViewData["loaiPhim"] = "PhimDangChieu";
            return View("DanhSachPhim", model);
        }

        public IActionResult PhimSapChieu()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var client = new RapChieuPhim.RapChieuPhimClient(channel);
            Output.Types.Phims phimSapChieus = 
                client.DanhSachPhimSapChieu(new Input.Types.Empty());
            var DSPhim = phimSapChieus.Items.ToList();
            var theLoais = client.DanhSachTheLoai(new Input.Types.Empty());
            var xepHangPhims = client.DanhSachXepHangPhim(new Input.Types.Empty());
            var dsTheLoai = theLoais.Items.ToList();
            List<PhimModel.Output.ThongTinPhim> model = new List<PhimModel.Output.ThongTinPhim>();
            foreach (var phim in DSPhim)
            {
                var thongTin = new PhimModel.Output.ThongTinPhim
                {
                    Id = phim.Id,
                    TenPhim = phim.TenPhim,
                    TenGoc = phim.TenGoc,
                    DienVien = phim.DienVien,
                    DaoDien = phim.DaoDien,
                    NoiDung = phim.NoiDung,
                    NgayKhoiChieu = phim.NgayKhoiChieu.ToDateTime(),
                    NgonNgu = phim.NgonNgu,
                    NhaSanXuat = phim.NhaSanXuat,
                    NuocSanXuat = phim.NuocSanXuat,
                    Poster = phim.Poster,
                    ThoiLuong = phim.ThoiLuong,
                    Trailer = phim.Trailer,
                    DanhSachTheLoaiId = phim.DanhSachTheLoaiId,
                    XepHangPhimId = phim.XepHangPhimId
                };

                var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                            StringSplitOptions.RemoveEmptyEntries).ToList();
                var theloai_phim = dsTheLoai
                                .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                if (theloai_phim != null)
                    foreach (var tl in theloai_phim)
                    {
                        thongTin.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                        {
                            Id = tl.Id,
                            Ten = tl.Ten
                        });
                    }
                var xepHang = xepHangPhims.Items
                                .FirstOrDefault(xh => xh.Id.Equals(phim.XepHangPhimId));
                if (xepHang != null)
                    thongTin.XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                    {
                        Id = xepHang.Id,
                        Ten = xepHang.Ten,
                        KyHieu = xepHang.KyHieu
                    };
                model.Add(thongTin);
            }
            ViewData["loaiPhim"] = "PhimSapChieu";
            return View("DanhSachPhim", model);
        }

        public IActionResult ThongTinPhim(int id)
        {
            if (id > 0)
            {
                //Kết nối đến dịch vụ gRPC để truy xuất dữ liệu
                var channel = GrpcChannel.ForAddress("https://localhost:5001/");
                var client = new RapChieuPhim.RapChieuPhimClient(channel);
                //Gọi phương thức XemThongTinPhim từ gRPC
                Output.Types.Phim phim =
                    client.XemThongTinPhim(new Input.Types.Phim { Id = id });
                //Gọi phương thức DanhSachTheLoai từ gRPC
                var theLoais = client.DanhSachTheLoai(new Input.Types.Empty());
                var dsTheLoai = theLoais.Items.ToList();
                //Gọi phương thức DanhSachXepHangPhim từ gRPC
                var xepHangPhims = client.DanhSachXepHangPhim(new Input.Types.Empty());

                if (phim != null && phim.Id > 0)
                {
                    PhimModel.Output.ThongTinPhim thongTinPhim = new PhimModel.Output.ThongTinPhim
                    {
                        Id = phim.Id,
                        TenPhim = phim.TenPhim,
                        TenGoc = phim.TenGoc,
                        DienVien = phim.DienVien,
                        DaoDien = phim.DaoDien,
                        NoiDung = phim.NoiDung,
                        NgayKhoiChieu = phim.NgayKhoiChieu.ToDateTime(),
                        NgonNgu = phim.NgonNgu,
                        NhaSanXuat = phim.NhaSanXuat,
                        NuocSanXuat = phim.NuocSanXuat,
                        Poster = phim.Poster,
                        ThoiLuong = phim.ThoiLuong,
                        Trailer = phim.Trailer,
                        DanhSachTheLoaiId = phim.DanhSachTheLoaiId,
                        XepHangPhimId = phim.XepHangPhimId
                    };
                    var dsIdTheLoai = phim.DanhSachTheLoaiId.Split(new string[] { "," },
                                           StringSplitOptions.RemoveEmptyEntries).ToList();
                    var theloai_phim = dsTheLoai
                                 .Where(x => dsIdTheLoai.Contains(x.Id.ToString())).ToList();
                    if (theloai_phim != null)
                        foreach (var tl in theloai_phim)
                        {
                            thongTinPhim.DanhSachTheLoai.Add(new TheLoaiPhimModel.TheLoaiPhimBase
                            {
                                Id = tl.Id,
                                Ten = tl.Ten
                            });
                        }
                    var xepHang = xepHangPhims.Items
                                    .FirstOrDefault(xh => xh.Id.Equals(phim.XepHangPhimId));
                    if (xepHang != null)
                        thongTinPhim.XepHangPhim = new XepHangPhimModel.XepHangPhimBase
                        {
                            Id = xepHang.Id,
                            Ten = xepHang.Ten,
                            KyHieu = xepHang.KyHieu
                        };
                    
                    return View(thongTinPhim);
                }
            }
            RedirectToAction("DanhSachPhim");
            return View();
        }
    }
}
