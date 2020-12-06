using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PPasaje
    {
        public MPasaje mapper;

        public PPasaje()
        {
            this.mapper = new MPasaje();
        }

        public List<DtoPasaje> GetPasajesVuelo(int id)
        {
            List<DtoPasaje> pasajesVuelo = new List<DtoPasaje>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Pasaje> pasajesBD = context.Pasaje.Where(w => w.vueloId == id).ToList();
                foreach (Pasaje item in pasajesBD)
                {
                    pasajesVuelo.Add(mapper.MapToDto(item));
                }
            }

            return pasajesVuelo;
        }


    }
}