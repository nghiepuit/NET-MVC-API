﻿using Ecommerce.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Ecommerce.Data
{
    public class EcommerceDbContext : IdentityDbContext<AppUser>
    {
        public EcommerceDbContext() : base("EcommerceConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }

        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }


        public DbSet<Color> Colors { set; get; }
        public DbSet<ProductColor> ProductColors { set; get; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<ProductSize> ProductSizes { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }

        public static EcommerceDbContext Create()
        {
            return new EcommerceDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AppRoles");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
        }
    }
}