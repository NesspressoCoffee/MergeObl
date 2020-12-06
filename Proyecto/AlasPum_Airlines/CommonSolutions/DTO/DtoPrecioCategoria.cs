using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoPrecioCategoria
    {
        public double precioEconomy { get; set; }
        public double precioPremium { get; set; }
        public double precioBusiness { get; set; }
        public double precioFirstClass { get; set; }
        public int vueloId { get; set; }
    }
}