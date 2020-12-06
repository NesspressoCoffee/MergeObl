using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonSolutions.DTO
{
    public class DtoEmpleado
    {
        [DisplayName("Documento: ")]
        [Remote("UserAvailable", "Vuelo", ErrorMessage = "Ya existe un empleado con ese documento")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(15, ErrorMessage = "El {0} no debe superar los {1} caracteres, y debe ser ingresado sin puntos ni guiones!")]
        public string documentoEmpleado { get; set; }

        [DisplayName("Nombre: ")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(20, ErrorMessage = "El {0} no debe superar los {1} caracteres.")]
        public string nombreEmpleado { get; set; }

        [DisplayName("Apellido: ")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(20, ErrorMessage = "El {0} no debe superar los {1} caracteres.")]
        public string apellidoEmpleado { get; set; }

        [Display(Name = "Contraseña: ")]
        [Required(ErrorMessage = "La {0} es requerida!")]
        [StringLength(15, ErrorMessage = "Longitud no puede exceder los 20 caracteres.",
                     MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string contrasenia { get; set; }

        public bool enServicio { get; set; }
    }
}