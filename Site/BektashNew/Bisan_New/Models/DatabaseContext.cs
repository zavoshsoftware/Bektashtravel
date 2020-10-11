using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
             System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
            
        }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<AirLine> AirLines { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TextType> TextTypes { get; set; }
        public DbSet<TextTypeItem> TextTypeItems { get; set; }
        public DbSet<CommentType> CommentTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Visa> Visas { get; set; }
        public System.Data.Entity.DbSet<Models.TourImage> TourImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<NewsLetter> NewsLetters{ get; set; }
        public DbSet<VisaDetail> VisaDetails{ get; set; }
        public DbSet<TourDetail> TourDetails{ get; set; }
        public DbSet<Type> Types{ get; set; }
        public DbSet<VisaTour> VisaTours{ get; set; }

        public System.Data.Entity.DbSet<Models.Feature> Features { get; set; }

        public System.Data.Entity.DbSet<Models.FeatureType> FeatureTypes { get; set; }

        public System.Data.Entity.DbSet<Models.HotelFeature> HotelFeatures { get; set; }

        public System.Data.Entity.DbSet<Models.HotelCategory> HotelCategories { get; set; }

        public System.Data.Entity.DbSet<Models.ProductGroup> ProductGroups { get; set; }
        public System.Data.Entity.DbSet<Models.Product> Products { get; set; }
        public System.Data.Entity.DbSet<Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<Models.OrderDetail> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<Models.VisaType> VisaTypes { get; set; }
    }
}
