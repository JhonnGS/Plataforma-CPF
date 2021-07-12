using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Net;
using Plataforma_CPF.Models;

using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Plataforma_CPF.Models.ViewModels;
using Plataforma_CPF.Repositories;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
namespace Plataforma_CPF.Controllers
{
    public class AdministradorController : Controller
    {
        private BDCPFORIEntities db = new BDCPFORIEntities();
        private IUsuarioRepository _repoUsuario;

        public AdministradorController(IUsuarioRepository repository)
        {
            _repoUsuario = repository;
        }

        public AdministradorController() : this(new UsuarioRepository())
        {

        }

        public ActionResult _LayoutMA(int? id)
        {
            if (Session["idUs"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult HomeAd(int? mesg, int? id)
        {
            return View();
        }
        // GET: Alumnos        
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Include(u => u.perfil);
            return View(usuario.ToList());
        }
        
        public ActionResult Mochila(int? id)
        {
            //ViewBag.idAlumno = new SelectList(db.Usuarios, "idUsuario", "nombre");
            return View(db.Mochila.ToList());
        }

        // GET: DocMochila/Create
        public ActionResult DocMochila()
        {
            //ViewBag.idAlumno = new SelectList(db.Usuarios, "idUsuario", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DocMochila([Bind(Include = "idMochila,nombre,elemento,fecha_subido,descripcion,status,idUsuario")] Mochila Doc)
        {
            if (ModelState.IsValid)
            {
                db.Mochila.Add(Doc);
                db.SaveChanges();

                return RedirectToAction("HomeAd");
            }

            return View(Doc);
        }

        public ActionResult AdPerfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios u = db.Usuarios.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdPerfil([Bind(Include = "idUsuario,usuario,correo,contraseña,perfil,status,TokenRecovery")] Usuarios u)
        {
            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }
            return View(u);
        }
        public ActionResult ADConfiguracion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrador ad = db.Administrador.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ADConfiguracion([Bind(Include = "idAdmin,nombre,app,apm,sexo,direccion,telefono,claveAdmin,idUsuario")] Administrador ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeAd");
            }
            return View(ad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }          
               
        public ActionResult CerrarSesion()
        {
            //SessionHelper.DestroyUserSession();
            Session["idAdministrador"] = null;
            Session["nombre"] = null;
            Session["UserAd"] = null;
            ViewBag.M = "USTED HA SALIDO DE SU SESIÓN";
            return RedirectToAction("Login", "Account");
        }
    }
}