using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonSolutions.DTO;
using BusinessLogic.Helpers;

namespace AlasPum_Airlines.AuthenticationHelper
{
    public class UserAuthentication : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = base.AuthorizeCore(httpContext);

            if (result == false)
            {
                return result;
            }

            string pagina = httpContext.Request.CurrentExecutionFilePath.Split('/')[2];

            string idEmpleado = httpContext.Session["idEmpleado"].ToString();
            DtoEmpleado empleado = HEmpleado.getInstance().GetEmpleadoByDoc(idEmpleado);

            //if (pagina == "AgregarEmpleado" && empleado.administrador != true)
            //{
            //    return false;
            //}
            //else if (pagina == "AgregarStock" && empleado.puesto == "Chofer")
            //{
            //    return false;
            //}


            return true;
        }
    }

}
