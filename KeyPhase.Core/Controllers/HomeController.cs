﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KeyPhase.Core.Models;
using KeyPhase.Service.Interface;

namespace KeyPhase.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _testService;

        public HomeController(IProjectService testService)
        {            
            _testService = testService;
        }

        public IActionResult Index()
        {
            var test2 = _testService.GetAllForUser(1);
            var test = _testService.GetAll();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
