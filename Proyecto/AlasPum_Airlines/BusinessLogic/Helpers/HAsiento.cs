using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HAsiento
    {
        private static HAsiento instance;

        private HAsiento()
        {

        }

        public static HAsiento getInstance()
        {
            if (instance == null)
            {
                instance = new HAsiento();
            }

            return instance;
        }


        public List<DtoAsiento> ObtenerAsientosAvion(int id)
        {
            PAsiento pa = new PAsiento();
            return pa.GetAsientosDisponibles(id);

        }


        public List<DtoVuelo> GetAsientosDisponibles(DtoVuelo vuelo, List<DtoVuelo> dto)
        {
            List<DtoVuelo> vuelosDirectos = new List<DtoVuelo>();

            foreach (DtoVuelo item in dto)
            {
                int asientosDisponibles = HPasaje.getInstance().GetAsientosDisponibles(item.idVuelo, item.avionId).Count();
                if (vuelo.cantidadAsientos <= asientosDisponibles)
                {
                    vuelosDirectos.Add(item);
                }
            }

            return vuelosDirectos;
        }
    }
}


















