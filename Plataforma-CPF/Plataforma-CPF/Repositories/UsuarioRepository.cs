using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plataforma_CPF.Models;

namespace Plataforma_CPF.Repositories
{

    public class UsuarioRepository : IUsuarioRepository
    {
        public BDCPFORIEntities db;

        public UsuarioRepository()
        {
            this.db = new BDCPFORIEntities();
        }


        public Usuarios GetPorNombre(string Nombre)
        {
            var query = (from u in db.Usuarios
                         where u.usuario == Nombre
                         select u);
            return query.FirstOrDefault<Usuarios>();
        }

        public Usuarios getPorCorreo(string correo)
        {
            var query = (from u in db.Usuarios
                         where u.correo == correo
                         select u);
            return query.FirstOrDefault<Usuarios>();
        }

        //public List<Usuarios> getPorPerfil(string Perfil)
        //{
        //    List<Usuarios> listUsuarios = new List<Usuarios>();
        //    var Query = (from u in db.Usuarios
        //                 where u.perfil == Perfil
        //                 select u);

        //    foreach (var obj in Query)
        //    {
        //        listUsuarios.Add(obj);
        //    }

        //    return listUsuarios;
        //}

        public Usuarios getPorUserCorreo(string user, string correo)
        {
            var query = (from u in db.Usuarios
                         where u.usuario == user && u.correo == correo
                         select u);
            return query.FirstOrDefault<Usuarios>();
        }

        public Usuarios getPorUserid(int? id)
        {
            var query = (from u in db.Usuarios
                         where u.idUsuario == id
                         select u);
            return query.FirstOrDefault<Usuarios>();
        }

        ////public UsuariosSet getPorUserid(int? id)
        ////{
        ////    throw new NotImplementedException();
        ////}
    }
}