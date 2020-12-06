using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
   public class DtoHistoricoTasa
    {
        [DisplayName("Fecha de Alta")]
        public DateTime fecha { get; set; }

        [DisplayName("Tasa Regional")]
        [Required(ErrorMessage = "Debe ingresar una {0}!")]
        public double tasaRegional { get; set; }

        [DisplayName("Tasa Intercontinental")]
        [Required(ErrorMessage = "Debe ingresar una {0}!")]
        public double tasaInter { get; set; }

        [DisplayName("Aeropuerto")]
        public string codigoAirport { get; set; }

    }
}
