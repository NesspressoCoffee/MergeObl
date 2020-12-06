using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoLogin
    {
        [DisplayName("Documento")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        public string documentoEmpleado { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es requerida!")]
        [StringLength(15, ErrorMessage = "Longitud no puede exceder los 20 caracteres.",
                     MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string contrasenia { get; set; }
    }
}