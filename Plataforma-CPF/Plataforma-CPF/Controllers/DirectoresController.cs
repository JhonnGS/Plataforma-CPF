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
    public class DirectoresController : Controller
    {
        private BDCPFORIEntities db = new BDCPFORIEntities();
        private IUsuarioRepository _repoUsuario;

        public DirectoresController(IUsuarioRepository repository)
        {
            _repoUsuario = repository;
        }

        public DirectoresController() : this(new UsuarioRepository())
        {

        }

        public ActionResult _LayoutMD(int? id)
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

        public ActionResult HomeD(int? mesg, int? id)
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

        public ActionResult DPerfil(int? id)
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
        public ActionResult DPerfil([Bind(Include = "idUsuario,usuario,correo,contraseña,perfil,status,TokenRecovery")] Usuarios u)
        {
            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeD");
            }
            return View(u);
        }
        public ActionResult DConfiguracion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directores D = db.Directores.Find(id);
            if (D == null)
            {
                return HttpNotFound();
            }
            return View(D);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DConfiguracion([Bind(Include = "idDirector,nombre,app,apm,sexo,direccion,telefono,claveAdmin,idUsuario")] Directores D)
        {
            if (ModelState.IsValid)
            {
                db.Entry(D).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeD");
            }
            return View(D);
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
            Session["idDirectores"] = null;
            Session["nombre"] = null;
            Session["UserD"] = null;
            ViewBag.M = "USTED HA SALIDO DE SU SESIÓN";
            return RedirectToAction("Login", "Account");
        }
    }
}