using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PAsiento
    {
        public MAsiento mapper;

        public PAsiento()
        {
            this.mapper = new MAsiento();
        }

        public void addAsientos(List<DtoAsiento> listDto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (DtoAsiento dto in listDto)
                {
                    Asiento nuevoAsiento = mapper.MapToObj(dto);
                    context.Asiento.Add(nuevoAsiento);
                }
                context.SaveChanges();
            }
        }

        public void modifyAsientos(List<DtoAsiento> listDto, int idAvion)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                List<Asiento> asientos = context.Asiento.Where(w => w.avionId == idAvion).ToList();
                if (listDto.Count() < asientos.Count())
                {
                    for(int i = listDto.Count(); i < asientos.Count(); i++)
                    {
                        context.Asiento.Remove(asientos[i]);
                    }
                }
                foreach (DtoAsiento dto in listDto)
                {
                    if (context.Asiento.Any(a => a.avionId == idAvion && a.numeroAsiento == dto.numeroAsiento))
                    {
                        Asiento asiento = context.Asiento.Where(w => w.avionId == idAvion && w.numeroAsiento == dto.numeroAsiento).FirstOrDefault();
                        asiento.categoria = dto.categoria;
                        asiento.disponible = dto.disponible;
                    }
                    else
                    {
                        context.Asiento.Add(mapper.MapToObj(dto));

                    }
                }
                context.SaveChanges();
            }
        }

        public List<DtoCategoriaAsiento> getColCategoriaAsiento(int idAvion)
        {
            List<DtoCategoriaAsiento> listDto = new List<DtoCategoriaAsiento>();
            List<Asiento> listAsiento = new List<Asiento>();
            string categoria = "";

            using (alasdbEntities context = new alasdbEntities())
            {
                listAsiento = context.Asiento.Where(w => w.avionId == idAvion).ToList();
            }

            DtoCategoriaAsiento dto = new DtoCategoriaAsiento();

            for (int i = 0; i < listAsiento.Count(); i++)
            {
                if (i == 0)
                {
                    dto.desde = listAsiento[i].numeroAsiento;
                    dto.categoria = listAsiento[i].categoria;
                    categoria = listAsiento[i].categoria;
                    dto.id = i;
                }
                else if (listAsiento[i].categoria != categoria)
                {
                    dto.hasta = listAsiento[i - 1].numeroAsiento;
                    listDto.Add(dto);
                    categoria = listAsiento[i].categoria;
                    dto = new DtoCategoriaAsiento();
                    dto.desde = listAsiento[i].numeroAsiento;
                    dto.categoria = listAsiento[i].categoria;
                    dto.id = i;
                }
                else if (i == listAsiento.Count() - 1)
                {
                    dto.hasta = listAsiento[i].numeroAsiento;
                    listDto.Add(dto);
                }
                else { }
            }

            return listDto;
        }

        public List<DtoDisponibilidadAsiento> getColDisponibilidadAsiento(int idAvion)
        {
            List<DtoDisponibilidadAsiento> listDto = new List<DtoDisponibilidadAsiento>();
            List<Asiento> listAsiento = new List<Asiento>();
            string categoria = "";

            using (alasdbEntities context = new alasdbEntities())
            {
                listAsiento = context.Asiento.Where(w => w.avionId == idAvion).ToList();
            }

            DtoDisponibilidadAsiento dto = new DtoDisponibilidadAsiento();

            for (int i = 0; i < listAsiento.Count(); i++)
            {
                if (i == 0)
                {
                    dto.desde = listAsiento[i].numeroAsiento;
                    dto.disponibilidad = listAsiento[i].disponible;

                    if (dto.disponibilidad == true)
                    {
                        dto.disponibilidadStr = "Si";
                    }
                    else dto.disponibilidadStr = "No";

                    categoria = listAsiento[i].categoria;
                    dto.id = i;
                }
                else if (listAsiento[i].categoria != categoria)
                {
                    dto.hasta = listAsiento[i - 1].numeroAsiento;
                    listDto.Add(dto);
                    categoria = listAsiento[i].categoria;
                    dto = new DtoDisponibilidadAsiento();
                    dto.desde = listAsiento[i].numeroAsiento;
                    dto.disponibilidad = listAsiento[i].disponible;
                    if (dto.disponibilidad == true)
                    {
                        dto.disponibilidadStr = "Si";
                    }
                    else dto.disponibilidadStr = "No";
                    dto.id = i;
                }
                else if (i == listAsiento.Count() - 1)
                {
                    dto.hasta = listAsiento[i].numeroAsiento;
                    listDto.Add(dto);
                }
                else { }
            }

            return listDto;
        }

        public List<DtoAsiento> GetAsientosDisponibles(int idAvion)
        {
            List<DtoAsiento> dto = new List<DtoAsiento>();

            using (alasdbEntities context = new alasdbEntities())
            {
                List<Asiento> asientos = context.Asiento.Where(w => w.avionId == idAvion && w.disponible == true).ToList();
                foreach (Asiento item in asientos)
                {
                    dto.Add(mapper.MapToDto(item));
                }
            }

            return dto;
        }
    }
}
