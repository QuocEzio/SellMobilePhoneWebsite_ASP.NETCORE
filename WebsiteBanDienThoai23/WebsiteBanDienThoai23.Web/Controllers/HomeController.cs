using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebsiteBanDienThoai23.DAL.Models;
using WebsiteBanDienThoai23.Web.Models;

namespace WebsiteBanDienThoai23.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly QLBanDienThoaiContext da = new QLBanDienThoaiContext();
        public IActionResult Index()
        {
            List<SanPham> list = da.SanPhams.OrderBy(s => s.MaSp).ToList();
            var viewModel = new SanPhamModel
            {
                Items = list,
                TotalItems = list.Count
            };
            return View(viewModel);
        }

        public async Task<ActionResult> Address()
        {
            string apiKey = "0b151c45f779c1e67b220970472775d5";
            string city = "hanoi";
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            List<string> ThongTinThoiTiet = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);

                        string weatherDescription = weatherData.weather[0].description;
                        double temperature = weatherData.main.temp;
                        int humidity = weatherData.main.humidity;
                        double windSpeed = weatherData.wind.speed;
                        //Thêm vào list để hiển thị
                        ThongTinThoiTiet.Add("Thời tiết: " + weatherDescription);
                        ThongTinThoiTiet.Add("Nhiệt độ: " + temperature.ToString() + "°C");
                        ThongTinThoiTiet.Add("Độ ẩm: " + humidity.ToString() + "%");
                        ThongTinThoiTiet.Add("Tốc độ gió: " + windSpeed + "m/s");



                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                }
            }
            ViewBag.ThoiTiet = ThongTinThoiTiet;
            return View(ViewBag.ThoiTiet);
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
