using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoPasajero
    {
        public int id { get; set; }
        public string nombre {get; set;}
        public string apellido { get; set; }
        public string documento { get; set; }

        public DtoPasajero(string nombre, string apellido, string documento)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.documento = documento;
        }
    }
}
