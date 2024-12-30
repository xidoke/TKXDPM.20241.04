using AIMS.Data.Entities;
using AIMS.Data.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Media> Medias { get; set; }
        public DbSet<CD> CDs { get; set; }
        public DbSet<DVD> DVDs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<OrderMedia> OrderMedias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderData> OrderDatas { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships, constraints, etc. here

            // Configure the base entity: Media
            modelBuilder.Entity<Media>(entity =>
            {
                entity.ToTable("media");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Category).HasColumnName("category");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.ImgUrl).HasColumnName("imgurl");
                entity.Property(e => e.RushSupport).HasColumnName("rush_support");
                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            // Configure derived entity: Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Author).HasColumnName("author");
                entity.Property(e => e.CoverType).HasColumnName("cover_type");
                entity.Property(e => e.Publisher).HasColumnName("publisher");
                entity.Property(e => e.PublishDate).HasColumnName("publish_date");
                entity.Property(e => e.NumberOfPages).HasColumnName("number_of_pages");
                entity.Property(e => e.Language).HasColumnName("language");
            });

            // Configure derived entity: CD
            modelBuilder.Entity<CD>(entity =>
            {
                entity.ToTable("cd");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Artist).HasColumnName("artist");
                entity.Property(e => e.RecordLabel).HasColumnName("record_label");
                entity.Property(e => e.Tracklist).HasColumnName("tracklist");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            });

            // Configure derived entity: DVD
            modelBuilder.Entity<DVD>(entity =>
            {
                entity.ToTable("dvd");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DiscType).HasColumnName("disc_type");
                entity.Property(e => e.Director).HasColumnName("director");
                entity.Property(e => e.Runtime).HasColumnName("runtime");
                entity.Property(e => e.Studio).HasColumnName("studio");
                entity.Property(e => e.Subtitle).HasColumnName("subtitle");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            });

            // Configure other relationships
            modelBuilder.Entity<OrderData>()
                .HasMany(o => o.OrderMedias)
                .WithOne(om => om.Order)
                .HasForeignKey(om => om.OrderId);
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("province");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            // Configure District entity
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.ProvinceId).HasColumnName("province_id");

                // Configure relationship with Province
                entity.HasOne(d => d.Province)
                    .WithMany() // Assuming a Province can have many Districts
                    .HasForeignKey(d => d.ProvinceId);
            });

            // Configure Ward entity
            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("ward");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.DistrictId).HasColumnName("district_id");

                // Configure relationship with District
                entity.HasOne(w => w.District)
                    .WithMany() // Assuming a District can have many Wards
                    .HasForeignKey(w => w.DistrictId);
            });
        }
    }
}