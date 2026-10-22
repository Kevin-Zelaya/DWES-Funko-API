using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using Serilog;

public static class MongoContextConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        Log.Debug(
            "[DBCONTEXT-CONFIG] Configurando entidades para MongoDB");

        // FUNKO
        modelBuilder.Entity<Funko>(entity =>
        {
            entity.ToCollection("funkos");

            entity.HasKey(f => f.Id);

            entity.Property(f => f.Id)
                .HasElementName("_id");

            entity.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(f => f.Name)
                .IsUnique();

            entity.Property(f => f.Franchise)
                .HasMaxLength(100);

            entity.Property(f => f.Number)
                .IsRequired();

            entity.Property(f => f.Price)
                .IsRequired();

            entity.Property(f => f.Exclusive)
                .HasDefaultValue(false);

            entity.Property(f => f.IsDelete)
                .HasDefaultValue(false);

            entity.Property(f => f.CreatedAt)
                .IsRequired();

            entity.Property(f => f.UpdatedAt)
                .IsRequired(false);

            entity.Property(f => f.DeleteAT)
                .IsRequired(false);

            entity.HasQueryFilter(f => !f.IsDelete);
        });

        // SUPPLIER
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToCollection("suppliers");

            entity.HasKey(s => s.Id);

            entity.Property(s => s.Id)
                .HasElementName("_id");

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(s => s.Name);

            entity.Property(s => s.IsDeleted)
                .HasDefaultValue(false);

            entity.Property(s => s.CreateAt)
                .IsRequired();

            entity.Property(s => s.UpdateAt)
                .IsRequired(false);

            entity.Property(s => s.DeleteAt)
                .IsRequired(false);

            entity.HasQueryFilter(s => !s.IsDeleted);

            if(!AppConfig.shouldRecreateDb) return;
            entity.HasData(
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Funko Europe", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Collector Store", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Pop In A Box", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Entertainment Earth", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "BigBadToyStore", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Amazon Collectibles", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Geek Store", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Comic World", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Pop Culture Shop", IsDeleted = false },
                new Supplier { Id = ObjectId.GenerateNewId().ToString(), Name = "Collector's Corner", IsDeleted = false }
            );

            Log.Information(
                "[MONGODB] Se han insertado {Count} proveedores iniciales",
                10);
        });

        
    }

}