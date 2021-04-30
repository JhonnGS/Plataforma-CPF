//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Examenes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Examenes()
        {
            this.PreguntasExamen = new HashSet<PreguntasExamen>();
            this.RespuestasAlumno = new HashSet<RespuestasAlumno>();
        }
    
        public int idExamen { get; set; }
        public int idMateria { get; set; }
        public string nombreExamen { get; set; }
        public System.DateTime fechaI { get; set; }
        public System.DateTime fechaC { get; set; }
    
        public virtual Materias Materias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreguntasExamen> PreguntasExamen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RespuestasAlumno> RespuestasAlumno { get; set; }
    }
}
