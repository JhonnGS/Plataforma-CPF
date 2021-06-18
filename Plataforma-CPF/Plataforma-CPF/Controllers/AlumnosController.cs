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

        // GET: Alumnos/Perfil/5
        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            
            return View(usuario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil([Bind(Include = "idUsuario,foto,usuario,correo,contraseña,perfil,status,TokenRecovery")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }
            
            return View(usuario);
        }
        
        // GET: Alumnos/Configuracion/5
        public ActionResult Configuracion(int? id)
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
            
            ViewBag.Mess = "HOLA! ACONTINUACIÓN CAMBIAREMOS SUS DATOS DE USUARIO (PERFÍL)";
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Configuracion([Bind(Include = "idAlumno,nombre,app,apm,sexo,direccion,telefono,idUsuario,seccion,grado,grupo")] Alumnos usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HomeA");
            }
           
            return View(usuario);
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

        // GET: CatAreas/Edit/5
        //public ActionResult Edit(int? id)
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

        // POST: CatAreas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id_area,nombre,descripcion,responsable,correo,estatus")] CatAreas catAreas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(catAreas).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(catAreas);
        //}

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
            return RedirectToAction("Login", "Acount");
        }
    }
}