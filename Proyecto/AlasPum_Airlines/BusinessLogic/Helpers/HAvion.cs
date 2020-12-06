using BusinessLogic.Interfaces;
using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HAvion
    {
        private static HAvion instance;

        private HAvion()
        {

        }

        public static HAvion getInstance()
        {
            if (instance == null)
            {
                instance = new HAvion();
            }

            return instance;
        }

        public void addAvion(DtoAvion dto, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            int idAvion;
            dto.disponible = true;
            PAvion pa = new PAvion();
            pa.addAvion(dto, out idAvion);
            HAsiento.getInstance().addAsientos(idAvion, dto.cantAsientos, listCategoria, listDisponibilidad);
            HEmpleado.getInstance().addAccionEmpleado("Alta", DateTime.Now, "123", idAvion, null, null);
        } 

        public void modifyAvion(DtoAvion dto, List<DtoCategoriaAsiento> listCategoria, List<DtoDisponibilidadAsiento> listDisponibilidad)
        {
            int idAvion;
            PAvion pa = new PAvion();
            pa.modifyAvion(dto, out idAvion);
            HAsiento.getInstance().modifyAsientos(dto.idAvion, dto.cantAsientos, listCategoria, listDisponibilidad);
        }

        public List<DtoAvion> getColAvion()
        {
            List<DtoAvion> col = new List<DtoAvion>();
            PAvion pa = new PAvion();
            col = pa.getColAvion();
            return col;
     
        }
     
        public DtoAvion getAvion(int idAvion)
        {
            PAvion pa = new PAvion();
            return pa.getAvion(idAvion);
        }
     
        public int deleteAvion(int idAvion)
        {
            PAvion pa = new PAvion();
            DtoAvion dto = getAvion(idAvion); //PARA RESOLVER: CAMBIAR DOC. EMPLEADO POR DOC. EN SESION
            HEmpleado.getInstance().addAccionEmpleado("Baja", DateTime.Now, "123", dto.idAvion, null, null);
            return pa.deleteAvion(idAvion);
        }

        public bool ValidarExistenciaAvion(int avionId)
        {
            PAvion pa = new PAvion();
            return pa.ValidarExistenciaAvion(avionId);
        }

        public bool ValidarDisponibilidadAvion(int idAvion, DateTime salida, DateTime llegada, string destino, string origen, int idVuelo)
        {
            PAvion pa = new PAvion();
            return pa.ValidarDisponibilidadAvion(idAvion, salida, llegada, destino, origen, idVuelo);
        }
    }
}
