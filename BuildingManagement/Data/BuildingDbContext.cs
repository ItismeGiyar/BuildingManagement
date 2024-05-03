using BuildingManagement.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace BuildingManagement.Data
{
    public class BuildingDbContext :DbContext
    {
        public BuildingDbContext(DbContextOptions<BuildingDbContext> options) : base(options) { }

        public DbSet<Company> ms_company { get; set; }
        public DbSet<Config> ms_config { get; set; }
        public DbSet<Menugp> ms_menugp { get; set;}
        public DbSet<Menuaccess> ms_menuaccess { get; set;}
        public DbSet<User> ms_user { get; set; }
        public DbSet<Residenttype> ms_residenttype { get; set; }
        public DbSet<Location> ms_location { get; set; }
        public DbSet<Buildingtype> ms_buildingtype { get; set;}
        public DbSet<Propertyinfo> ms_propertyinfo { get; set;}
        public DbSet<Tenant> ms_tenant { get; set; }
        public DbSet<Propertyroom> ms_propertyroom { get; set;}
        public DbSet<Billitem> ms_billitem { get; set; }
        public DbSet<Tenantvehical> ms_tenantvehical { get; set; }
        public DbSet<Complaintcatg> ms_complaintcatg { get; set; }
        public DbSet<Billitemtenant> ms_billitemtenant { get;set; }

    }
}