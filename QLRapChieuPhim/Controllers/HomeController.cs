using Grpc.Net.Client;
using gRPCRapChieuPhim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QLRapChieuPhim.Common;
using QLRapChieuPhim.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var channel = GrpcChannel.ForAddress(Utilities.ServiceURL);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);

            var response = client.DanhSachSlideBanner(new Input.Types.Empty());
            var dsSlide = response.Items.ToList();
            HomeFrontendModel model = new();
            model.DanhSachBanner = dsSlide.Where(x => x.KichHoat)
                                            .Select(x => new SlideBannerModel.SlideBannerBase
                                            {
                                                Id = x.Id,
                                                Ten = x.Ten,
                                                Hinh = x.Hinh,
                                                LienKet = x.LienKet,
                                                KichHoat = x.KichHoat
                                            }).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
