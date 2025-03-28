﻿using AIMS.Data.Entities.Address;
using AIMS.Models.Entities;
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
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                entity.Property(e => e.Value).HasColumnName("value");
            });
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
            modelBuilder.Entity<CD>(entity =>
            {
                entity.ToTable("cd");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Artist).HasColumnName("artist");
                entity.Property(e => e.RecordLabel).HasColumnName("record_label");
                entity.Property(e => e.Tracklist).HasColumnName("tracklist");
                entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            });
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
            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("province");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
            });
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.ProvinceId).HasColumnName("province_id");
                entity.HasOne(d => d.Province).WithMany().HasForeignKey(d => d.ProvinceId);
            });
            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("ward");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.DistrictId).HasColumnName("district_id");
                entity.HasOne(w => w.District).WithMany().HasForeignKey(w => w.DistrictId);
            });
            modelBuilder.Entity<OrderData>(entity =>
            {
                entity.ToTable("orderdata");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Address).HasColumnName("address");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.ShippingFee).HasColumnName("shipping_fee");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.Instructions).HasColumnName("instructions");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.TotalPrice).HasColumnName("total_price");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Fullname).HasColumnName("fullname");
            });
            modelBuilder.Entity<OrderMedia>(entity =>
            {
                entity.ToTable("ordermedia");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(e => e.Name).HasColumnName("media_name");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.MediaId).HasColumnName("media_id");
            });
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("usercart");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(e => e.isSelected).HasColumnName("isSelected");
                entity.Property(e => e.MediaID).HasColumnName("media_id");
                entity.Property(e => e.MediaName).HasColumnName("media_name");
                entity.Property(e => e.MediaImgUrl).HasColumnName("media_imgurl");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Email).HasColumnName("email");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id").UseIdentityColumn(); // Tự động tăng ID
                entity.Property(e => e.Fullname).HasColumnName("fullname");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Admin).HasColumnName("admin");
                entity.Property(e => e.Salt).HasColumnName("salt");
                entity.Property(e => e.Status).HasColumnName("status");
            });
        }
    }
}