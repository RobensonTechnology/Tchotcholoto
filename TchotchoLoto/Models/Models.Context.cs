﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TchotchoLoto.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccessTimeApplication> AccessTimeApplications { get; set; }
        public virtual DbSet<AddAppAccessLog> AddAppAccessLogs { get; set; }
        public virtual DbSet<AddUserActionLog> AddUserActionLogs { get; set; }
        public virtual DbSet<Agence> Agences { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<AppNavigation> AppNavigations { get; set; }
        public virtual DbSet<AppNavigationApplication> AppNavigationApplications { get; set; }
        public virtual DbSet<AppNavigationPermission> AppNavigationPermissions { get; set; }
        public virtual DbSet<Boule> Boules { get; set; }
        public virtual DbSet<Compagnie> Compagnies { get; set; }
        public virtual DbSet<GagnantLotterie> GagnantLotteries { get; set; }
        public virtual DbSet<LivJwetla> LivJwetlas { get; set; }
        public virtual DbSet<LotterieTirage> LotterieTirages { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PointDeVente> PointDeVentes { get; set; }
        public virtual DbSet<RapportDeVente> RapportDeVentes { get; set; }
        public virtual DbSet<RapportTirage> RapportTirages { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<TicketDetail> TicketDetails { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Tirage> Tirages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCompagnie> UserCompagnies { get; set; }
        public virtual DbSet<UserPointDeVente> UserPointDeVentes { get; set; }
        public virtual DbSet<Paiement> Paiements { get; set; }
        public virtual DbSet<TirageEnExecution> TirageEnExecutions { get; set; }
    }
}
