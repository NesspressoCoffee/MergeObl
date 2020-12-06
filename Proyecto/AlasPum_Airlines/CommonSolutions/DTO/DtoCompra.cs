using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoCompra
    {
        DtoPasaje pasajeModel = new DtoPasaje();

        public DtoPasaje pasaje
        {
            get { return this.pasajeModel; }
            set { pasajeModel = value; }
        }

        public DtoVuelo vuelo { get; set; }

        public int idCompra { get; set; }

       
        public System.DateTime fechaCompra { get; set; }
        public string nombreTitular { get; set; }
        public string apellidoTitular { get; set; }
        public string documentoTitular { get; set; }
        public string tipoTarjeta { get; set; }
        public string companiaTarjeta { get; set; }
        public int numeroTarjeta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> vencimientoTarjeta { get; set; }
        public string email { get; set; }
        public bool titular { get; set; }
    }
}
