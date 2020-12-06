using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PVuelo
    {
        public MVuelo mapper;

        public PVuelo()
        {
            this.mapper = new MVuelo();
        }

        public void addVuelo(List<DtoVuelo> listDto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (DtoVuelo dto in listDto)
                {
                    context.Vuelo.Add(mapper.MapToObj(dto));
                }
                context.SaveChanges();
            }
        }
        public void deleteVuelo(int idVuelo)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Vuelo vuelo = context.Vuelo.FirstOrDefault(f => f.idVuelo == idVuelo);
                vuelo.estado = "Cancelado";
                context.SaveChanges();
            }
        }
        public List<DtoVuelo> getColVuelo()
        {
            List<DtoVuelo> col = new List<DtoVuelo>();

            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (Vuelo item in context.Vuelo)
                {
                    DtoVuelo dto = mapper.MapToDto(item);
                    col.Add(dto);
                }
            }
            return col;
        }
        public DtoVuelo getVuelo(int idVuelo)
        {
            Vuelo vuelo = new Vuelo();
            using (alasdbEntities context = new alasdbEntities())
            {
                vuelo = context.Vuelo.Where(w => w.idVuelo == idVuelo).FirstOrDefault();
                
            }
            return mapper.MapToDto(vuelo);
        }

        public void modifyVuelo(DtoVuelo dto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Vuelo vuelo = context.Vuelo.Where(w => w.idVuelo == dto.idVuelo).FirstOrDefault();
                vuelo.avionId = dto.avionId;
                vuelo.destino = dto.destino;
                vuelo.estado = dto.estado;
                vuelo.fechaLlegada = dto.fechaLlegada;
                vuelo.fechaSalida = dto.fechaSalida;
                vuelo.horasVuelo = dto.horasVuelo;
                vuelo.numeroVuelo = dto.numeroVuelo;
                vuelo.origen = dto.origen;
                vuelo.tipo = dto.tipo;
                vuelo.visa = dto.visa;
                context.SaveChanges();
            }
        }

        public bool validarNumeroVuelo(string num)
        {
            bool ok;
            using (alasdbEntities context = new alasdbEntities())
            {
                ok = context.Vuelo.Any(a => a.numeroVuelo == num && a.estado != "Finalizado" && a.estado != "Cancelado");
            }
            if(ok == true)
            {
                return false;
            }
            return true;
        }

        public List<int> getIdsVueloByNumero(string num)
        {
            List<int> listId = new List<int>();
            using (alasdbEntities context = new alasdbEntities())
            {
                listId = context.Vuelo.Where(w => w.numeroVuelo == num && w.estado != "Finalizado" && w.estado != "Cancelado").Select(s => s.idVuelo).ToList();
            }
            return listId;
        }
        public List<DtoVuelo> VuelosDirectos_SoloIda(DtoVuelo dto)
        {
            List<DtoVuelo> vuelosDirectos = new List<DtoVuelo>();
            PPrecioCategoria pc = new PPrecioCategoria();

            using (alasdbEntities context = new alasdbEntities())
            {

                List<Vuelo> vuelosSoloIda = context.Vuelo.Where(w => w.origen == dto.origen && w.destino == dto.destino && w.estado == "Programado").ToList();
                foreach (Vuelo item in vuelosSoloIda)
                {

                    if (item.fechaSalida.Date == dto.fechaSalida.Date)
                    {
                        vuelosDirectos.Add(mapper.MapToDto(item));
                    }
                }
                foreach (DtoVuelo item in vuelosDirectos)
                {
                    item.precioCat.precioEconomy = context.PrecioCategoria.FirstOrDefault(s => s.vueloId == item.idVuelo).precioEconomy;
                    item.precioCat.precioBusiness = context.PrecioCategoria.FirstOrDefault(s => s.vueloId == item.idVuelo).precioBusiness;
                    item.precioCat.precioFirstClass = context.PrecioCategoria.FirstOrDefault(s => s.vueloId == item.idVuelo).precioFirstClass;
                    item.precioCat.precioPremium = context.PrecioCategoria.FirstOrDefault(s => s.vueloId == item.idVuelo).precioPremium;
                }
            }

            return vuelosDirectos;
        }

        public List<DtoVuelo> VuelosDirectos_IdaVuelta(DtoVuelo dto)
        {
            List<DtoVuelo> vuelosDirectos = new List<DtoVuelo>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelosIda = new List<Vuelo>();
                List<Vuelo> vuelosVuelta = new List<Vuelo>();

                vuelosIda = context.Vuelo.Where(w => w.origen == dto.origen && w.destino == dto.destino && w.estado == "Programado").ToList();
                vuelosVuelta = context.Vuelo.Where(w => w.origen == dto.destino && w.destino == dto.origen && w.estado == "Programado").ToList();

                foreach (Vuelo item in vuelosIda)
                {
                    if (item.fechaSalida.Date == dto.fechaSalida.Date && item.fechaLlegada.Date == dto.fechaLlegada.Date)
                    {
                        vuelosDirectos.Add(mapper.MapToDto(item));
                    }
                }
                foreach (Vuelo item in vuelosVuelta)
                {
                    if (item.fechaSalida.Date == dto.fechaLlegada.Date)
                    {
                        vuelosDirectos.Add(mapper.MapToDto(item));
                    }
                }
            }

            return vuelosDirectos;
        }

        public List<DtoVuelo> VuelosEscala_Ida(DtoVuelo dto)
        {
            List<DtoVuelo> escalaSoloIda = new List<DtoVuelo>();


            using (alasdbEntities context = new alasdbEntities())
            {

                List<Vuelo> todosOrigenIda = context.Vuelo.Where(w => w.origen == dto.origen && w.estado == "Programado").ToList();
                List<Vuelo> todosDestinoIda = context.Vuelo.Where(w => w.destino == dto.destino && w.estado == "Programado").ToList();

                foreach (Vuelo item in todosOrigenIda)
                {
                    foreach (Vuelo aux in todosDestinoIda)
                    {

                        if (item.fechaSalida.Date == dto.fechaSalida.Date && item.destino == aux.origen && (item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddMinutes(-30) && item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddHours(20)))
                        {
                            if (!escalaSoloIda.Contains(mapper.MapToDto(item)))
                            {
                                escalaSoloIda.Add(mapper.MapToDto(item));
                            }

                            escalaSoloIda.Add(mapper.MapToDto(aux));
                        }

                    }
                }
            }

            return escalaSoloIda;
        }

        public List<DtoVuelo> VuelosEscala_Ida_Vuelta(DtoVuelo dto)
        {
            List<DtoVuelo> vuelosEscala = new List<DtoVuelo>();


            using (alasdbEntities context = new alasdbEntities())
            {

                List<Vuelo> todosOrigenIda = context.Vuelo.Where(w => w.origen == dto.origen && w.estado == "Programado").ToList();
                List<Vuelo> todosDestinoIda = context.Vuelo.Where(w => w.destino == dto.destino && w.estado == "Programado").ToList();

                foreach (Vuelo item in todosOrigenIda)
                {
                    foreach (Vuelo aux in todosDestinoIda)
                    {

                        if (item.fechaSalida.Date == dto.fechaSalida.Date && item.destino == aux.origen && (item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddMinutes(-30) && item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddHours(20)))
                        {
                            if (!vuelosEscala.Contains(mapper.MapToDto(item)))
                            {
                                vuelosEscala.Add(mapper.MapToDto(item));
                            }

                            vuelosEscala.Add(mapper.MapToDto(aux));
                        }

                    }
                }

                List<Vuelo> todosOrigenVuelta = context.Vuelo.Where(w => w.origen == dto.destino && w.estado == "Programado").ToList();
                List<Vuelo> todosDestinoVuelta = context.Vuelo.Where(w => w.destino == dto.origen && w.estado == "Programado").ToList();

                foreach (Vuelo item in todosOrigenVuelta)
                {
                    foreach (Vuelo aux in todosDestinoVuelta)
                    {

                        if (item.fechaSalida.Date == dto.fechaLlegada.Date && item.destino == aux.origen && (item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddMinutes(-30) && item.fechaSalida.AddHours(item.horasVuelo) <= aux.fechaSalida.AddHours(20)))
                        {
                            if (!vuelosEscala.Contains(mapper.MapToDto(item)))
                            {
                                vuelosEscala.Add(mapper.MapToDto(item));
                            }

                            vuelosEscala.Add(mapper.MapToDto(aux));
                        }

                    }
                }
            }


            return vuelosEscala;

        }

        public DtoVuelo GetVueloCompleto(int idVuelo)
        {
            DtoVuelo vueloCompleto = new DtoVuelo();

            using (alasdbEntities context = new alasdbEntities())
            {
                vueloCompleto = mapper.MapToDto(context.Vuelo.FirstOrDefault(f => f.idVuelo == idVuelo));
            }

            return vueloCompleto;
        }

        public List<DtoVuelo> GetVuelosByFecha(string num, DateTime fecha)
        {
            List<DtoVuelo> colVuelos = new List<DtoVuelo>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = context.Vuelo.Where(w => w.numeroVuelo == num).ToList();

                foreach (Vuelo item in vuelos)
                {
                    if (item.fechaSalida >= fecha.AddDays(-3) || item.fechaSalida <= fecha.AddDays(3))
                    {

                        colVuelos.Add(mapper.MapToDto(item));
                    }
                }

                colVuelos.OrderBy(o => o.fechaSalida);
            }

            return colVuelos;
        }

    }
}
