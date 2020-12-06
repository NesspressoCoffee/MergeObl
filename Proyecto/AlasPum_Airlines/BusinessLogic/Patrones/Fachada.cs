using BusinessLogic.Helpers;
using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLogic.Patrones
{
    public class Fachada
    {
        private HAeropuerto haeropuerto;
        private HAsiento hasiento;
        private HAvion havion;
        private HEmpleado hempleado;
        private HVuelo hvuelo;
        private HPrecioCategoria hprecio;
        private HReporte hreporte;

        public Fachada()
        {
            haeropuerto = HAeropuerto.getInstance();
            hasiento = HAsiento.getInstance();
            havion = HAvion.getInstance();
            hempleado = HEmpleado.getInstance();
            hvuelo = HVuelo.getInstance();
            hprecio = HPrecioCategoria.getInstance();
            hreporte = HReporte.getInstance();
        }

        #region - Avion -
        public void addAvion(DtoAvion dto, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            havion.addAvion(dto, listCategoria, listDisponibilidad);
        }
        public int deleteAvion(int id)
        {
            return havion.deleteAvion(id);
        }

        public void modifyAvion(DtoAvion dto, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            havion.modifyAvion(dto, listCategoria, listDisponibilidad);
        }

        public DtoAvion getAvion(int id)
        {
            return havion.getAvion(id);
        }
        public List<DtoAvion> getColAvion()
        {
            return havion.getColAvion();
        }

        public bool ValidarExistenciaAvion(int avionId)
        {
            return havion.ValidarExistenciaAvion(avionId);
        }
        #endregion

        #region - Asiento -
        public List<DtoCategoriaAsiento> getColCategoriaAsiento(int id)
        {
            return hasiento.getColCategoriaAsiento(id);
        }

        public List<DtoDisponibilidadAsiento> getColDisponibilidadAsiento(int id)
        {
            return hasiento.getColDisponibilidadAsiento(id);
        }

        public bool validarCantidadDeAsientos(int cantAsientos, List<DtoCategoriaAsiento> listCategoria)
        {
            return hasiento.validarCantidadDeAsientos(cantAsientos, listCategoria);
        }

        public int validarGrupoDeAsientos(int cantAsientos, List<DtoCategoriaAsiento> listCategoria, int? desde, int? hasta)
        {
            return hasiento.validarGrupoDeAsientos(cantAsientos, listCategoria, desde, hasta);
        }

        public bool validarCantidadDeAsientos(int cantAsientos, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            return hasiento.validarCantidadDeAsientos(cantAsientos, listDisponibilidad);
        }

        public int validarGrupoDeAsientos(int cantAsientos, List<DtoDisponibilidadAsiento> listDisponibilidad, int? desde, int? hasta)
        {
            return hasiento.validarGrupoDeAsientos(cantAsientos, listDisponibilidad, desde, hasta);
        }
        #endregion

        #region - Aeropuerto - 
        public List<DtoAeropuerto> ListarAeros()
        {
            return haeropuerto.ListarAeros();
        }

        public DtoHistoricoTasa GetTasaByCode(string codigo)
        {
            return haeropuerto.GetTasaByCode(codigo);
        }

        public void modificarTasa(string codigoAirport, double tasaRegional, double tasaInter, string user)
        {
            haeropuerto.modificarTasa(codigoAirport, tasaRegional, tasaInter, user);
        }

        public bool ValidarCodigo(string abreviatura)
        {
            return haeropuerto.ValidarCodigo(abreviatura);
        }

        public void AddAeropuerto(DtoAeropuerto dto, string user)
        {
            haeropuerto.AddAeropuerto(dto, user);
        }

        public void DeshabilitarAirport(string codigo, string user)
        {
            haeropuerto.DeshabilitarAirport(codigo, user);
        }

        public void HabilitarAirport(string codigo, string user)
        {
            haeropuerto.HabilitarAirport(codigo, user);
        }
        #endregion

        #region - Login -
        public bool LoginEmpleado(DtoLogin dto)
        {
            return hempleado.LoginEmpleado(dto);
        }

        public DtoEmpleado GetEmpleadoByDoc(string doc)
        {
            return hempleado.GetEmpleadoByDoc(doc);
        }
        #endregion

        #region - Vuelo -
        public List<SelectListItem> getListEstado()
        {
            return hvuelo.getListEstado();
        }

        public List<SelectListItem> getListVisa()
        {
            return hvuelo.getListVisa();
        }

        public List<DtoVuelo> getColVuelo()
        {
            return hvuelo.getColVuelo();
        }

        public DtoVuelo getVuelo(int id)
        {
            return hvuelo.getVuelo(id);
        }

        public int addVuelo(DtoVuelo dto, List<DtoFechaExclusiva> listFechas, List<string> listDias)
        {
            return hvuelo.addVuelo(dto, listFechas, listDias);
        }

        public int modifyVuelo(DtoVuelo dto)
        {
            return hvuelo.modifyVuelo(dto);
        }

        public void deleteVuelo(int id)
        {
            hvuelo.deleteVuelo(id);
        }

        public bool validarNumeroVuelo(string num)
        {
            return hvuelo.validarNumeroVuelo(num);
        }

        public List<DtoAeropuerto> GetVuelosByLugar(string term)
        {
            return hvuelo.GetVuelosByLugar(term);
        }

        public List<DtoVuelo> VuelosEscala_IdaVuelta(DtoVuelo dto)
        {
            return hvuelo.VuelosEscala_IdaVuelta(dto);
        }
        
        public List<DtoVuelo> VuelosDirectos_IdaVuelta(DtoVuelo dto)
        {
            return hvuelo.VuelosEscala_IdaVuelta(dto);
        }

        public List<DtoVuelo> VuelosEscala_Ida(DtoVuelo dto)
        {
            return hvuelo.VuelosEscala_IdaVuelta(dto);
        }

        public List<DtoVuelo> VuelosDirectos_SoloIda(DtoVuelo dto)
        {
            return hvuelo.VuelosEscala_IdaVuelta(dto);
        }

        public DtoVuelo GetVueloCompleto(int id)
        {
            return hvuelo.GetVueloCompleto(id);
        }
        #endregion

        #region - Empleado -
        public List<DtoEmpleado> ListarEmpleados()
        {
            return hempleado.ListarEmpleados();
        }

        public void AddEmpleado(DtoEmpleado dto, string docEmpleado)
        {
            hempleado.AddEmpleado(dto, docEmpleado);
        }

        public List<DtoEmpleado> GetEmpleadosByData(string data)
        {
            return hempleado.GetEmpleadosByData(data);
        }

        public void BajaEmpleado(string usuario)
        {
            hempleado.DeleteEmpleado(usuario);
        }

        public bool ValidarUser(string documento)
        {
            return hempleado.ValidarUser(documento);
        }
        #endregion

        #region - Precio Categoria -
        public List<DtoVuelo> GetPreciosCategoriasByFecha(string numVuelo, DateTime date)
        {
            return hprecio.GetPreciosCategoriasByFecha(numVuelo, date);
        }
        #endregion

        #region - Reportes -
        public float porcentajeDeOcupacionByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return hreporte.porcentajeDeOcupacionByFechas(fechaDesde, fechaHasta);
        }

        public List<DtoReporteAsientosVacios> masMenosAsientosOcupadosByFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            return hreporte.masMenosAsientosOcupadosByFecha(fechaDesde, fechaHasta);
        }

        public DtoCliente clienteConMasComprasByFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return hreporte.clienteConMasComprasByFechas(fechaDesde, fechaHasta);
        }
        #endregion
    }
}
