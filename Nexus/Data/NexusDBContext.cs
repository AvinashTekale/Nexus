using Microsoft.EntityFrameworkCore;
using Nexus.Entities;
using System.Collections.Generic;

namespace Nexus.Data
{
    public class NexusDBContext : DbContext
    {
        public NexusDBContext(DbContextOptions<NexusDBContext> options) : base(options) { }

        // Existing Tables
        public DbSet<User> Users { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<BMEAccount> BMEAccounts { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EquipmentModel> EquipmentModels { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategories { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentMaintenance> EquipmentMaintenances { get; set; }

        // New Tables
        public DbSet<PurchaseOrderEntity> PurchaseOrders { get; set; }
        public DbSet<InstallationBaseEntity> InstallationBases { get; set; }
        public DbSet<BreakDownOpenEntity> BreakDownsOpen { get; set; }
        public DbSet<BreakDownCloseEntity> BreakDownsClose { get; set; }
        public DbSet<PartDetailEntity> PartDetails { get; set; }
        public DbSet<ContractOrderEntity> ContractOrders { get; set; }
        public DbSet<PMEntity> PMs { get; set; }
        public DbSet<CalibrationEntity> Calibrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ One-to-Many: PurchaseOrder → InstallationBase
            modelBuilder.Entity<InstallationBaseEntity>()
                .HasOne(i => i.PurchaseOrder)
                .WithMany(p => p.InstallationBases)
                .HasForeignKey(i => i.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete

            // ✅ Equipment Foreign Keys
            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.EquipmentModel)
                .WithMany()
                .HasForeignKey(e => e.EquipmentModelId);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.EquipmentCategory)
                .WithMany()
                .HasForeignKey(e => e.EquipmentCategoryId);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Hospital)
                .WithMany()
                .HasForeignKey(e => e.HospitalId);
        }
    }
}
