using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoAsiento
    {
        public int numeroAsiento { get; set; }
        public string categoria { get; set; }
        public bool disponible { get; set; }
        public int idAvion { get; set; }

        public DtoAsiento() { }
        public DtoAsiento(int _numeroAsiento, int _idAvion)
        {
            this.numeroAsiento = _numeroAsiento;
            this.idAvion = _idAvion;
        }
    }
}