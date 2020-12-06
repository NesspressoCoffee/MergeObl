using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MCompra
    {
        public DtoCompra MapToDto(Compra obj)
        {
            DtoCompra dto = new DtoCompra();

            dto.apellidoTitular = obj.apellidoTitular;
            dto.nombreTitular = obj.nombreTitular;
            dto.documentoTitular = obj.documentoTitular;
            dto.fechaCompra = obj.fechaCompra;
            dto.companiaTarjeta = obj.companiaTarjeta;
            dto.tipoTarjeta = obj.tipoTarjeta;
            dto.companiaTarjeta = obj.companiaTarjeta;
            dto.email = obj.email;
            dto.idCompra = obj.idCompra;
            dto.numeroTarjeta = obj.numeroTarjeta;
            dto.vencimientoTarjeta = obj.vencimientoTarjeta;


            return dto;
        }

        public Compra MapToObj(DtoCompra dto)
        {
            Compra obj = new Compra();
            obj.apellidoTitular = dto.apellidoTitular;
            obj.nombreTitular = dto.nombreTitular;
            obj.documentoTitular = dto.documentoTitular;
            obj.fechaCompra = dto.fechaCompra;
            obj.companiaTarjeta = dto.companiaTarjeta;
            obj.tipoTarjeta = dto.tipoTarjeta;
            obj.companiaTarjeta = dto.companiaTarjeta;
            obj.email = dto.email;
            obj.numeroTarjeta = dto.numeroTarjeta;
            obj.vencimientoTarjeta = dto.vencimientoTarjeta;

            return obj;
        }
    }
}
