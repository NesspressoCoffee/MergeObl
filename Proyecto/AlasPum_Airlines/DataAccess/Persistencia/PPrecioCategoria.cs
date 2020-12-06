using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PPrecioCategoria
    {
        public MPrecioCategoria mapper;

        public PPrecioCategoria()
        {
            this.mapper = new MPrecioCategoria();
        }


        public DtoPrecioCategoria GetPreciosByIdVuelo(int idVuelo)
        {
            PrecioCategoria precios = new PrecioCategoria();
            using (alasdbEntities context = new alasdbEntities())
            {
                precios = context.PrecioCategoria.Where(w => w.vueloId == idVuelo).FirstOrDefault();
            }
            return mapper.MapToDto(precios);
        }

        public List<DtoVuelo> GetPreciosCategoriasByFecha(string numVuelo, DateTime fecha)
        {
            List<DtoVuelo> preciosCat = new List<DtoVuelo>();
            DateTime fecha7 = fecha.AddDays(7);
            List<string> estados = new List<string>();
            estados.Add("Programado");
            estados.Add("En Vuelo");
            estados.Add("Abordando");

            List<DtoVuelo> dtoVuelos = new List<DtoVuelo>().ToList();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = context.Vuelo.Where(w => w.numeroVuelo == numVuelo && estados.Contains(w.estado) && w.fechaSalida >= fecha && w.fechaSalida <= fecha7).OrderBy(o => o.fechaSalida).ToList();

                MVuelo mv = new MVuelo();

                foreach (Vuelo item in vuelos)
                {
                    dtoVuelos.Add(mv.MapToDto(item));
                }

            }

            for (int i = 0; i < 7; i++)
            {
                if (dtoVuelos.Any(a => a.fechaSalida.Date == fecha.Date.AddDays(i)))
                {

                }
                else
                {
                    DtoVuelo nuevoDto = new DtoVuelo();
                    nuevoDto.fechaSalida = fecha.Date.AddDays(i);
                    nuevoDto.numeroVuelo = numVuelo;
                    dtoVuelos.Add(nuevoDto);
                }

            }

            foreach (DtoVuelo item in dtoVuelos.ToList())
            {
                if (item.idVuelo != 0)
                {
                    item.precioCat.precioEconomy = GetPreciosByIdVuelo(item.idVuelo).precioEconomy;
                    item.precioCat.precioBusiness = GetPreciosByIdVuelo(item.idVuelo).precioBusiness;
                    item.precioCat.precioPremium = GetPreciosByIdVuelo(item.idVuelo).precioPremium;
                    item.precioCat.precioFirstClass = GetPreciosByIdVuelo(item.idVuelo).precioFirstClass;
                    preciosCat.Add(item);

                }
                else
                {
                    preciosCat.Add(item);
                }
            }

            var preciosVuelos = preciosCat.OrderBy(o => o.fechaSalida.Date).ToList().Take(7);
            return preciosVuelos.ToList();

        }


    }
}

