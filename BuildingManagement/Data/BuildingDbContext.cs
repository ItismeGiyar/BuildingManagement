using BuildingManagement.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace BuildingManagement.Data
{
    public class BuildingDbContext : DbContext
    {
      

        public BuildingDbContext(DbContextOptions <BuildingDbContext> options) : base(options) { }

        public DbSet<Company> ms_company { get; set; }
        public DbSet<Config> ms_config { get; set; }
        public DbSet<Menugp> ms_menugp { get; set; }
        public DbSet<Menuaccess> ms_menuaccess { get; set; }
        public DbSet<User> ms_user { get; set; }
        public DbSet<Residenttype> ms_residenttype { get; set; }
        public DbSet<Location> ms_location { get; set; }
        public DbSet<Buildingtype> ms_buildingtype { get; set; }
        public DbSet<PropertyInfo> ms_propertyinfo { get; set; }
        public DbSet<Tenant> ms_tenant { get; set; }
        public DbSet<PropertyRoom> ms_propertyroom { get; set; }
        public DbSet<BillItem> ms_billitem { get; set; }
        public DbSet<TenantVehical> ms_tenantvehical { get; set; }
        public DbSet<ComplaintCatg> ms_complaintcatg { get; set; }
        public DbSet<BillItemTenant> ms_billitemtenant { get; set; }
        public DbSet<BillPayment> pms_billpayment { get; set; }

        public DbSet<Billledger> pms_billledger { get; set; }


    }
}
