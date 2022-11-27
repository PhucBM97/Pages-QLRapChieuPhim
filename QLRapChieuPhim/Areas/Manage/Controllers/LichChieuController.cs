using Grpc.Net.Client;
using gRPCRapChieuPhim;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhim.Models;
using QLRapChieuPhim.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class LichChieuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DanhSachSuatChieu()
        {
            var channel = GrpcChannel.ForAddress(Utilities.ServiceURL);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);
            var response = client.DanhSachSuatChieu(new Input.Types.Empty());
            var SuatChieus = response.Items.ToList();
            var dsSuatChieu = SuatChieus.Select(x => new SuatChieuModel.Output.ThongTinSuatChieu { 
                Id = x.Id,
                TenSuatChieu = x.TenSuatChieu,
                GioBatDau = x.GioBatDau,
                GioKetThuc = x.GioKetThuc
            }).ToList();
            return View(dsSuatChieu);
        }

        public IActionResult ThemSuatChieu()
        {
            SuatChieuModel.Output.ThemSuatChieu model = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult ThemSuatChieu(IFormCollection form)
        {
            SuatChieuModel.Output.ThemSuatChieu model = new SuatChieuModel.Output.ThemSuatChieu();
            var channel = GrpcChannel.ForAddress(Utilities.ServiceURL);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);
            try
            {
                if (string.IsNullOrEmpty(form["TenSuatChieu"]))
                    model.ThongBao = "<p>- Tên suất chiếu phải khác rỗng</p>";
                if (string.IsNullOrEmpty(form["GioBatDau"]))
                    model.ThongBao += "<p>- Giờ bắt đầu phải khác rỗng</p>";
                else 
                {
                    var batdau = form["GioBatDau"].ToString().Split(':');
                    if (batdau.Length != 2 ||  !batdau[0].All(char.IsDigit) || !batdau[1].All(char.IsDigit))
                        model.ThongBao += "<p>- Giờ bắt đầu không hợp lệ</p>";
                    else if(int.Parse(batdau[0]) < 8 || int.Parse(batdau[0]) > 24 || int.Parse(batdau[1]) < 0 || int.Parse(batdau[1]) > 59)
                        model.ThongBao += "<p>- Giờ bắt đầu không hợp lệ</p>";
                }
                if (string.IsNullOrEmpty(model.ThongBao))
                {
                    var suatChieuMoi = new Input.Types.SuatChieu
                    {
                        //Gán thông tin phim cho phimThemMoi
                        //...  
                        TenSuatChieu = form["TenSuatChieu"].ToString(),
                        GioBatDau = form["GioBatDau"].ToString(),
                        GioKetThuc = form["GioKetThuc"].ToString()                        
                    };
                    var tb = client.ThemSuatChieuMoi(suatChieuMoi);
                    model.ThongBao = tb.NoiDung;
                }
            }
            catch (Exception ex)
            {
                model.ThongBao = "Có lỗi xảy ra: " + ex.Message;
            }
            return View(model);
        }

        public IActionResult CapNhatSuatChieu(int id)
        {
            if (id > 0)
            {
                //Kết nối đến dịch vụ gRPC để truy xuất dữ liệu
                var channel = GrpcChannel.ForAddress(Utilities.ServiceURL);
                var client = new RapChieuPhim.RapChieuPhimClient(channel);
                //Gọi phương thức XemThongTinPhim từ gRPC
                Output.Types.SuatChieu suatChieu = client.XemThongSuatChieu(new Input.Types.SuatChieu { Id = id });

                if (suatChieu != null && suatChieu.Id > 0)
                {
                    SuatChieuModel.Output.CapNhatSuatChieu thongTinSuatChieu = new SuatChieuModel.Output.CapNhatSuatChieu
                    {
                        Id = suatChieu.Id,
                        TenSuatChieu = suatChieu.TenSuatChieu,
                        GioBatDau = suatChieu.GioBatDau,
                        GioKetThuc = suatChieu.GioKetThuc
                    };
                    return View(thongTinSuatChieu);
                }
            }
            RedirectToAction("DanhSachSuatChieu");
            return View();
        }
        [HttpPost]
        public IActionResult CapNhatSuatChieu(IFormCollection form)
        {
            SuatChieuModel.Output.CapNhatSuatChieu model = new();
            var channel = GrpcChannel.ForAddress(Utilities.ServiceURL);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);

            try
            {
                if (string.IsNullOrEmpty(form["TenSuatChieu"]))
                    model.ThongBao = "<p>- Tên suất chiếu phải khác rỗng</p>";
                if (string.IsNullOrEmpty(form["GioBatDau"]))
                    model.ThongBao += "<p>- Giờ bắt đầu phải khác rỗng</p>";
                else
                {
                    var batdau = form["GioBatDau"].ToString().Split(':');
                    if (batdau.Length != 2 || !batdau[0].All(char.IsDigit) || !batdau[1].All(char.IsDigit))
                        model.ThongBao += "<p>- Giờ bắt đầu không hợp lệ</p>";
                    else if (int.Parse(batdau[0]) < 8 || int.Parse(batdau[0]) > 24 || int.Parse(batdau[1]) < 0 || int.Parse(batdau[1]) > 59)
                        model.ThongBao += "<p>- Giờ bắt đầu không hợp lệ</p>";
                }
                if (string.IsNullOrEmpty(model.ThongBao))
                {
                    var suatChieuCapNhat = new Input.Types.SuatChieu
                    {
                        //Gán thông tin phim cho phimThemMoi
                        //...  
                        Id = int.Parse(form["Id"].ToString()),
                        TenSuatChieu = form["TenSuatChieu"].ToString(),
                        GioBatDau = form["GioBatDau"].ToString(),
                        GioKetThuc = form["GioKetThuc"].ToString()
                    };
                    var tb = client.CapNhatSuatChieu(suatChieuCapNhat);
                    model.ThongBao = tb.NoiDung;
                }
            }
            catch (Exception ex)
            {
                model.ThongBao = "Có lỗi xảy ra: " + ex.Message;
            }

            //Gán thông tin phim cho phim cập nhật trả về view
            //...  
            model.Id = int.Parse(form["Id"].ToString());
            model.TenSuatChieu = form["TenSuatChieu"].ToString();
            model.GioBatDau = form["GioBatDau"].ToString();
            model.GioKetThuc = form["GioKetThuc"].ToString();
            return View(model);
        }
    }
}
