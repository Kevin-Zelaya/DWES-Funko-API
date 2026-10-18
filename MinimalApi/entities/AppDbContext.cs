using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Funko> Funkos {get; set;}
    public DbSet<Supplier> Suppliers {get; set;}
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funko>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();

            ///<summary> Nombre como indice </summary>
            entity.HasIndex(p => p.Name)
            .IsUnique();
            entity.Property(p => p.Name)
            .HasMaxLength(100);

            ///<summary> Franquicia </summary>
            entity.Property(p => p.Franchise).HasMaxLength(100);
            ///<summary> número </summary>
            entity.Property(p => p.Number).IsRequired();
            ///<summary> Exclusivo valor por defecto false </summary>
            entity.Property(p => p.Exclusive).HasDefaultValue(false);
            ///<summary> Precio requerido </summary>
            entity.Property(p => p.Price).IsRequired(true);
            ///<summary> Eliminado, valor por defecto false </summary>
            entity.Property(p => p.IsDelete).HasDefaultValue(false);
            ///<summary> Fecha de creado </summary>
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de actualizado </summary>
            entity.Property(p => p.UpdatedAt).IsRequired(false).HasDefaultValue(null);
            ///<summary> Fecha de eliminado </summary>
            entity.Property(p => p.DeleteAT).IsRequired(false).HasDefaultValue(null);
            //<summary> Conversión de categoría a json </summary>
            entity.ComplexProperty(f => f.Category, category => category.ToJson());

            //<summary> Relación con categoria </summary>
            entity.HasOne(f => f.Supplier)
                .WithMany(s => s.Funkos)
                .HasForeignKey(f => f.SupplierId);

            entity.HasQueryFilter(f => !f.IsDelete);
        });
        modelBuilder.Entity<Supplier>(entity =>
        {
            ///<summary> Key de categoria se genera automaticamente </summary>
            entity.HasKey(s => s.Id);
            entity.Property(s=> s.Id).ValueGeneratedOnAdd();
            ///<summary> 
            ///     Nombre como indice 
            ///     requerido y largo máximo de 100
            /// </summary>
            entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(s => s.Name);
            ///<summary> Eliminado lógico, falso por defecto </summary>
            entity.Property(s=>s.IsDeleted).HasDefaultValue(false);
            ///<summary> Fecha de creación valor por defecto "ahora" </summary>
            entity.Property(s=>s.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de actualización valor por defecto "ahora" </summary>
            entity.Property(s=>s.UpdateAt).IsRequired(false).HasDefaultValue(null);
            ///<summary> Fecha de eliminado </summary>
            entity.Property(s => s.DeleteAt).IsRequired(false).HasDefaultValue(null);
            ///<summary> Filtro </summary>
            entity.HasQueryFilter(f => !f.IsDeleted);

            ///<summary> Semíllas </summary>
            entity.HasData(
                new Supplier{Id = 1, Name = "Funko Europe", IsDeleted = false},
                new Supplier{Id = 2, Name = "Collector Store", IsDeleted = false});
        });

    }
}