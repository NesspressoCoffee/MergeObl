using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoCategoriaAsiento
    {
        public int id { get; set; }
        public int? desde { get; set; }
        public int? hasta { get; set; }
        public string categoria { get; set; }

        public DtoCategoriaAsiento() { }

        public DtoCategoriaAsiento(int? _desde, int? _hasta, string _categoria)
        {
            this.desde = _desde;
            this.hasta = _hasta;
            this.categoria = _categoria;


        }
    }
}
