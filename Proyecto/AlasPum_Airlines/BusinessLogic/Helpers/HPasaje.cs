using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class HPasaje
    {
        private static HPasaje instance;

        private HPasaje()
        {

        }

        public static HPasaje getInstance()
        {
            if (instance == null)
            {
                instance = new HPasaje();
            }

            return instance;
        }

        public List<DtoAsiento> GetAsientosDisponibles(int idVuelo, int avionId)
        {

            List<DtoAsiento> asientosDisponibles = new List<DtoAsiento>();
            List<DtoAsiento> asientosDeAvion = HAsiento.getInstance().ObtenerAsientosAvion(avionId);
            PPasaje pp = new PPasaje();
            List<DtoPasaje> pasajesConfirmados = pp.GetPasajesVuelo(idVuelo);
            foreach (DtoAsiento item in asientosDeAvion)
            {
                if (pasajesConfirmados.Count == 0)
                {
                    return asientosDeAvion;

                }
                else
                {

                    foreach (DtoPasaje aux in pasajesConfirmados)
                    {

                        if (item.numeroAsiento != aux.asientoNumero)
                        {

                            asientosDisponibles.Add(item);
                        }

                    }
                }


            }


            return asientosDisponibles;

        }



        public void IngresarPasajes(DtoCompra dto, List<DtoPasajero> pasajeros, int idCompra)
        {
            DtoPasaje pasaje = new DtoPasaje();
            List<DtoPasaje> colPasajes = new List<DtoPasaje>();
            List<DtoAsiento> asientos = new List<DtoAsiento>();
            foreach (DtoPasajero item in pasajeros)
            {

                pasaje.nombrePasajero = item.nombre;
                pasaje.apellidoPasajero = item.apellido;
                pasaje.documentoPasajero = item.documento;
                pasaje.vueloId = dto.vuelo.idVuelo;
                pasaje.avionId = dto.vuelo.avionId;
                pasaje.compraId = idCompra;

                if (asientos.Count == 0)
                {
                    {
                        asientos = GetAsientosDisponibles(pasaje.vueloId, pasaje.avionId).Where(w => w.categoria == dto.vuelo.categoria).Take(pasajeros.Count()).ToList();
                    }

                    foreach (DtoAsiento aux in asientos.ToList())
                    {
                        pasaje.asientoNumero = aux.numeroAsiento;
                        asientos.Remove(aux);
                    }
                    colPasajes.Add(pasaje);

                }

                PPasaje pasajes = new PPasaje();
                pasajes.IngresarPasajes(colPasajes);
            }


        }
    }
}
