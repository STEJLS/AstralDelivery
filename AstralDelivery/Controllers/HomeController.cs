﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер главной страницы пользователя
    /// </summary>
    [Authorize]
    [Route("Home")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
