using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HEmpleado
    {
        private static HEmpleado instance;

        private HEmpleado()
        {

        }

        public static HEmpleado getInstance()
        {
            if (instance == null)
            {
                instance = new HEmpleado();
            }

            return instance;
        }


        public void AddEmpleado(DtoEmpleado dto, string user)
        {
            PEmpleado pe = new PEmpleado();
            pe.AddEmpleado(dto);

        }

        public List<DtoEmpleado> ListarEmpleados()
        {
            PEmpleado pe = new PEmpleado();

            List<DtoEmpleado> col = pe.GetEmpleados();
            return col;
        }

        public List<DtoEmpleado> GetEmpleadosByData(string data)
        {
            PEmpleado pe = new PEmpleado();

            List<DtoEmpleado> col = pe.GetEmpleadosByData(data);
            return col;
        }

        public bool LoginEmpleado(DtoLogin dto)
        {
            PEmpleado pe = new PEmpleado();
            bool ok = pe.IngresoEmpleado(dto.documentoEmpleado, dto.contrasenia);
            return ok;
        }


        public DtoEmpleado GetEmpleadoByDoc(string docEmpleado)
        {
            PEmpleado pe = new PEmpleado();
            DtoEmpleado dto = pe.GetEmpleadoByDoc(docEmpleado);
            return dto;
        }

        public void UpdateEmpleado(DtoEmpleado dto)
        {
            PEmpleado pe = new PEmpleado();
            pe.UpdateEmpleado(dto);
        }

        public void DeleteEmpleado(string docEmpleado)
        {
            PEmpleado pe = new PEmpleado();
            pe.DeleteEmpleado(docEmpleado);
        }
        public void addAccionEmpleado(string tipoModificacion, DateTime fecha, string docEmpleado, int? avionId, int? vueloId, string abreviaturaAeropuerto)
        {
            DtoAccionEmpleado dto = new DtoAccionEmpleado(tipoModificacion, fecha, docEmpleado, avionId, vueloId, abreviaturaAeropuerto);
            PEmpleado pe= new PEmpleado();
            pe.addAccionEmpleado(dto);
        }

        public bool ValidarUser(string user)
        {
            PEmpleado pe = new PEmpleado();
            return pe.ValidarUser(user);
        }
    }
}
