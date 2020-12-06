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
    public class DtoVuelo
    {
        DtoPrecioCategoria pcModel = new DtoPrecioCategoria();
        public DtoPrecioCategoria precioCat
        {
            get { return this.pcModel; }
            set { pcModel = value; }
        }
        public int idVuelo { get; set; }

        [DisplayName("Numero de Vuelo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, ErrorMessage = "El campo {0} no debe superar los {1} caracteres")]
        [Remote("ValidarNumeroVuelo", "Vuelo", ErrorMessage = "Ya existe una tanda de vuelos activa con ese numero")]
        public string numeroVuelo { get; set; }

        [DisplayName("Salida")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaSalida { get; set; }

        [DisplayName("Llegada")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaLlegada { get; set; }

        [DisplayName("Horas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]              
        [Range(0, Int32.MaxValue, ErrorMessage = "Las Horas de Vuelo no pueden ser negativas")]
        public double horasVuelo { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string estado { get; set; }

        [DisplayName("Origen")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string origen { get; set; }

        [DisplayName("Destino")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Remote("ValidarDestino", "Aeropuerto", AdditionalFields = "origen", ErrorMessage = "El Destino debe ser distinto al Origen")]
        public string destino { get; set; }

        [DisplayName("Avion")]
        [Remote("ValidarExistenciaAvion", "Avion", ErrorMessage = "Numero de aeronave invalido")]
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int avionId { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string tipo { get; set; }

        [DisplayName("Visa")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string visa { get; set; }

        [DisplayName("Hora de Salida")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Time)]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime horaSalida { get; set; }

        [DisplayName("Fecha de Comienzo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Remote("ValidarFechaComienzo", "Vuelo", ErrorMessage = "La fecha de comienzo debe ser igual o mayor que hoy")]
        public System.DateTime fechaComienzo { get; set; }

        [DisplayName("Fecha Limite")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Remote("ValidarFechaLimite", "Vuelo", AdditionalFields = "fechaComienzo", ErrorMessage = "La fecha limite debe ser mayor a la de comienzo")]
        public System.DateTime fechaLimite { get; set; }

        [DisplayName("Economy")]
        [Remote("ValidarPrecioEconomy", "Vuelo", ErrorMessage = "Debe asignarle un precio a {0}")]
        public double precioEconomy { get; set; }

        [DisplayName("Business")]
        [Remote("ValidarPrecioBusiness", "Vuelo", ErrorMessage = "Debe asignarle un precio a {0}")]
        public double precioBusiness { get; set; }

        [DisplayName("Premium")]
        [Remote("ValidarPrecioPremium", "Vuelo", ErrorMessage = "Debe asignarle un precio a {0}")]
        public double precioPremium { get; set; }

        [DisplayName("First Class")]
        [Remote("ValidarPrecioFirstClass", "Vuelo", ErrorMessage = "Debe asignarle un precio a {0}")]
        public double precioFirstClass { get; set; }

        public int cantidadAsientos { get; set; }

    }
}
