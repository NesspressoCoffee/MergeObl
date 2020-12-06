using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HAsiento
    {
        private static HAsiento instance;

        private HAsiento()
        {

        }

        public static HAsiento getInstance()
        {
            if (instance == null)
            {
                instance = new HAsiento();
            }

            return instance;
        }

        public void addAsientos(int idAvion, int cantidadAsientos, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            PAsiento pa = new PAsiento();

            List<DtoAsiento> listDto = new List<DtoAsiento>();
            for (int i = 0; i < cantidadAsientos; i++)
            {
                listDto.Add(new DtoAsiento(i + 1, idAvion));
            }

            foreach (DtoCategoriaAsiento categoria in listCategoria)
            {
                for (int i = categoria.desde.GetValueOrDefault() - 1; i < categoria.hasta.GetValueOrDefault(); i++)
                {
                    listDto[i].categoria = categoria.categoria;
                }
            }

            foreach (DtoDisponibilidadAsiento disponibilidad in listDisponibilidad)
            {
                for (int i = disponibilidad.desde.GetValueOrDefault() - 1; i < disponibilidad.hasta.GetValueOrDefault(); i++)
                {
                    listDto[i].disponible = disponibilidad.disponibilidad;
                }
            }

            pa.addAsientos(listDto);
        }

        public void modifyAsientos(int idAvion, int cantidadAsientos, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            PAsiento pa = new PAsiento();

            List<DtoAsiento> listDto = new List<DtoAsiento>();
            for (int i = 0; i < cantidadAsientos; i++)
            {
                listDto.Add(new DtoAsiento(i + 1, idAvion));
            }

            foreach (DtoCategoriaAsiento categoria in listCategoria)
            {
                for (int i = categoria.desde.GetValueOrDefault() - 1; i < categoria.hasta.GetValueOrDefault(); i++)
                {
                    listDto[i].categoria = categoria.categoria;
                }
            }

            foreach (DtoDisponibilidadAsiento disponibilidad in listDisponibilidad)
            {
                for (int i = disponibilidad.desde.GetValueOrDefault() - 1; i < disponibilidad.hasta.GetValueOrDefault(); i++)
                {
                    listDto[i].disponible = disponibilidad.disponibilidad;
                }
            }

            pa.modifyAsientos(listDto, idAvion);
        }

        public List<DtoCategoriaAsiento> getColCategoriaAsiento(int idAvion)
        {
            PAsiento pa = new PAsiento();
            return pa.getColCategoriaAsiento(idAvion);
        }

        public List<DtoDisponibilidadAsiento> getColDisponibilidadAsiento(int idAvion)
        {
            PAsiento pa = new PAsiento();
            return pa.getColDisponibilidadAsiento(idAvion);
        }

        //VALIDACIONES
        public bool validarCantidadDeAsientos(int cantAsientos, List<DtoCategoriaAsiento> listDto)
        {
            if (listDto != null)
            {
                int totalGrupo = 0;
                foreach (DtoCategoriaAsiento dto in listDto)
                {
                    totalGrupo += (dto.hasta.GetValueOrDefault() - dto.desde.GetValueOrDefault() + 1);
                }

                if (cantAsientos == totalGrupo) return true;
                return false;
            }
            return false;
        }

        public bool validarCantidadDeAsientos(int cantAsientos, List<DtoDisponibilidadAsiento> listDto)
        {
            if (listDto != null)
            {
                int totalGrupo = 0;
                foreach (DtoDisponibilidadAsiento dto in listDto)
                {
                    totalGrupo += (dto.hasta.GetValueOrDefault() - dto.desde.GetValueOrDefault() + 1);
                }

                if (cantAsientos == totalGrupo) return true;
                return false;
            }
            return false;
        }

        public int validarGrupoDeAsientos(int cantAsientos, List<DtoCategoriaAsiento> listDto, int? desde, int? hasta)
        {
            if (desde == null || hasta == null) return 5;
            else if (desde < 0 || hasta < 0) return 4;

            if (hasta >= desde)
            {
                int desdeInt = desde.GetValueOrDefault();
                int hastaInt = hasta.GetValueOrDefault();

                int totalGrupo = hastaInt - desdeInt + 1;
                foreach (DtoCategoriaAsiento dto in listDto)
                {
                    if (hastaInt < dto.desde || desdeInt > dto.hasta)
                    {
                        totalGrupo += (dto.hasta.GetValueOrDefault() - dto.desde.GetValueOrDefault() + 1);
                    }
                    else return 3;
                }

                if (cantAsientos < totalGrupo) return 2;

                return 0;
            }
            return 1;

        }

        public int validarGrupoDeAsientos(int cantAsientos, List<DtoDisponibilidadAsiento> listDto, int? desde, int? hasta)
        {
            if (desde == null || hasta == null) return 5;
            else if (desde < 0 || hasta < 0) return 4;

            if (hasta >= desde)
            {
                int desdeInt = desde.GetValueOrDefault();
                int hastaInt = hasta.GetValueOrDefault();

                int totalGrupo = hastaInt - desdeInt + 1;
                foreach (DtoDisponibilidadAsiento dto in listDto)
                {
                    if (hastaInt < dto.desde || desdeInt > dto.hasta)
                    {
                        totalGrupo += (dto.hasta.GetValueOrDefault() - dto.desde.GetValueOrDefault() + 1);
                    }
                    else return 3;
                }

                if (cantAsientos < totalGrupo) return 2;

                return 0;
            }
            return 1;
        }

        public List<DtoAsiento> ObtenerAsientosAvion(int id)
        {
            PAsiento pa = new PAsiento();
            return pa.GetAsientosDisponibles(id);

        }


        public List<DtoVuelo> GetAsientosDisponibles(DtoVuelo vuelo, List<DtoVuelo> dto)
        {
            List<DtoVuelo> vuelosDirectos = new List<DtoVuelo>();

            foreach (DtoVuelo item in dto)
            {
                int asientosDisponibles = HPasaje.getInstance().GetAsientosDisponibles(item.idVuelo, item.avionId).Count();
                if (vuelo.cantidadAsientos <= asientosDisponibles)
                {
                    vuelosDirectos.Add(item);
                }
            }

            return vuelosDirectos;
        }
    }
}
