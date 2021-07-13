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

namespace CPF_Plataforma.Controllers
{
    public class AlumnosController : Controller
    {
        private BDCPFORIEntities db = new BDCPFORIEntities();
        private IUsuarioRepository _repoUsuario;

        public AlumnosController(IUsuarioRepository repository)
        {
            _repoUsuario = repository;
        }

        public AlumnosController() : this(new UsuarioRepository())
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

        public ActionResult HomeA(int? mesg, int? id)
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

                return RedirectToAction("HomeA");
            }

            return View(Doc);
        }    
        
        public ActionResult APerfil(int? id)
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
        public ActionResult APerfil([Bind(Include = "idUsuario,usuario,correo,contraseña,perfil,status,TokenRecovery")] Usuarios u)
        {
            if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }
            return View(u);
        }
        public ActionResult AConfiguracion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumnos a = db.Alumnos.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AConfiguracion([Bind(Include = "idAlumno,nombre,app,apm,sexo,direccion,telefono,idUsuario,seccion,grado,grupo")] Alumnos a)
        {
            if (ModelState.IsValid)
            {
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }
            return View(a);
        }
        // GET: CatAreas/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CatAreas catAreas = db.CatAreasSet.Find(id);
        //    if (catAreas == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(catAreas);
        //}

        // POST: CatAreas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CatAreas catAreas = db.CatAreasSet.Find(id);
        //    db.CatAreasSet.Remove(catAreas);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Materia()
        {
            //ViewBag.idAlumno = new SelectList(db.Usuarios, "idUsuario", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cargarMateria([Bind(Include = "idMateria,nombre")] Materias ma)
        {
            if (ModelState.IsValid)
            {
                db.Materias.Add(ma);
                db.SaveChanges();

                return RedirectToAction("Materia");
            }

            return View(ma);
        }

        // GET: Alumnos/EditDatosE/5
        public ActionResult EditDatosE(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumnos user = db.Alumnos.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.Mess = "HOLA! ACONTINUACIÓN CAMBIAREMOS SUS DATOS ESCOLARES (NUEVO INICIO DE CLASE)";
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDatosE([Bind(Include = "idAlumno,nombre,app,apm,sexo,direccion,telefono,idUsuario,seccion,grado,grupo")] Alumnos usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }

            return View(usuario);
        }

        public ActionResult CerrarSesion()
        {
            //SessionHelper.DestroyUserSession();
            Session["idAlumno"] = null;
            Session["nombre"] = null;
            Session["UserA"] = null;
            ViewBag.M = "USTED HA SALIDO DE SU SESIÓN";
            return RedirectToAction("Login", "Account");
        }
    }
}