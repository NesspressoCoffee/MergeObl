using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MPasaje
    {
        public DtoPasaje MapToDto(Pasaje obj)
        {
            DtoPasaje dto = new DtoPasaje();

            dto.idPasaje = obj.idPasaje;
            dto.nombrePasajero = obj.nombrePasajero;
            dto.apellidoPasajero = obj.apellidoPasajero;
            dto.documentoPasajero = obj.documentoPasajero;
            dto.vueloId = obj.vueloId;
            dto.compraId = obj.compraId;
            dto.asientoNumero = obj.asientoNumero;
            dto.avionId = obj.avionId;

            return dto;
        }

        public Pasaje MapToObj(DtoPasaje dto)
        {
            Pasaje obj = new Pasaje();

            obj.idPasaje = dto.idPasaje;
            obj.nombrePasajero = dto.nombrePasajero;
            obj.apellidoPasajero = dto.apellidoPasajero;
            obj.documentoPasajero = dto.documentoPasajero;
            obj.vueloId = dto.vueloId;
            obj.compraId = dto.compraId;
            obj.asientoNumero = dto.asientoNumero;
            obj.avionId = dto.avionId;

            return obj;
        }
    }
}