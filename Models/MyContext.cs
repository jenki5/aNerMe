using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}        
        public DbSet<AdminLevel> AdminLevels {get;set;}
        public DbSet<Administrator> Administrators {get;set;}
        public DbSet<Address> Addresses {get;set;}
        public DbSet<Banner> Banners {get;set;}
        public DbSet<BannerPartial> BannerPartials {get;set;}
        public DbSet<Category> Categories {get;set;}
        public DbSet<City> Cities {get;set;}
        public DbSet<Company> Companies {get;set;}
        public DbSet<CompanyAddress> CompanyAddresses {get;set;}
        public DbSet<ContactUsPage> ContactUsPages {get;set;}
        public DbSet<DeliveryMethod> DeliveryMethods {get;set;}
        public DbSet<Image> Images {get;set;}
        public DbSet<LTPAction> LTPActions {get;set;}
        public DbSet<MenuAction> MenuActions {get;set;}
        public DbSet<MenuItem> MenuItems {get;set;}
        public DbSet<PageCategory> PageCategories {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<ProductCategory> ProductCategories {get;set;}
        public DbSet<ProductImage> ProductImages {get;set;}
        public DbSet<State> States {get;set;}
        public DbSet<StreetAddress> StreetAddresses {get;set;}
        public DbSet<StoreDeliveryMethod> StoreDeliveryMethods {get;set;}
        public DbSet<User> Users {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyAddress>().HasKey(c => new { c.CompanyID, c.AddressID });
            modelBuilder.Entity<PageCategory>().HasKey(c => new { c.MenuItemID, c.CategoryID });
            modelBuilder.Entity<ProductImage>().HasKey(c => new { c.ImageID, c.ProductID, c.OrderNumber });
            modelBuilder.Entity<ProductCategory>().HasKey(c => new { c.ProductID, c.CategoryID });
            modelBuilder.Entity<StoreDeliveryMethod>().HasKey(c => new { c.ContactUsPageID, c.DeliveryMethodID });
        }
    }    
}