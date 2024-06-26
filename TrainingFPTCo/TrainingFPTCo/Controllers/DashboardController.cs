﻿using Microsoft.AspNetCore.Mvc;

namespace TrainingFPTCo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("SessionRoleId")))
            {
				return RedirectToAction(nameof(LoginController.Index), "Login");
			}
            return View();
        }
    }
}
