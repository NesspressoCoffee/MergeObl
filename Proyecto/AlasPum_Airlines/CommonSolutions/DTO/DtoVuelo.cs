using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoVuelo
    {
       
        DtoPrecioCategoria pcModel = new DtoPrecioCategoria();
        public DtoPrecioCategoria precioCat
        {
            get { return this.pcModel; }
            set { pcModel = value; }
        }
        public int idVuelo { get; set; }
   
        [Required(ErrorMessage = "El {0} es requerido!")]
        public string numeroVuelo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaSalida { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaLlegada { get; set; }

        public DateTime fechaGrilla { get; set; }
        public double horasVuelo { get; set; }
        public string estado { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public int avionId { get; set; }
        public string tipo { get; set; }
        public string visa { get; set; }
        public List<string> dias { get; set; }

       public int cantidadAsientos { get; set; }
        public string categoria{ get; set; }

        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime horaSalida { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaComienzo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaLimite { get; set; }

    }
}