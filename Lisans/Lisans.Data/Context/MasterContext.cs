using Lisans.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lisans.Data.Context
{
    public class MasterContext : DbContext
    {

        public MasterContext()
        {
        }

        public MasterContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<LisansModel> LisansModel { get; set; }
        public DbSet<ProvinceModel> ProvinceModel { get; set; }
        public DbSet<DistrictModel> DistrictModel { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("MYSQL_URI"));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LisansModel>().Property(x => x.Status).HasConversion<short>();
            modelBuilder.Entity<UserModel>().Property(x => x.isSuperAdmin).HasConversion<short>();
        }
    }
}
