using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PReportes
    {
        public MVuelo mapperVuelo;

        public PReportes()
        {
            mapperVuelo = new MVuelo();
        }
        public float porcentajeDeOcupacionByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<float> promedios = new List<float>();
            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = new List<Vuelo>(); /*context.Vuelo.Where(w => w.fechaSalida.Date >= fechaDesde && w.fechaSalida.Date <= fechaHasta).ToList();*/
                foreach (Vuelo v in context.Vuelo)
                {
                    if(v.fechaSalida.Date >= fechaDesde && v.fechaSalida.Date <= fechaHasta)
                    {
                        vuelos.Add(v);
                    }
                }
                foreach (Vuelo vuelo in vuelos)
                {
                    int cantPasajes = context.Pasaje.Count(c => c.vueloId == vuelo.idVuelo);
                    int cantAsientos = context.Avion.Where(w => w.idAvion == vuelo.avionId).FirstOrDefault().cantAsientos;

                    float promedio = ((cantPasajes * 100) / cantAsientos);
                    promedios.Add(promedio);
                }

            }
            return (promedios.Sum() / promedios.Count());
        }

        public List<DtoReporteAsientosVacios> masMenosAsientosOcupadosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<DtoReporteAsientosVacios> listDto = new List<DtoReporteAsientosVacios>();
            DtoReporteAsientosVacios mas = new DtoReporteAsientosVacios();
            DtoReporteAsientosVacios menos = new DtoReporteAsientosVacios();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = new List<Vuelo>(); /*context.Vuelo.Where(w => w.fechaSalida.Date >= fechaDesde && w.fechaSalida.Date <= fechaHasta).ToList();*/
                foreach (Vuelo v in context.Vuelo)
                {
                    if (v.fechaSalida.Date >= fechaDesde && v.fechaSalida.Date <= fechaHasta)
                    {
                        vuelos.Add(v);
                    }
                }
                foreach (Vuelo vuelo in vuelos)
                {
                    int cantPasajes = context.Pasaje.Count(c => c.vueloId == vuelo.idVuelo);
                    int cantAsientos = context.Avion.Where(w => w.idAvion == vuelo.avionId).FirstOrDefault().cantAsientos;

                    int asientosVacios = cantAsientos - cantPasajes;

                    if (mas.vuelo == null || mas.asientosVacios < asientosVacios)
                    {
                        mas.vuelo = mapperVuelo.MapToDto(vuelo);
                        mas.asientosVacios = asientosVacios;

                    }
                    if (menos.vuelo == null || menos.asientosVacios > asientosVacios)
                    {

                        menos.vuelo = mapperVuelo.MapToDto(vuelo);
                        menos.asientosVacios = asientosVacios;
                    }
                }
            }
            listDto.Add(mas);
            listDto.Add(menos);
            return listDto;
        }

        public DtoCliente clienteConMasComprasByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<DtoReporteCliente> colRep = new List<DtoReporteCliente>();
            Compra cliente = new Compra();
            using (alasdbEntities context = new alasdbEntities())
            {
                List<Compra> compras = context.Compra.Where(w => w.fechaCompra >= fechaDesde && w.fechaCompra <= fechaHasta).ToList();
                List<string> docClientes = compras.Select(s => s.documentoTitular).Distinct().ToList();

                foreach (Compra compra in compras)
                {
                    foreach (string doc in docClientes)
                    {
                        if (compra.documentoTitular == doc)
                        {
                            if (colRep.Any(a => a.documento == doc))
                            {
                                colRep.Where(w => w.documento == doc).FirstOrDefault().cantidad++;
                            }
                            else
                            {
                                DtoReporteCliente dto = new DtoReporteCliente();
                                dto.documento = doc;
                                dto.cantidad = 1;
                                colRep.Add(dto);
                            }
                        }
                    }
                }

                string documento = colRep.OrderByDescending(o => o.cantidad).Select(s => s.documento).FirstOrDefault();
                cliente = context.Compra.Where(w => w.documentoTitular == documento).FirstOrDefault();
            }
            DtoCliente dtoCli = null;
            if (cliente != null)
            {
                 dtoCli = new DtoCliente();
                dtoCli.apellido = cliente.apellidoTitular;
                dtoCli.nombre = cliente.nombreTitular;
                dtoCli.documento = cliente.documentoTitular;
                dtoCli.email = cliente.email;
            }
            return dtoCli;
        }

    }
}
