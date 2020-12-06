using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HPasaje
    {
        private static HPasaje instance;

        private HPasaje()
        {

        }

        public static HPasaje getInstance()
        {
            if (instance == null)
            {
                instance = new HPasaje();
            }

            return instance;
        }

        public List<DtoAsiento> GetAsientosDisponibles(int idVuelo, int avionId)
        {
            List<DtoAsiento> asientosDisponibles = new List<DtoAsiento>();
            List<DtoAsiento> asientosDeAvion = HAsiento.getInstance().ObtenerAsientosAvion(avionId);
            PPasaje pp = new PPasaje();
            List<DtoPasaje> dp = pp.GetPasajesVuelo(idVuelo);
            foreach (DtoAsiento a in asientosDeAvion)
            {
                if (dp.Count == 0)
                {
                    return asientosDeAvion;

                }
                else
                {

                    foreach (DtoPasaje p in dp)
                    {

                        if (a.numeroAsiento != p.asientoNumero)
                        {

                            asientosDisponibles.Add(a);
                        }

                    }
                }


            }

            return asientosDisponibles;

        }
    }
}