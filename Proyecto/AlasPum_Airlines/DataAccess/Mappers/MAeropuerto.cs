using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MAeropuerto
    {
        public DtoAeropuerto MapToDto(Aeropuerto obj)
        {
            DtoAeropuerto dto = new DtoAeropuerto();
            dto.abreviatura = obj.abreviatura;
            dto.continente = obj.continente;
            dto.pais = obj.pais;
            dto.ciudad = obj.ciudad;
            dto.disponible = obj.disponible;

            return dto;
        }

        public Aeropuerto MapToObj(DtoAeropuerto dto)
        {
            Aeropuerto obj = new Aeropuerto();
            obj.abreviatura = dto.abreviatura;
            obj.continente = dto.continente;
            obj.pais = dto.pais;
            obj.ciudad = dto.ciudad;
            obj.disponible = dto.disponible;

            return obj;
        }

    }
}
