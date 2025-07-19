using ECommerceMVC.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Inventory> Inventories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<ProductTag> ProductTags { get; set; } = null!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = null!;
    public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; } = null!;
    public DbSet<ProductVariantImage> ProductVariantImages { get; set; } = null!;
    public DbSet<RefundRequest> RefundRequests { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<ShippingMethod> ShippingMethods { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<WishlistItem> WishlistItems { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Složený klíč pro ProductTag
        modelBuilder.Entity<ProductTag>()
            .HasKey(pt => new { pt.ProductId, pt.TagId });
        
        
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Kategorie
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1, Name = "Mobilní telefony", Description = "Chytré telefony a příslušenství.", IsActive = true
            },
            new Category
            {
                Id = 2, Name = "Notebooky a PC", Description = "Počítače, notebooky a jejich komponenty.",
                IsActive = true
            },
            new Category
            {
                Id = 3, Name = "Bezpečnostní technika", Description = "Kamery, senzory, alarmy a chytré zabezpečení.",
                IsActive = true
            },
            new Category
            {
                Id = 4, Name = "Sportovní vybavení", Description = "Sportovní potřeby, oblečení a doplňky.",
                IsActive = true
            },
            new Category
            {
                Id = 5, Name = "Móda a oblečení", Description = "Stylové oblečení a obuv pro každého.", IsActive = true
            },
            new Category
            {
                Id = 6, Name = "Hračky a stavebnice", Description = "Hračky pro děti všech věkových kategorií.",
                IsActive = true
            },
            new Category
            {
                Id = 7, Name = "TV a audio", Description = "Televize, reproduktory, audio zařízení.", IsActive = true
            },
            new Category
            {
                Id = 8, Name = "Fotoaparáty a kamery", Description = "Digitální a akční kamery, příslušenství.",
                IsActive = true
            },
            new Category
            {
                Id = 9, Name = "Herní zóna", Description = "Konzole, ovladače, příslušenství pro hráče.",
                IsActive = true
            },
            new Category
            {
                Id = 10, Name = "Domácnost a chytrá zařízení",
                Description = "Smart zařízení, žárovky, zásuvky a pomocníci do domácnosti.", IsActive = true
            }
        );


        // Značky
        modelBuilder.Entity<Brand>().HasData(
            new Brand
            {
                Id = 1, Name = "Apple", Description = "Prémiová značka mobilních telefonů a elektroniky.",
                LogoUrl = "/images/brands/apple.png"
            },
            new Brand
            {
                Id = 2, Name = "Samsung", Description = "Globální výrobce telefonů, televizí a paměťových zařízení.",
                LogoUrl = "/images/brands/samsung.png"
            },
            new Brand
            {
                Id = 3, Name = "Reolink", Description = "Značka specializující se na bezpečnostní kamery a systémy.",
                LogoUrl = "/images/brands/reolink.png"
            },
            new Brand
            {
                Id = 4, Name = "Nike", Description = "Značka sportovního oblečení, obuvi a doplňků.",
                LogoUrl = "/images/brands/nike.png"
            },
            new Brand
            {
                Id = 5, Name = "Dell", Description = "Výrobce výkonných notebooků, PC a serverů.",
                LogoUrl = "/images/brands/dell.png"
            },
            new Brand
            {
                Id = 6, Name = "GoPro", Description = "Akční kamery a příslušenství pro outdoor a sport.",
                LogoUrl = "/images/brands/gopro.png"
            },
            new Brand
            {
                Id = 7, Name = "Sony", Description = "Značka kvalitní spotřební elektroniky a zábavy.",
                LogoUrl = "/images/brands/sony.png"
            },
            new Brand
            {
                Id = 8, Name = "LEGO", Description = "Kreativní stavebnice a hračky pro děti i dospělé.",
                LogoUrl = "/images/brands/lego.png"
            },
            new Brand
            {
                Id = 9, Name = "Asus", Description = "Počítače, komponenty a herní zařízení.",
                LogoUrl = "/images/brands/asus.png"
            },
            new Brand
            {
                Id = 10, Name = "H&M", Description = "Módní oblečení za dostupné ceny.",
                LogoUrl = "/images/brands/hm.png"
            }
        );

        // Štítky
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Novinka" },
            new Tag { Id = 2, Name = "Akce" },
            new Tag { Id = 3, Name = "Doprava zdarma" },
            new Tag { Id = 4, Name = "Bestseller" },
            new Tag { Id = 5, Name = "Výprodej" },
            new Tag { Id = 6, Name = "Prémiové" }
        );
                

        // Sklad
        modelBuilder.Entity<Warehouse>().HasData(
            new Warehouse { Id = 1, Name = "Hlavní sklad Praha", Address = "U Skladu 1, Praha" },
            new Warehouse { Id = 2, Name = "Sklad Brno", Address = "Technologická 12, Brno" },
            new Warehouse { Id = 3, Name = "Sklad Ostrava", Address = "Logistická 99, Ostrava" },
            new Warehouse { Id = 4, Name = "Záložní sklad Plzeň", Address = "Průmyslová 23, Plzeň" },
            new Warehouse { Id = 5, Name = "Zahraniční sklad - Berlín", Address = "Lagerstraße 17, Berlin, Německo" }
        );


        // Metody dopravy
        modelBuilder.Entity<ShippingMethod>().HasData(
            new ShippingMethod
            {
                Id = 1, Name = "Česká pošta", Description = "Standardní doručení Českou poštou", Price = 89m,
                EstimatedDeliveryDays = 3
            },
            new ShippingMethod
            {
                Id = 2, Name = "Zásilkovna", Description = "Výdejní místa Zásilkovny po celé ČR", Price = 79m,
                EstimatedDeliveryDays = 2
            },
            new ShippingMethod
            {
                Id = 3, Name = "GLS", Description = "Expresní doručení GLS kurýrem", Price = 119m,
                EstimatedDeliveryDays = 1
            },
            new ShippingMethod
            {
                Id = 4, Name = "Osobní odběr", Description = "Osobní odběr na pobočce", Price = 0m,
                EstimatedDeliveryDays = 0
            }
        );


        // Produkty
        modelBuilder.Entity<Product>().HasData(
            // Kategorie 1 - Mobilní telefony
            new Product
            {
                Id = 1, Name = "iPhone 15 Pro", Description = "Nejnovější Apple iPhone", CategoryId = 1, BrandId = 1,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 2, Name = "Samsung Galaxy S23", Description = "Vlajkový model Samsungu", CategoryId = 1,
                BrandId = 2, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 2 - Notebooky a PC
            new Product
            {
                Id = 3, Name = "Dell XPS 13", Description = "Lehký a výkonný notebook Dell", CategoryId = 2,
                BrandId = 5, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 4, Name = "Asus ROG Strix", Description = "Herní notebook Asus", CategoryId = 2, BrandId = 9,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 3 - Bezpečnostní technika
            new Product
            {
                Id = 5, Name = "Reolink Argus 3", Description = "Bezdrátová bezpečnostní kamera", CategoryId = 3,
                BrandId = 3, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 6, Name = "Hikvision DS-2CD", Description = "Profesionální IP kamera", CategoryId = 3, BrandId = 7,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 4 - Sportovní vybavení
            new Product
            {
                Id = 7, Name = "Nike Air Zoom", Description = "Sportovní běžecké boty", CategoryId = 4, BrandId = 4,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 8, Name = "Adidas T-shirt", Description = "Pohodlné sportovní tričko", CategoryId = 4, BrandId = 4,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 5 - Móda a oblečení
            new Product
            {
                Id = 9, Name = "H&M Jeans", Description = "Stylové džíny", CategoryId = 5, BrandId = 10,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 10, Name = "H&M Svetr", Description = "Teplý svetr na zimu", CategoryId = 5, BrandId = 10,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 6 - Hračky a stavebnice
            new Product
            {
                Id = 11, Name = "LEGO City Police", Description = "Policie stavebnice", CategoryId = 6, BrandId = 8,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 12, Name = "LEGO Technic", Description = "Technická stavebnice pro pokročilé", CategoryId = 6,
                BrandId = 8, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 7 - TV a audio
            new Product
            {
                Id = 13, Name = "Sony Bravia 55\"", Description = "4K UHD televizor", CategoryId = 7, BrandId = 7,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 14, Name = "Bose SoundLink", Description = "Přenosný Bluetooth reproduktor", CategoryId = 7,
                BrandId = 6, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 8 - Fotoaparáty a kamery
            new Product
            {
                Id = 15, Name = "GoPro HERO11", Description = "Akční kamera nové generace", CategoryId = 8, BrandId = 6,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 16, Name = "Canon EOS R6", Description = "Profesionální bezzrcadlovka", CategoryId = 8,
                BrandId = 7, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 9 - Herní zóna
            new Product
            {
                Id = 17, Name = "PlayStation 5", Description = "Nová herní konzole Sony", CategoryId = 9, BrandId = 7,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 18, Name = "Xbox Series X", Description = "Výkonná konzole Microsoft", CategoryId = 9, BrandId = 9,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },

            // Kategorie 10 - Domácnost a chytrá zařízení
            new Product
            {
                Id = 19, Name = "Philips Hue Starter Kit", Description = "Chytré osvětlení do domácnosti",
                CategoryId = 10, BrandId = 7, IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            },
            new Product
            {
                Id = 20, Name = "Google Nest Hub", Description = "Chytrý domácí asistent", CategoryId = 10, BrandId = 7,
                IsActive = true, CreatedAt = new DateTime(2025, 7, 19)
            }
        );


        // Produktové štítky
        modelBuilder.Entity<ProductTag>().HasData(
            new ProductTag { ProductId = 1, TagId = 1 }, // iPhone 15 Pro - Novinka
            new ProductTag { ProductId = 2, TagId = 2 }, // Samsung Galaxy S23 - Akce
            new ProductTag { ProductId = 3, TagId = 3 }, // Dell XPS 13 - Doprava zdarma
            new ProductTag { ProductId = 4, TagId = 4 }, // Asus ROG Strix - Bestseller
            new ProductTag { ProductId = 5, TagId = 5 }, // Reolink Argus 3 - Výprodej
            new ProductTag { ProductId = 6, TagId = 6 } // Nike Air Zoom - Prémiové
        );


        // Varianty produktů
        modelBuilder.Entity<ProductVariant>().HasData(
            new ProductVariant
            {
                Id = 1, ProductId = 1, Sku = "IP15PRO-128GB-BLK", Price = 29999m, DiscountPercentage = 0,
                StockQuantity = 50
            },
            new ProductVariant
            {
                Id = 2, ProductId = 1, Sku = "IP15PRO-256GB-WHT", Price = 34999m, DiscountPercentage = 5,
                StockQuantity = 30
            },
            new ProductVariant
            {
                Id = 3, ProductId = 2, Sku = "SGS23-128GB-GRN", Price = 24999m, DiscountPercentage = 0,
                StockQuantity = 70
            },
            new ProductVariant
            {
                Id = 4, ProductId = 2, Sku = "SGS23-256GB-PNK", Price = 27999m, DiscountPercentage = 10,
                StockQuantity = 40
            },
            new ProductVariant
            {
                Id = 5, ProductId = 3, Sku = "DXPS13-I7-16GB", Price = 34999m, DiscountPercentage = 0,
                StockQuantity = 25
            },
            new ProductVariant
            {
                Id = 6, ProductId = 4, Sku = "NIKE-AIRZOOM-42", Price = 2999m, DiscountPercentage = 0,
                StockQuantity = 120
            },
            new ProductVariant
            {
                Id = 7, ProductId = 4, Sku = "NIKE-AIRZOOM-44", Price = 2999m, DiscountPercentage = 0,
                StockQuantity = 100
            },
            new ProductVariant
            {
                Id = 8, ProductId = 5, Sku = "REOLINK-ARGUS3-BLK", Price = 3999m, DiscountPercentage = 0,
                StockQuantity = 100
            },
            new ProductVariant
            {
                Id = 9, ProductId = 6, Sku = "ADIDAS-TSHIRT-M", Price = 599m, DiscountPercentage = 0,
                StockQuantity = 200
            },
            new ProductVariant
            {
                Id = 10, ProductId = 7, Sku = "LEGO-CITY-POLICE", Price = 1499m, DiscountPercentage = 0,
                StockQuantity = 300
            }
        );

        // Obrázky produktů
        modelBuilder.Entity<ProductVariantImage>().HasData(
            new ProductVariantImage
                { Id = 1, ProductVariantId = 1, ImageUrl = "/images/variants/iphone15pro_black.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 2, ProductVariantId = 2, ImageUrl = "/images/variants/iphone15pro_white.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 3, ProductVariantId = 3, ImageUrl = "/images/variants/galaxys23_green.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 4, ProductVariantId = 4, ImageUrl = "/images/variants/galaxys23_pink.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 5, ProductVariantId = 5, ImageUrl = "/images/variants/dellxps13.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 6, ProductVariantId = 6, ImageUrl = "/images/variants/nike_air_zoom_42.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 7, ProductVariantId = 7, ImageUrl = "/images/variants/nike_air_zoom_44.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 8, ProductVariantId = 8, ImageUrl = "/images/variants/reolink_argus3.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 9, ProductVariantId = 9, ImageUrl = "/images/variants/adidas_tshirt.jpg", IsMain = true },
            new ProductVariantImage
                { Id = 10, ProductVariantId = 10, ImageUrl = "/images/variants/lego_city_police.jpg", IsMain = true }
        );

        // Uživatelé s předem vygenerovanými hash hesly
        modelBuilder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = "e067f58c-77d3-4c91-9e99-65e56a479bed",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Test",
                AdressLine1 = "123 Main St",
                AdressLine2 = "Apt 4B",
                City = "Prague",
                PostalCode = "11000",
                Country = "Czech Republic",
                SecurityStamp = "e067f58c-77d3-4c91-9e99-65e56a479bed",
                PasswordHash = "AQAAAAIAAYagAAAAEKpJ0s7HKbFjJUuPiqdsDMlqvn8F4CZFbELwW5JLBpKvGF3HU8kJ5Q4Z6Hq8tQ2m9Q==" // Admin123!
            },
            new AppUser
            {
                Id = "f067f58c-77d3-4c91-9e99-65e56a479bed",
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.COM",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                FirstName = "User",
                LastName = "Test",
                AdressLine1 = "456 Oak Street",
                AdressLine2 = null,
                City = "Brno",
                PostalCode = "60200",
                Country = "Czech Republic",
                SecurityStamp = "f067f58c-77d3-4c91-9e99-65e56a479bed",
                PasswordHash = "AQAAAAIAAYagAAAAEDcWJHQu0zQ9TiIYwJK1AUteBrK6YNbN2h5oeXZG9Kkn7RHFDMIQg3fT9RqUrcpFMA==" // User123!
            }
        );
        
        // Role
        modelBuilder.Entity<AppRole>().HasData(
            new AppRole
            {
                Id = "e267f58c-77d3-4c91-9e99-65e56a479bed",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Id = "f267a58c-77d3-4c91-9e99-65e56a479bed",
                Name = "User",
                NormalizedName = "USER"
            }
        );
        
        // Přiřazení rolí uživatelům
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "e067f58c-77d3-4c91-9e99-65e56a479bed",
                RoleId = "e267f58c-77d3-4c91-9e99-65e56a479bed"
            },
            new IdentityUserRole<string>
            {
                UserId = "f067f58c-77d3-4c91-9e99-65e56a479bed",
                RoleId = "f267a58c-77d3-4c91-9e99-65e56a479bed"
            }
        );
        
        // Skladové zásoby
        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { Id = 1, ProductVariantId = 1, WarehouseId = 1, Quantity = 50 }, 
            new Inventory { Id = 2, ProductVariantId = 2, WarehouseId = 1, Quantity = 0 },   
            new Inventory { Id = 3, ProductVariantId = 3, WarehouseId = 1, Quantity = 70 },  
            new Inventory { Id = 4, ProductVariantId = 4, WarehouseId = 2, Quantity = 40 },  
            new Inventory { Id = 5, ProductVariantId = 5, WarehouseId = 2, Quantity = 25 },  
            new Inventory { Id = 6, ProductVariantId = 6, WarehouseId = 3, Quantity = 120 }, 
            new Inventory { Id = 7, ProductVariantId = 7, WarehouseId = 3, Quantity = 100 }

        );
        
        // Objednávky
        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CustomerId = "e067f58c-77d3-4c91-9e99-65e56a479bed", OrderDate = new DateTime(2025, 7, 1, 14, 30, 0), TotalAmount = 64998m, ShippingMethodId = 1 },
            new Order { Id = 2, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed", OrderDate = new DateTime(2025, 7, 3, 9, 0, 0), TotalAmount = 3299m, ShippingMethodId = 2 },
            new Order { Id = 3, CustomerId = "e067f58c-77d3-4c91-9e99-65e56a479bed", OrderDate = new DateTime(2025, 7, 5, 16, 45, 0), TotalAmount = 1499m, ShippingMethodId = 3 },
            new Order { Id = 4, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed", OrderDate = new DateTime(2025, 7, 6, 11, 15, 0), TotalAmount = 29999m, ShippingMethodId = 1 },
            new Order { Id = 5, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed", OrderDate = new DateTime(2025, 7, 7, 18, 0, 0), TotalAmount = 599m, ShippingMethodId = 2 }
        );
        
        // Položky objednávek
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem { Id = 1, OrderId = 1, ProductVariantId = 1, Quantity = 2, UnitPrice = 29999m }, // iPhone 15 Pro 128GB černý
            new OrderItem { Id = 2, OrderId = 2, ProductVariantId = 6, Quantity = 1, UnitPrice = 2999m }, // Nike Air Zoom vel.42
            new OrderItem { Id = 3, OrderId = 3, ProductVariantId = 10, Quantity = 1, UnitPrice = 1499m }, // Lego City Police
            new OrderItem { Id = 4, OrderId = 4, ProductVariantId = 2, Quantity = 1, UnitPrice = 34999m }, // iPhone 15 Pro 256GB bílý
            new OrderItem { Id = 5, OrderId = 5, ProductVariantId = 9, Quantity = 1, UnitPrice = 599m } // Adidas tričko M
        );
        
        // Vrátné žádosti
        modelBuilder.Entity<RefundRequest>().HasData(
            new RefundRequest { Id = 1, OrderItemId = 2, RequestedAt = new DateTime(2025, 7, 10, 12, 0, 0), Reason = "Produkt byl poškozený při doručení", Status = RefundStatus.Pending },
            new RefundRequest { Id = 2, OrderItemId = 4, RequestedAt = new DateTime(2025, 7, 11, 15, 30, 0), Reason = "Nesedí velikost", Status = RefundStatus.Approved },
            new RefundRequest { Id = 3, OrderItemId = 5, RequestedAt = new DateTime(2025, 7, 12, 9, 45, 0), Reason = "Neodpovídá popisu", Status = RefundStatus.Rejected },
            new RefundRequest { Id = 4, OrderItemId = 1, RequestedAt = new DateTime(2025, 7, 13, 18, 20, 0), Reason = "Změna rozhodnutí", Status = RefundStatus.Processed }
        );
        
        // Recenze
        modelBuilder.Entity<Review>().HasData(
            new Review { Id = 1, Rating = 5, Comment = "Skvělý telefon, výborná výdrž baterie.", CreatedAt = new DateTime(2025, 7, 2), ProductId = 1, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed"},
            new Review { Id = 3, Rating = 3, Comment = "Funguje, ale občas se seká.", CreatedAt = new DateTime(2025, 7, 4), ProductId = 2, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed" },
            new Review { Id = 4, Rating = 5, Comment = "Úžasné sluchátka, perfektní zvuk.", CreatedAt = new DateTime(2025, 7, 5), ProductId = 5, CustomerId = "f067f58c-77d3-4c91-9e99-65e56a479bed" },
            new Review { Id = 5, Rating = 2, Comment = "Velikost neodpovídá, vracím.", CreatedAt = new DateTime(2025, 7, 6), ProductId = 6, CustomerId = "e067f58c-77d3-4c91-9e99-65e56a479bed" }
        );

        



        
        
        
    }





}