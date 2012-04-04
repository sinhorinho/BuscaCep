using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaCep.Models;

namespace BuscaCep.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DadosLogradouro(string id)
        {
            Logradouro log = new Logradouro();

            return Json(log.RecuperaCep(id),JsonRequestBehavior.AllowGet);
        }

      
    }
}
