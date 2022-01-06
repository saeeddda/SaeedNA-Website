using System.Linq;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Data.Entities.Resume;
using SaeedNA.Data.Entities.MService;
using SaeedNA.Data.Entities.Settings;
using SaeedNA.Data.Entities.Contact;
using SaeedNA.Data.Entities.Account;

namespace SaeedNA.Data.Context
{
    public class SaeedNAContext : DbContext
    {
        public SaeedNAContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relations in builder.Model.GetEntityTypes().SelectMany(s=>s.GetForeignKeys()))
            {
                relations.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<MyService> MyServices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Seo> Seos { get; set; }
        public DbSet<SocialMedia> SocialMediaes { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
