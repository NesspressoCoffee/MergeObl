using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PAsiento
    {
        public MAsiento mapper;

        public PAsiento()
        {
            this.mapper = new MAsiento();
        }

        public List<DtoAsiento> GetAsientosDisponibles(int idAvion)
        {
            List<DtoAsiento> dto = new List<DtoAsiento>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Asiento> asientos = context.Asiento.Where(w => w.avionId == idAvion && w.disponible == true).ToList();
                foreach(Asiento item in asientos)
                {
                    dto.Add(mapper.MapToDto(item));
                }
            }

            return dto;
        }

        

    }
}
