using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MPrecioCategoria
    {
        public DtoPrecioCategoria MapToDto(PrecioCategoria obj)
        {
            DtoPrecioCategoria dto = new DtoPrecioCategoria();

            dto.vueloId = obj.vueloId;
            dto.precioEconomy = obj.precioEconomy;
            dto.precioBusiness = obj.precioBusiness;
            dto.precioFirstClass = obj.precioFirstClass;
            dto.precioPremium = obj.precioPremium;

            return dto;
        }

        public PrecioCategoria MapToObj(DtoPrecioCategoria dto)
        {
            PrecioCategoria obj = new PrecioCategoria();
            obj.vueloId = dto.vueloId;
            obj.precioEconomy = dto.precioEconomy;
            obj.precioBusiness = dto.precioBusiness;
            obj.precioFirstClass = dto.precioFirstClass;
            obj.precioPremium = dto.precioPremium;

            return obj;
        }
    }
}