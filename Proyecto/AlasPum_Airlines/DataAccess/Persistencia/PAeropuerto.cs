using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PAeropuerto
    {
        public MAeropuerto mapper;

        public PAeropuerto()
        {
            this.mapper = new MAeropuerto();
        }

        public bool ValidarAero(string codigo)
        {
            bool existe = false;

            using (alasdbEntities context = new alasdbEntities())
            {
                existe = context.Aeropuerto.Any(a => a.abreviatura == codigo);

            }

            return existe;
        }

        public List<DtoAeropuerto> GetLugares(string locacion)
        {
            List<DtoAeropuerto> aerosEncontrados = new List<DtoAeropuerto>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Aeropuerto> aeros = context.Aeropuerto.Select(s => s).ToList();

                foreach (Aeropuerto item in aeros)
                {
                    if (item.ciudad.ToLower().Contains(locacion.ToLower()) || item.pais.ToLower().Contains(locacion.ToLower()))
                    {

                        DtoAeropuerto dto = mapper.MapToDto(item);
                        aerosEncontrados.Add(dto);
                    }
                    
                }
            }

            return aerosEncontrados;
        }
        

        public void AddAeropuerto(DtoAeropuerto dto)
        {
            Aeropuerto nuevoAero = mapper.MapToObj(dto);
            nuevoAero.disponible = true;
            HistoricoTasa tasaAero = new HistoricoTasa();
            tasaAero.codigoAirport = dto.abreviatura;
            tasaAero.tasaIntercontinental = dto.historico.tasaInter;
            tasaAero.tasaRegional = dto.historico.tasaRegional;
            tasaAero.fecha = DateTime.Now;

            using (alasdbEntities context = new alasdbEntities())
            {
                context.Aeropuerto.Add(nuevoAero);
                context.HistoricoTasa.Add(tasaAero);
                context.SaveChanges();
            }
        }

        public void DeshabilitarAero(string codigo)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Aeropuerto airport = context.Aeropuerto.FirstOrDefault(f => f.abreviatura == codigo);
                airport.disponible = false;
                context.SaveChanges();
            }

        }

        public void HabilitarAero(string codigo)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Aeropuerto airport = context.Aeropuerto.FirstOrDefault(f => f.abreviatura == codigo);
                airport.disponible = true;
                context.SaveChanges();
            }

        }


        public void UpdateTasa(string codigo, double regional, double inter)
        {

            using (alasdbEntities context = new alasdbEntities())
            {
                HistoricoTasa tasaAero = new HistoricoTasa();
                tasaAero.codigoAirport = codigo;
                tasaAero.tasaIntercontinental = inter;
                tasaAero.tasaRegional = regional;
                tasaAero.fecha = DateTime.Now;
                context.HistoricoTasa.Add(tasaAero);
                context.SaveChanges();
            }

        }

        public DtoHistoricoTasa GetTasaByCode(string codigo)
        {
            DtoHistoricoTasa tasaActual = new DtoHistoricoTasa();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<HistoricoTasa> ht = context.HistoricoTasa.Where(w => w.codigoAirport == codigo).OrderByDescending(o => o.idTasa).Take(1).ToList();
                foreach (HistoricoTasa item in ht)
                {
                    tasaActual.codigoAirport = codigo;
                    tasaActual.fecha = item.fecha;
                    tasaActual.tasaRegional = item.tasaRegional;
                    tasaActual.tasaInter = item.tasaIntercontinental;

                }
                context.SaveChanges();
            }

            return tasaActual;
        }



        public List<DtoAeropuerto> GetAeropuertos()
        {
            List<DtoAeropuerto> listaAeros = new List<DtoAeropuerto>();

            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (Aeropuerto item in context.Aeropuerto)
                {
                    DtoAeropuerto dto = mapper.MapToDto(item);
                    listaAeros.Add(dto);
                }
            }
            return listaAeros;
        }

        public DtoAeropuerto GetAeroByCodigo(string codigo)
        {
            DtoAeropuerto dto = new DtoAeropuerto();
            using (alasdbEntities context = new alasdbEntities())
            {
                dto = mapper.MapToDto(context.Aeropuerto.FirstOrDefault(f => f.abreviatura == codigo));
            }
            return dto;
        }


        
    }
}
