using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoDisponibilidadAsiento
    {
        public int id { get; set; }
        public int? desde { get; set; }
        public int? hasta { get; set; }
        public bool disponibilidad { get; set; }
        public string disponibilidadStr { get; set; }

        public DtoDisponibilidadAsiento(){}

        public DtoDisponibilidadAsiento(int? _desde, int? _hasta, string _disponible)
        {
            this.desde = _desde;
            this.hasta = _hasta;
            this.disponibilidadStr = _disponible;
            if (_disponible == "Si") this.disponibilidad = true;
            else this.disponibilidad = false;
        }

    }
}
