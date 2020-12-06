using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HPrecioCategoria
    {
        private static HPrecioCategoria instance;

        private HPrecioCategoria()
        {

        }

        public static HPrecioCategoria getInstance()
        {
            if (instance == null)
            {
                instance = new HPrecioCategoria();
            }

            return instance;
        }


        public List<DtoVuelo> GetPreciosCategoriasByFecha(string numVuelo, DateTime fechaInicio)
        {
            PPrecioCategoria pp = new PPrecioCategoria();
            return pp.GetPreciosCategoriasByFecha(numVuelo, fechaInicio);
        }

        public DtoPrecioCategoria GetPreciosByIdVuelo(int idVuelo)
        {
            PPrecioCategoria pp = new PPrecioCategoria();
            return pp.GetPreciosByIdVuelo(idVuelo);
        }
    }
}
