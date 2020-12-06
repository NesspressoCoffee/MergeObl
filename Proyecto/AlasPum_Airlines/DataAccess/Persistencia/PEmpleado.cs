using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PEmpleado
    {
        public MAccionEmpleado mapperAcciones;
        public MEmpleado mapper;

        public PEmpleado()
        {
            this.mapper = new MEmpleado();
            this.mapperAcciones = new MAccionEmpleado();
        }

        public void AddEmpleado(DtoEmpleado dto)
        {
            Empleado nuevoEmpleado = mapper.MapToObj(dto);

            using (alasdbEntities context = new alasdbEntities())
            {
                context.Empleado.Add(nuevoEmpleado);
                context.SaveChanges();
            }
        }

        public void DeleteEmpleado(string docEmpleado)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Empleado borrarEmpleado = context.Empleado.FirstOrDefault(f => f.documentoEmpleado == docEmpleado);
                context.Empleado.Remove(borrarEmpleado);
                context.SaveChanges();
            }

        }


        public void UpdateEmpleado(DtoEmpleado dto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                Empleado modEmpleado = context.Empleado.FirstOrDefault(f => f.documentoEmpleado == dto.documentoEmpleado);
                modEmpleado.documentoEmpleado = dto.documentoEmpleado;
                modEmpleado.nombreEmpleado = dto.nombreEmpleado;
                modEmpleado.apellidoEmpleado = dto.apellidoEmpleado;
                modEmpleado.contrasenia = dto.contrasenia;

                context.SaveChanges();
            }

        }

        public bool IngresoEmpleado(string doc, string pwd)
        {
            bool ok = true;
            using (alasdbEntities context = new alasdbEntities())
            {
                ok = context.Empleado.Any(a => a.documentoEmpleado == doc && a.contrasenia == pwd && a.disponible == true);
            }
            return ok;

        }

        public List<DtoEmpleado> GetEmpleados()
        {
            List<DtoEmpleado> listaEmp = new List<DtoEmpleado>();

            using (alasdbEntities context = new alasdbEntities())
            {
                foreach (Empleado item in context.Empleado)
                {
                    DtoEmpleado dto = mapper.MapToDto(item);
                    listaEmp.Add(dto);
                }
            }
            return listaEmp;
        }

        public List<DtoEmpleado> GetEmpleadosByData(string data)
        {
            List<DtoEmpleado> listaEmp = new List<DtoEmpleado>();
            List<Empleado> lista = new List<Empleado>();

            using (alasdbEntities context = new alasdbEntities())
            {
                lista = context.Empleado.Where(w => w.documentoEmpleado.Contains(data) || w.nombreEmpleado.Contains(data) || w.apellidoEmpleado.Contains(data)).ToList();

                foreach (Empleado item in lista)
                {
                    DtoEmpleado dto = mapper.MapToDto(item);
                    listaEmp.Add(dto);
                }
            }

            return listaEmp;
        }

        public DtoEmpleado GetEmpleadoByDoc(string docEmpleado)
        {
            DtoEmpleado dto = new DtoEmpleado();
            using (alasdbEntities context = new alasdbEntities())
            {
                dto = mapper.MapToDto(context.Empleado.FirstOrDefault(f => f.documentoEmpleado == docEmpleado));
            }
            return dto;
        }

        public void addAccionEmpleado(DtoAccionEmpleado dto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                context.AccionesEmpleados.Add(mapperAcciones.MapToObj(dto));
                context.SaveChanges();
            }
        }

        public bool ValidarUser(string user)
        {
            bool existe = false;

            using (alasdbEntities context = new alasdbEntities())
            {
                existe = context.Empleado.Any(a => a.documentoEmpleado == user && a.disponible == true);

            }

            return existe;
        }
    }
}
