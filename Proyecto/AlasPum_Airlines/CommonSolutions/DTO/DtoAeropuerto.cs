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
    public class DtoAeropuerto
    {
        DtoHistoricoTasa htModel = new DtoHistoricoTasa();

        [Required(ErrorMessage = "El {0} es requerido!")]
        public DtoHistoricoTasa historico
        {
            get { return this.htModel; }
            set { htModel = value; }
        }

        [DisplayName("Continente")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(20, ErrorMessage = "El {0} no debe superar los {1} caracteres.")]
        public string continente { get; set; }

        [DisplayName("País")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(20, ErrorMessage = "El {0} no debe superar los {1} caracteres.")]
        public string pais { get; set; }

        [DisplayName("Ciudad")]
        [Required(ErrorMessage = "La {0} es requerida!")]
        [StringLength(20, ErrorMessage = "El {0} no debe superar los {1} caracteres.")]
        public string ciudad { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "El {0} es requerido!")]
        [StringLength(5, ErrorMessage = "El {0} a ingresar debe ser el dispuesto por la IATA.")]
        [Remote("CodigoAvailable", "Aeropuerto", ErrorMessage = "Ya existe este codigo.")]
        public string abreviatura { get; set; }

        [DisplayName("Tasa Aeroportuaria")]
        [Required(ErrorMessage = "La {0} es requerida!")]
        public double tasa { get; set; }

        [DisplayName("Habilitado")]
        [HiddenInput(DisplayValue = true)]
        public bool disponible { get; set; }
    }
}
