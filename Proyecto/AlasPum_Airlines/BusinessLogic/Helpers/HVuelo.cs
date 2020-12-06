using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        public int addVuelo(DtoVuelo dto, List<DtoFechaExclusiva> fechasExclusivas, List<string> dias)
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

                                if (HAvion.getInstance().ValidarDisponibilidadAvion(dto.avionId, nuevoVuelo.fechaSalida, nuevoVuelo.fechaLlegada, dto.destino, dto.origen, -1))
                                {
                                    return 1;
                                }

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

                            if (HAvion.getInstance().ValidarDisponibilidadAvion(dto.avionId, nuevoVuelo.fechaSalida, nuevoVuelo.fechaLlegada, dto.destino, dto.origen, -1))
                            {
                                return 1;
                            }

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

            List<int> listIdVuelo = pv.getIdsVueloByNumero(dto.numeroVuelo);
            List<DtoPrecioCategoria> listDtoPrecios = new List<DtoPrecioCategoria>();
            foreach (int id in listIdVuelo)
            {
                DtoPrecioCategoria dtoPrecios = new DtoPrecioCategoria();
                dtoPrecios.precioBusiness = dto.precioBusiness;
                dtoPrecios.precioEconomy = dto.precioEconomy;
                dtoPrecios.precioFirstClass = dto.precioFirstClass;
                dtoPrecios.precioPremium = dto.precioPremium;
                dtoPrecios.vueloId = id;
                listDtoPrecios.Add(dtoPrecios);
            }

            HPrecioCategoria.getInstance().addPrecioCategoria(listDtoPrecios);

            return 0;
        }

        public void deleteVuelo(int idVuelo)
        {
            PVuelo pv = new PVuelo();
            pv.deleteVuelo(idVuelo);
        }

        public List<DtoVuelo> getColVuelo()
        {
            PVuelo pa = new PVuelo();
            return pa.getColVuelo();
        }

        public int modifyVuelo(DtoVuelo dto)
        {
            PVuelo pv = new PVuelo();

            dto.fechaSalida = dto.fechaSalida.Date.AddHours(dto.horaSalida.Hour);
            dto.fechaLlegada = dto.fechaSalida.AddHours(dto.horasVuelo);

            if (HAvion.getInstance().ValidarDisponibilidadAvion(dto.avionId, dto.fechaSalida, dto.fechaLlegada, dto.destino, dto.origen, dto.idVuelo))
            {
                return 1;
            }
            DtoAeropuerto origen = HAeropuerto.getInstance().GetAeroByCodigo(dto.origen);
            DtoAeropuerto destino = HAeropuerto.getInstance().GetAeroByCodigo(dto.destino);

            if (origen.pais == destino.pais)
            {
                dto.tipo = "Nacional";
            }
            else if (origen.pais != destino.pais && origen.continente == destino.continente)
            {
                dto.tipo = "Regional";
            }
            else if (origen.continente != destino.continente)
            {
                dto.tipo = "Intercontinental";
            }
            pv.modifyVuelo(dto);

            DtoPrecioCategoria dtoPrecios = new DtoPrecioCategoria();
            dtoPrecios.vueloId = dto.idVuelo;
            dtoPrecios.precioBusiness = dto.precioBusiness;
            dtoPrecios.precioEconomy = dto.precioEconomy;
            dtoPrecios.precioFirstClass = dto.precioFirstClass;
            dtoPrecios.precioPremium = dto.precioPremium;

            HPrecioCategoria.getInstance().modifyPrecioCategoria(dtoPrecios);

            return 0;
        }
        public DtoVuelo getVuelo(int idVuelo)
        {
            
            PVuelo pv = new PVuelo();
            DtoVuelo dto = pv.getVuelo(idVuelo);
            DtoPrecioCategoria precios = HPrecioCategoria.getInstance().GetPreciosByIdVuelo(idVuelo);
            dto.precioBusiness = precios.precioBusiness;
            dto.precioEconomy = precios.precioEconomy;
            dto.precioFirstClass = precios.precioFirstClass;
            dto.precioPremium = precios.precioPremium;
            dto.horaSalida = dto.fechaSalida;
            return dto;
        }

        public List<SelectListItem> getListEstado()
        {
            List<SelectListItem> listEstado = new List<SelectListItem>();

            SelectListItem opcionProgramado = new SelectListItem();
            opcionProgramado.Text = "Programado";
            opcionProgramado.Value = "Programado";
            listEstado.Add(opcionProgramado);

            SelectListItem opcionAbordando = new SelectListItem();
            opcionAbordando.Text = "Abordando";
            opcionAbordando.Value = "Abordando";
            listEstado.Add(opcionAbordando);

            SelectListItem opcionEnVuelo = new SelectListItem();
            opcionEnVuelo.Text = "En Vuelo";
            opcionEnVuelo.Value = "En Vuelo";
            listEstado.Add(opcionEnVuelo);

            SelectListItem opcionFinalizado = new SelectListItem();
            opcionFinalizado.Text = "Finalizado";
            opcionFinalizado.Value = "Finalizado";
            listEstado.Add(opcionFinalizado);

            SelectListItem opcionCancelado = new SelectListItem();
            opcionCancelado.Text = "Cancelado";
            opcionCancelado.Value = "Cancelado";
            listEstado.Add(opcionCancelado);

            return listEstado;
        }

        public List<SelectListItem> getListVisa()
        {
            List<SelectListItem> listVisa = new List<SelectListItem>();


            SelectListItem opcionSi = new SelectListItem();
            opcionSi.Text = "Si";
            opcionSi.Value = "Si";
            listVisa.Add(opcionSi);
            SelectListItem opcionNo = new SelectListItem();
            opcionNo.Text = "No";
            opcionNo.Value = "No";
            listVisa.Add(opcionNo);

            return listVisa;
        }
        public bool validarNumeroVuelo(string num)
        {
            PVuelo pv = new PVuelo();
            return pv.validarNumeroVuelo(num);
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
            dto.origen = HAeropuerto.getInstance().ListarAeros().FirstOrDefault(f => f.ciudad == dto.origen || f.pais == dto.origen).abreviatura;
            dto.destino = HAeropuerto.getInstance().ListarAeros().FirstOrDefault(f => f.ciudad == dto.destino || f.pais == dto.destino).abreviatura;

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

    }
}
