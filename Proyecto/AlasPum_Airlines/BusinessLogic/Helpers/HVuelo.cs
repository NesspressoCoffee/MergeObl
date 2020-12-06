using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HVuelo
    {
        private static HVuelo instance;

        private HVuelo()
        {

        }

        public static HVuelo getInstance()
        {
            if (instance == null)
            {
                instance = new HVuelo();
            }

            return instance;
        }
        
            public List<DtoVuelo> GetVuelosByFecha(string numVuelo, DateTime fecha)
        {
            PVuelo pv = new PVuelo();
            return pv.GetVuelosByFecha(numVuelo, fecha);
        }

        public DtoVuelo GetVueloCompleto(int id)
        {
            PVuelo pv = new PVuelo();
            return pv.GetVueloCompleto(id);
        }
            
        public List<DtoVuelo> VuelosDirectos_SoloIda(DtoVuelo dto)
        { 
            if(dto.destino.Length > 3) { 
            
            dto.origen = HAeropuerto.getInstance().ListarAeros().FirstOrDefault(f => f.ciudad == dto.origen || f.pais == dto.origen).abreviatura;
            dto.destino = HAeropuerto.getInstance().ListarAeros().FirstOrDefault(f => f.ciudad == dto.destino || f.pais == dto.destino).abreviatura;

            }
            
            PVuelo pv = new PVuelo();
            List<DtoVuelo> vuelosD = pv.VuelosDirectos_SoloIda(dto);

            return HAsiento.getInstance().GetAsientosDisponibles(dto, vuelosD);
        }

        public List<DtoVuelo> VuelosDirectos_IdaVuelta(DtoVuelo dto)
        {

            PVuelo pv = new PVuelo();
            List<DtoVuelo> vuelosD = pv.VuelosDirectos_IdaVuelta(dto);

            return HAsiento.getInstance().GetAsientosDisponibles(dto, vuelosD);
        }

        public List<DtoVuelo> VuelosEscala_Ida(DtoVuelo dto)
        {

            PVuelo pv = new PVuelo();
            List<DtoVuelo> vuelosD = pv.VuelosEscala_Ida(dto);

            return HAsiento.getInstance().GetAsientosDisponibles(dto, vuelosD);
        }

        public List<DtoVuelo> VuelosEscala_IdaVuelta(DtoVuelo dto)
        {

            PVuelo pv = new PVuelo();
            List<DtoVuelo> vuelosD = pv.VuelosEscala_Ida_Vuelta(dto);

            return HAsiento.getInstance().GetAsientosDisponibles(dto, vuelosD);
        }

        public List<DtoAeropuerto> GetVuelosByLugar(string locacion)
        {
            return HAeropuerto.getInstance().GetVuelosByLugar(locacion);
        }


        public void addVuelo(DtoVuelo dto, List<DtoFechaExclusiva> fechasExclusivas, List<string> dias)
        {
            List<DtoVuelo> listDto = new List<DtoVuelo>();
            while (true)
            {

                foreach (string dia in dias)
                {
                    if (dto.fechaComienzo.DayOfWeek.ToString() == dia)
                    {
                        if (fechasExclusivas != null && fechasExclusivas.Any(a => a.fecha == dto.fechaComienzo))
                        {
                            DtoFechaExclusiva fechaExclusiva = fechasExclusivas.Where(w => w.fecha == dto.fechaComienzo).FirstOrDefault();

                            if (fechaExclusiva.hora != null)
                            {
                                DtoVuelo nuevoVuelo = new DtoVuelo();
                                nuevoVuelo.avionId = dto.avionId;
                                nuevoVuelo.destino = dto.destino;
                                nuevoVuelo.estado = dto.estado;

                                nuevoVuelo.fechaLlegada = dto.fechaComienzo.AddHours(dto.horasVuelo + fechaExclusiva.hora.Value.Hour);
                                nuevoVuelo.fechaSalida = dto.fechaComienzo.AddHours(fechaExclusiva.hora.Value.Hour);

                                nuevoVuelo.horasVuelo = dto.horasVuelo;
                                nuevoVuelo.numeroVuelo = dto.numeroVuelo;
                                nuevoVuelo.origen = dto.origen;
                                nuevoVuelo.tipo = dto.tipo;
                                nuevoVuelo.visa = dto.visa;
                                listDto.Add(nuevoVuelo);
                            }
                        }
                        else
                        {
                            DtoVuelo nuevoVuelo = new DtoVuelo();
                            nuevoVuelo.avionId = dto.avionId;
                            nuevoVuelo.destino = dto.destino;
                            nuevoVuelo.estado = dto.estado;

                            nuevoVuelo.fechaLlegada = dto.fechaComienzo.AddHours(dto.horasVuelo + dto.horaSalida.Hour);
                            nuevoVuelo.fechaSalida = dto.fechaComienzo.AddHours(dto.horaSalida.Hour);

                            nuevoVuelo.horasVuelo = dto.horasVuelo;
                            nuevoVuelo.numeroVuelo = dto.numeroVuelo;
                            nuevoVuelo.origen = dto.origen;
                            nuevoVuelo.tipo = dto.tipo;
                            nuevoVuelo.visa = dto.visa;
                            listDto.Add(nuevoVuelo);
                        }

                    }

                }

                dto.fechaComienzo = dto.fechaComienzo.AddDays(1);

                if (dto.fechaComienzo == dto.fechaLimite)
                    break;

            }

            DtoAeropuerto origen = HAeropuerto.getInstance().GetAeroByCodigo(dto.origen);
            DtoAeropuerto destino = HAeropuerto.getInstance().GetAeroByCodigo(dto.destino);

            foreach (DtoVuelo item in listDto)
            {
                if (origen.pais == destino.pais)
                {
                    item.tipo = "Nacional";
                }
                else if (origen.pais != destino.pais && origen.continente == destino.continente)
                {
                    item.tipo = "Regional";
                }
                else if (origen.continente != destino.continente)
                {
                    item.tipo = "Intercontinental";
                }
                item.estado = "Programado";
            }

            PVuelo pv = new PVuelo();
            pv.addVuelo(listDto);
        }

        #region
            public List<DtoAsiento> GetAsientosDisponibles(int idVuelo, int avionId)
        {
            return HPasaje.getInstance().GetAsientosDisponibles(idVuelo, avionId);
        }

        #endregion

    }

}



