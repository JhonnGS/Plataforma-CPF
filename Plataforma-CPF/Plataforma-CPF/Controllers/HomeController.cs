using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plataforma_CPF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de ......";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Esta es tu pagina de contacto";

            return View();
        }

        public ActionResult Aviso()
        {
            return View();
        }
        
        public ActionResult Bienvenido()
        {
            ViewBag.Message = "SEA USTDE BIENVENIDO A SU SESIÓN DE CPF";

            return View();
        }
    }
}