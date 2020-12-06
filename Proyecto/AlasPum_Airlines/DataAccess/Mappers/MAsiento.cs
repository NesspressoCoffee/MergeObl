using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MAsiento
    {
        public DtoAsiento MapToDto(Asiento obj)
        {
            DtoAsiento dto = new DtoAsiento();
            dto.categoria = obj.categoria;
            dto.disponible = obj.disponible;
            dto.idAvion = obj.avionId;
            dto.numeroAsiento = obj.numeroAsiento;

            return dto;
        }

        public Asiento MapToObj(DtoAsiento dto)
        {
            Asiento obj = new Asiento();
            obj.categoria = dto.categoria;
            obj.disponible = dto.disponible;
            obj.avionId = dto.idAvion;
            obj.numeroAsiento = dto.numeroAsiento;

            return obj;
        }
    }
}
