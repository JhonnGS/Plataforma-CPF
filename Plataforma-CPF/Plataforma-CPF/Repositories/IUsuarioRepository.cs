using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plataforma_CPF.Models;

namespace Plataforma_CPF.Repositories
{
    public interface IUsuarioRepository
    {       
        /// <summary>
        /// Busca por nombre de usuario y retorna un objeto de tipo Usuario
        /// </summary>
        /// <param name="Nombre">nombre de usuario</param>
        /// <returns>Objeto usuario</returns>
        Usuarios GetPorNombre(string Nombre);

        /// <summary>
        /// Busca por correo para poder actualizar su correo
        /// </summary>
        /// <param name="correo">correo</param>
        /// <returns>Objeto usuario</returns>
        Usuarios getPorCorreo(string correo);
        //UsuariosSet getPorUserid(int? id);                        

        ///// <summary>
        ///// Lista por tipo perfil
        ///// </summary>
        ///// <param name="Perfil"></param>
        ///// <returns>Lista de usuarios</returns>
        //List<Usuarios> getPorPerfil(string Perfil);

        /// <summary>
        /// Verificar si existe usuario y correo de la bd
        /// </summary>
        /// <param name="user"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        Usuarios getPorUserCorreo(string user, string correo);

        /// <summary>
        /// Verificar idUsuario y usuario de la bd
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Usuarios getPorUserid(int? id);

    }
}