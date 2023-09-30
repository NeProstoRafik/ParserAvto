﻿using AngleSharp.Browser;
using Microsoft.AspNetCore.Mvc;
using ParserAvto.Core;
using ParserAvto.Core.AvtoParser;
using ParserAvto.Models;
using System.Diagnostics;

namespace ParserAvto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly Core.IParser parser;
        private readonly IParserSettings settings;    
        private readonly AvtoParser avtoParser;
        private readonly PageLoader pageLoader;
       
        public HomeController(ILogger<HomeController> logger, IParserSettings settings, AvtoParser avtoParser, PageLoader pageLoader)
        {
            _logger = logger;
        
            this.settings = settings;
            this.avtoParser = avtoParser;
            this.pageLoader = pageLoader;
           
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
        public async Task<IActionResult> GetPage()
        {
          var  document=await avtoParser.HtmlLoad( settings);
            var ListAvto = avtoParser.Parse(document);
            return View(ListAvto);
        }
        public async Task<IActionResult> NextPage()
        {
            var document = await pageLoader.GetFromPageId();
            var ListAvto = avtoParser.Parse(document);
            return View(ListAvto);
        }
        public async Task<IActionResult> PreviousPage()
        {
            var document = await pageLoader.GetFromPreviousPageId();
            var ListAvto = avtoParser.Parse(document);
            return View(ListAvto);
        }
    }
}