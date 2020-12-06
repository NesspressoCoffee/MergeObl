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
    public class DtoAvion
    {

        public int idAvion { get; set; }

        [DisplayName("Fecha de Ingreso")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaIngreso { get; set; }

        [DisplayName("Horas Totales")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Las Horas Totales no pueden ser negativas")]
        public double horasTotales { get; set; }

        [DisplayName("Modelo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, ErrorMessage = "El campo {0} no debe superar los {1} caracteres")]
        public string modelo { get; set; }

        [DisplayName("Cantidad de Asientos")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, Int32.MaxValue, ErrorMessage = "La {0} no puede ser negativa")]

        public int cantAsientos { get; set; }

        public bool disponible { get; set; }

        public string linkVideo { get; set; }
    }
}
