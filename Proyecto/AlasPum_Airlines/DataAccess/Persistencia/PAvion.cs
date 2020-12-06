using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PAvion
    {
        public MAvion mapper;

        public PAvion()
        {
            this.mapper = new MAvion();
        }

        public void addAvion(DtoAvion dto, out int idAvion)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Avion nuevoAvion = mapper.MapToObj(dto);
                context.Avion.Add(nuevoAvion);
                context.SaveChanges();
                idAvion = nuevoAvion.idAvion;
            }
        }

        public void modifyAvion(DtoAvion dto, out int idAvion)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Avion avion = context.Avion.Where(w => w.idAvion == dto.idAvion).FirstOrDefault();
                avion.cantAsientos = dto.cantAsientos;
                avion.fechaIngreso = dto.fechaIngreso;
                avion.horasTotales = dto.horasTotales;
                avion.modelo = dto.modelo;

                context.SaveChanges();
                idAvion = avion.idAvion;
            }
        }

        public int deleteAvion(int idAvion)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                if (context.Vuelo.Any(a => a.avionId == idAvion && a.estado != "Cancelado" && a.estado != "Finalizado") == false)
                {
                    Avion avion = context.Avion.Where(w => w.idAvion == idAvion).FirstOrDefault();
                    avion.disponible = false;
                    context.SaveChanges();
                    return 0;
                }
                else return 1;
            }
        }

        public List<DtoAvion> getColAvion()
        {
            List<DtoAvion> col = new List<DtoAvion>();

            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (Avion item in context.Avion)
                {
                    if (item.disponible == true)
                    {
                        DtoAvion dto = mapper.MapToDto(item);
                        col.Add(dto);
                    }
                }
            }
            return col;
        }

        public DtoAvion getAvion(int idAvion)
        {
            DtoAvion dto = new DtoAvion();
            using (alasdbEntities context = new alasdbEntities())
            {
                dto = mapper.MapToDto(context.Avion.Where(w => w.idAvion == idAvion).FirstOrDefault());
            }
            return dto;
        }

        public bool ValidarExistenciaAvion(int avionId)
        {
            bool ok = false;
            using (alasdbEntities context = new alasdbEntities())
            {
                ok = context.Avion.Any(a => a.idAvion == avionId && a.disponible == true);
            }
            return ok;
        }

        public bool ValidarDisponibilidadAvion(int idAvion, DateTime salida, DateTime llegada, string destino, string origen, int idVuelo)
        {
            bool ok = false;
            using (alasdbEntities context = new alasdbEntities())
            {
                List<Vuelo> vuelos = context.Vuelo.Where(w => w.avionId == idAvion && w.estado != "Cancelado").ToList();

                foreach (Vuelo vuelo in vuelos)
                {
                    while (ok == false)
                    {
                        if (vuelo.idVuelo != idVuelo)
                        {
                            if (vuelo.fechaSalida.AddHours(-2) < salida && vuelo.fechaLlegada.AddHours(2) > salida)
                            {
                                ok = true;
                            }
                            else if (vuelo.fechaSalida.AddHours(-2) < llegada && vuelo.fechaLlegada.AddHours(2) > llegada)
                            {
                                ok = true;
                            }
                            else if (llegada.Date == vuelo.fechaSalida.Date && vuelo.fechaSalida.Date == salida.Date && salida.Date == vuelo.fechaLlegada.Date)
                            {
                                if (llegada < vuelo.fechaSalida)
                                {
                                    if (destino != vuelo.origen)
                                        ok = true;
                                }
                                else if (salida > vuelo.fechaLlegada)
                                {
                                    if (origen != vuelo.destino)
                                        ok = true;
                                }
                            }
                            else if (llegada.Date == vuelo.fechaSalida.Date)
                            {
                                if (destino == vuelo.origen) { 
                                    ok = false;
                                    break;
                                }
                                else
                                    ok = true;
                            }
                            else if (salida.Date == vuelo.fechaLlegada.Date)
                            {
                                if (origen == vuelo.destino) { 
                                    ok = false;
                                    break;
                                }
                                else
                                    ok = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                    }
                }
            }
            return ok;
        }
    }
}
