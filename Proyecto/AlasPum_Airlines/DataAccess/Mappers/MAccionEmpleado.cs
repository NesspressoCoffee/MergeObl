using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MAccionEmpleado
    {
        public DtoAccionEmpleado MapToDto(AccionesEmpleados obj)
        {
            DtoAccionEmpleado dto = new DtoAccionEmpleado();

            dto.docEmpleado = obj.docEmpleado;
            dto.fecha = obj.fecha;
            dto.id = obj.id;
            dto.vueloId = obj.vueloId;
            dto.abreviaturaAeropuerto = obj.abreviaturaAeropuerto;
            dto.tipoModificacion = obj.tipoModificacion;

            return dto;
        }

        public AccionesEmpleados MapToObj(DtoAccionEmpleado dto)
        {
            AccionesEmpleados obj = new AccionesEmpleados();
            obj.avionId = dto.avionId;
            obj.docEmpleado = dto.docEmpleado;
            obj.fecha = dto.fecha;
            obj.id = dto.id;
            obj.vueloId = dto.vueloId;
            obj.abreviaturaAeropuerto = dto.abreviaturaAeropuerto;
            obj.tipoModificacion = dto.tipoModificacion;

            return obj;
        }
    }
}
