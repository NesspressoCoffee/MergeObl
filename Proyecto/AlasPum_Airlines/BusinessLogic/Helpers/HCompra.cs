using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HCompra
    {

        private static HCompra instance;

        private HCompra()
        {

        }

        public static HCompra getInstance()
        {
            if (instance == null)
            {
                instance = new HCompra();
            }

            return instance;
        }

        public int ConfirmarCompra(DtoCompra dto, List<DtoPasajero> pasajeros, DtoVuelo vuelo)
        {
            dto.fechaCompra = DateTime.Now;
            dto.vuelo = vuelo;
            PCompra nuevaCompra = new PCompra();
            int idCompra = nuevaCompra.IngresarCompra(dto);
            HPasaje.getInstance().IngresarPasajes(dto, pasajeros, idCompra);

            return idCompra;
        }






    }
}
