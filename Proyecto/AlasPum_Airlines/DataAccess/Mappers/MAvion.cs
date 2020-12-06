using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MAvion
    {
        public DtoAvion MapToDto(Avion obj)
        {
            DtoAvion dto = new DtoAvion();
            dto.idAvion = obj.idAvion;
            dto.cantAsientos = obj.cantAsientos;
            dto.fechaIngreso = obj.fechaIngreso;
            dto.horasTotales = obj.horasTotales;
            dto.modelo = obj.modelo;
            dto.disponible = obj.disponible;

            return dto;
        }

        public Avion MapToObj(DtoAvion dto)
        {
            Avion obj = new Avion();
            obj.idAvion = dto.idAvion;
            obj.cantAsientos = dto.cantAsientos;
            obj.fechaIngreso = dto.fechaIngreso;
            obj.horasTotales = dto.horasTotales;
            obj.modelo = dto.modelo;
            obj.disponible = dto.disponible;

            return obj;
        }
    }
}
