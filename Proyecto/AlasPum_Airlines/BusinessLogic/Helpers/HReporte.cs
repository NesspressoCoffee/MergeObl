using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HReporte
    {
        private static HReporte instance;

        private HReporte()
        {

        }
        public static HReporte getInstance()
        {
            if (instance == null)
            {
                instance = new HReporte();
            }

            return instance;
        }

        public float porcentajeDeOcupacionByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            PReportes pr = new PReportes();
            return pr.porcentajeDeOcupacionByFechas(fechaDesde, fechaHasta);
        }

        public List<DtoReporteAsientosVacios> masMenosAsientosOcupadosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            PReportes pr = new PReportes();
            return pr.masMenosAsientosOcupadosByFecha(fechaDesde, fechaHasta);
        }

        public DtoCliente clienteConMasComprasByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            PReportes pr = new PReportes();
            return pr.clienteConMasComprasByFechas(fechaDesde, fechaHasta);
        }
    }
}
