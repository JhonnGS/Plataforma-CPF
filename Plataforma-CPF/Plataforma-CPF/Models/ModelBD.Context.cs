﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Plataforma_CPF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDCPFORIEntities : DbContext
    {
        public BDCPFORIEntities()
            : base("name=BDCPFORIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actividades> Actividades { get; set; }
        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Alumnos> Alumnos { get; set; }
        public virtual DbSet<Directores> Directores { get; set; }
        public virtual DbSet<EntregasTareas> EntregasTareas { get; set; }
        public virtual DbSet<Examenes> Examenes { get; set; }
        public virtual DbSet<Maestros> Maestros { get; set; }
        public virtual DbSet<Materias> Materias { get; set; }
        public virtual DbSet<Mochila> Mochila { get; set; }
        public virtual DbSet<PreguntasExamen> PreguntasExamen { get; set; }
        public virtual DbSet<RespuestasAlumno> RespuestasAlumno { get; set; }
        public virtual DbSet<Tareas> Tareas { get; set; }
        public virtual DbSet<Tutores> Tutores { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
