using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MEmpleado
    {
        public DtoEmpleado MapToDto(Empleado obj)
        {
            DtoEmpleado dto = new DtoEmpleado();
            dto.documentoEmpleado = obj.documentoEmpleado;
            dto.nombreEmpleado = obj.nombreEmpleado;
            dto.apellidoEmpleado = obj.apellidoEmpleado;
            dto.contrasenia = obj.contrasenia;
            dto.enServicio = obj.disponible;
            return dto;
        }

        public Empleado MapToObj(DtoEmpleado dto)
        {
            Empleado obj = new Empleado();
            obj.documentoEmpleado = dto.documentoEmpleado;
            obj.nombreEmpleado = dto.nombreEmpleado;
            obj.apellidoEmpleado = dto.apellidoEmpleado;
            obj.contrasenia = dto.contrasenia;
            obj.disponible = true;
            return obj;
        }

    }
}