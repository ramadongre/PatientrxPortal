﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Patientportal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PatientPortalEntities : DbContext
    {
        public PatientPortalEntities()
            : base("name=PatientPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_Patient> TB_Patient { get; set; }
        public virtual DbSet<TB_PatientRxData> TB_PatientRxData { get; set; }
        public virtual DbSet<TB_RxData> TB_RxData { get; set; }
        public virtual DbSet<TB_PortalUser> TB_PortalUser { get; set; }
    }
}
