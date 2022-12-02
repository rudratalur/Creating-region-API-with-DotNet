using Microsoft.EntityFrameworkCore;
using WebApiDemoProject.Models.Domain;

namespace WebApiDemoProject.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext>
            options): base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
           

        //}
        public DbSet<User> Users{ get; set; }
         public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> UserRoles { get; set; }

public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User_Role>().HasOne(x => x.Role)
               .WithMany(y => y.UserRole)
               .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRole)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<Region>().HasData(new Region {Id=1, Code = "NRTHL", Area = 13789, Longitude = -35.3708304, Latitude = 172.5717825, Population = 194600, AreaName = "Northland Region" });
            //AUCK', 'Auckland Region', 4894, -36.5253207, 173.7785704, 1718982
            modelBuilder.Entity<Region>().HasData(new Region {Id=2, Code = "AUCK", AreaName = "Auckland Region", Area = 4894, Longitude = -36.5253207, Latitude = 173.7785704, Population = 1718982 });
            }

    }
}
