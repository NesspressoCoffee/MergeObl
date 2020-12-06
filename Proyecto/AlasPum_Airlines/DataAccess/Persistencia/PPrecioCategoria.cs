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

        public void addPrecioCategoria(List<DtoPrecioCategoria> listDto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (DtoPrecioCategoria dto in listDto)
                {
                    context.PrecioCategoria.Add(mapper.MapToObj(dto));
                }
                context.SaveChanges();
            }
        }

        public void modifyPrecioCategoria(DtoPrecioCategoria dto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                PrecioCategoria obj = context.PrecioCategoria.Where(w =>  w.vueloId == dto.vueloId).FirstOrDefault();
                obj.precioBusiness = dto.precioBusiness;
                obj.precioEconomy = dto.precioEconomy;
                obj.precioFirstClass = dto.precioFirstClass;
                obj.precioPremium = dto.precioPremium;

                context.SaveChanges();
            }
        }

        public List<DtoVuelo> GetPreciosCategoriasByFecha(string numVuelo, DateTime fecha)
        {
            List<DtoVuelo> preciosCat = new List<DtoVuelo>();
            DateTime fecha7 = fecha.AddDays(7);

            List<DtoVuelo> dtoVuelos = new List<DtoVuelo>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = context.Vuelo.Where(w => w.numeroVuelo == numVuelo && w.estado != "Cancelado" && (w.fechaSalida >= fecha || w.fechaSalida <= fecha7)).OrderBy(o => o.fechaSalida).Take(7).ToList();

                MVuelo mv = new MVuelo();

                foreach (Vuelo item in vuelos)
                {
                    dtoVuelos.Add(mv.MapToDto(item));
                }

            }

            List<DtoVuelo> nuevoVuelo = new List<DtoVuelo>();

            for (int i = 0; i < 7; i++)
            {

                DtoVuelo nuevoDto = new DtoVuelo();
                nuevoDto.fechaSalida = fecha.Date.AddDays(i);
                nuevoDto.numeroVuelo = numVuelo;
                nuevoVuelo.Add(nuevoDto);
            }

            IEnumerable<DtoVuelo> firstNotSecond = dtoVuelos.Except(nuevoVuelo).ToList().Union(nuevoVuelo.Except(dtoVuelos).ToList());


            foreach (DtoVuelo item in firstNotSecond.ToList())
            {
                if (item.idVuelo != 0)
                {
                    item.precioCat.precioEconomy = GetPreciosByIdVuelo(item.idVuelo).precioEconomy;
                    item.precioCat.precioBusiness = GetPreciosByIdVuelo(item.idVuelo).precioBusiness;
                    item.precioCat.precioPremium = GetPreciosByIdVuelo(item.idVuelo).precioPremium;
                    item.precioCat.precioFirstClass = GetPreciosByIdVuelo(item.idVuelo).precioFirstClass;
                    preciosCat.Add(item);


                }
                else if (item.fechaSalida.Date != preciosCat[0].fechaSalida.Date && item.fechaSalida.Date != preciosCat[1].fechaSalida.Date)
                {

                    preciosCat.Add(item);
                }
            }

            var precios = preciosCat.OrderBy(o => o.fechaSalida.Date).ToList();


            return precios.ToList();


        }
    }
}