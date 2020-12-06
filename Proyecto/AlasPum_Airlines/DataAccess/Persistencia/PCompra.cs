using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PCompra
    {
       
            public MCompra mapper;

            public PCompra()
            {
                this.mapper = new MCompra();
            }


        public int IngresarCompra(DtoCompra compra)
        {
            int id = compra.idCompra;
            Compra nuevaCompra = mapper.MapToObj(compra);
            using (alasdbEntities context = new alasdbEntities())
            {
                context.Compra.Add(nuevaCompra);
                context.SaveChanges();

                id = nuevaCompra.idCompra;

            }

            return id;
        }



    }
}
