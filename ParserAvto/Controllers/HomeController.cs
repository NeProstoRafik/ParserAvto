using Microsoft.AspNetCore.Mvc;
using ParserAvto.Core.AvtoParser;
using ParserAvto.Models;
using System.Diagnostics;

namespace ParserAvto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ListAvto listAvto;

        public HomeController(ILogger<HomeController> logger, ListAvto listAvto)
        {
            _logger = logger;


            this.listAvto = listAvto;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> GetPage(int page = 1)
        {
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                CountItems = listAvto.CountFromBd()
            };
            var list = listAvto.ReadFromBd(page, pageInfo.PageSize);

            var pageLoaderModel = new PaginationViewModel
            {
                PageInfo = pageInfo,
                Avtos = list
            };

            return View(pageLoaderModel);
        }

    }
}