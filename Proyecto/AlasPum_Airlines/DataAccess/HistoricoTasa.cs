//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class HistoricoTasa
    {
        public int idTasa { get; set; }
        public System.DateTime fecha { get; set; }
        public double tasaRegional { get; set; }
        public double tasaIntercontinental { get; set; }
        public string codigoAirport { get; set; }
    
        public virtual Aeropuerto Aeropuerto { get; set; }
    }
}
