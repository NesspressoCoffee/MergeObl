using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MVuelo
    {
        public DtoVuelo MapToDto(Vuelo obj)
        {
            DtoVuelo dto = new DtoVuelo();
            dto.avionId = obj.avionId;
            dto.destino = obj.destino;
            dto.estado = obj.estado;
            dto.fechaLlegada = obj.fechaLlegada;
            dto.fechaSalida = obj.fechaSalida;
            dto.horasVuelo = obj.horasVuelo;
            dto.idVuelo = obj.idVuelo;
            dto.numeroVuelo = obj.numeroVuelo;
            dto.origen = obj.origen;
            dto.tipo = obj.tipo;
            dto.visa = obj.visa;
            dto.horaSalida = obj.fechaSalida;

            return dto;
        }

        public Vuelo MapToObj(DtoVuelo dto)
        {
            Vuelo obj = new Vuelo();
            obj.avionId = dto.avionId;
            obj.destino = dto.destino;
            obj.estado = dto.estado;
            obj.fechaLlegada = dto.fechaLlegada;
            obj.fechaSalida = dto.fechaSalida;
            obj.horasVuelo = dto.horasVuelo;
            obj.idVuelo = dto.idVuelo;
            obj.numeroVuelo = dto.numeroVuelo;
            obj.origen = dto.origen;
            obj.tipo = dto.tipo;
            obj.visa = dto.visa;

            return obj;
        }
    }
}
