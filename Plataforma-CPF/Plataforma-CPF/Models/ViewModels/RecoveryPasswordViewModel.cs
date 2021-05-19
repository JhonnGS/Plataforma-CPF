using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plataforma_CPF.Models.ViewModels
{
    public class RecoveryPasswordViewModel
    {
        public string token { get; set; }
        [Required(ErrorMessage = "EL CAMPO ES REQUERIDO")]
        public string Password { get; set; }
        [Compare("Password")]
        [Required(ErrorMessage = "EL CAMPO ES REQUERIDO")]
        public string Password2 { get; set; }

    }
}