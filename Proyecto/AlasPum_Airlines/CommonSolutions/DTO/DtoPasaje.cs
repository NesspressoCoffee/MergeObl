using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoPasaje
    {

        public int idPasaje { get; set; }
        public string nombrePasajero { get; set; }
        public string apellidoPasajero { get; set; }
        public string documentoPasajero { get; set; }
        public int vueloId { get; set; }
        public int compraId { get; set; }
        public int asientoNumero { get; set; }
        public int avionId { get; set; }

    }
}