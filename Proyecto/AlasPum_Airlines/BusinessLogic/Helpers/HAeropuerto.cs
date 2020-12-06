using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HAeropuerto
    {
        private static HAeropuerto instance;

        private HAeropuerto()
        {

        }

        public static HAeropuerto getInstance()
        {
            if (instance == null)
            {
                instance = new HAeropuerto();
            }

            return instance;
        }


        public bool AddAeropuerto(DtoAeropuerto dto, string user)
        {
            PAeropuerto pa = new PAeropuerto();

            bool existe = pa.ValidarAero(dto.abreviatura);
            if (!existe)
            {
                pa.AddAeropuerto(dto);
            }

            return existe;
        }

        public List<DtoAeropuerto> GetVuelosByLugar(string locacion)
        {
            PAeropuerto pa = new PAeropuerto();

            List<DtoAeropuerto> locaciones = pa.GetLugares(locacion);

            return locaciones;

        }

        public List<DtoAeropuerto> ListarAeros()
        {
            PAeropuerto pa = new PAeropuerto();

            List<DtoAeropuerto> col = pa.GetAeropuertos();
            return col;
        }


        public DtoAeropuerto GetAeroByCodigo(string codigo)
        {
            PAeropuerto pa = new PAeropuerto();
            DtoAeropuerto dto = pa.GetAeroByCodigo(codigo);
            return dto;
        }

        public void modificarTasa(string codigoAirport, double tasaRegional, double tasaInter, string user)
        {
            PAeropuerto pa = new PAeropuerto();
            DtoHistoricoTasa dto = new DtoHistoricoTasa();
            dto.codigoAirport = codigoAirport;
            dto.tasaRegional = tasaRegional;
            dto.tasaInter = tasaInter;
            pa.UpdateTasa(dto);
        }

        public DtoHistoricoTasa GetTasaByCode(string codigo)
        {
            PAeropuerto pa = new PAeropuerto();
            return pa.GetTasaByCode(codigo);
        }

        public void DeshabilitarAirport(string codigo, string user)
        {
            PAeropuerto pa = new PAeropuerto();
            pa.DeshabilitarAero(codigo);
        }

        public void HabilitarAirport(string codigo, string user)
        {
            PAeropuerto pa = new PAeropuerto();
            pa.HabilitarAero(codigo);
        }

        public bool ValidarCodigo(string codigo)
        {
            PAeropuerto pa = new PAeropuerto();
            return pa.ValidarAero(codigo);
        }


    }
}