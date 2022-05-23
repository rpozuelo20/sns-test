using Microsoft.AspNetCore.Mvc;
using sns_test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sns_test.Controllers
{
    public class NotificationController : Controller
    {
        private ServiceAWSSNS _service;
        public NotificationController(ServiceAWSSNS service)
        {
            this._service = service;
        }

        public IActionResult SendSNS()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendSNS(string num)
        {
            Random random = new Random();
            int code = random.Next(100000, 999999);
            TempData["CODE"] = code;
            await this._service.EnviarSNS(num, code);
            return RedirectToAction("VerifySNS");
        }
        public IActionResult VerifySNS()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifySNS(int code)
        {
            int codigo = (int)TempData["CODE"];
            if (code == codigo)
            {
                ViewData["MENSAJE"] = "VERIFICACION EXITOSA";
            }
            else
            {
                ViewData["MENSAJE"] = "VERIFICACION FALLIDA";
            }
            return View();
        }
    }
}
