﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class alasdbEntities : DbContext
    {
        public alasdbEntities()
            : base("name=alasdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccionesEmpleados> AccionesEmpleados { get; set; }
        public virtual DbSet<Aeropuerto> Aeropuerto { get; set; }
        public virtual DbSet<Asiento> Asiento { get; set; }
        public virtual DbSet<Avion> Avion { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<HistoricoTasa> HistoricoTasa { get; set; }
        public virtual DbSet<Pasaje> Pasaje { get; set; }
        public virtual DbSet<Vuelo> Vuelo { get; set; }
        public virtual DbSet<VideoAvion> VideoAvion { get; set; }
        public virtual DbSet<PrecioCategoria> PrecioCategoria { get; set; }
    }
}
