using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoAccionEmpleado
    {
        public int id { get; set; }
        public string tipoModificacion { get; set; }
        public System.DateTime fecha { get; set; }
        public string docEmpleado { get; set; }
        public Nullable<int> avionId { get; set; }
        public int? vueloId { get; set; }
        public string abreviaturaAeropuerto { get; set; }

        public DtoAccionEmpleado(string tipoModificacion, DateTime fecha, string docEmpleado, int? avionId, int? vueloId, string abreviaturaAeropuerto)
        {
            this.tipoModificacion = tipoModificacion;
            this.fecha = fecha;
            this.docEmpleado = docEmpleado;
            this.avionId = avionId;
            this.vueloId = vueloId;
            this.abreviaturaAeropuerto = abreviaturaAeropuerto;
        }

        public DtoAccionEmpleado() { }

    }
}
