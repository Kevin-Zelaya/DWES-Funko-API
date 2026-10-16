using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Funko> Funkos {get; set;}
    public DbSet<Category> Categories {get; set;}
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
            entity.HasIndex(p => p.Nombre)
            .IsUnique();
            entity.Property(p => p.Nombre)
            .HasMaxLength(100);

            ///<summary> Franquicia </summary>
            entity.Property(p => p.Franquicia).HasMaxLength(100);
            ///<summary> número </summary>
            entity.Property(p => p.Numero).IsRequired();
            ///<summary> Exclusivo valor por defecto false </summary>
            entity.Property(p => p.Exclusivo).HasDefaultValue(false);
            ///<summary> Precio requerido </summary>
            entity.Property(p => p.Precio).IsRequired(true);
            ///<summary> Eliminado, valor por defecto false </summary>
            entity.Property(p => p.IsDelete).HasDefaultValue(false);
            ///<summary> Fecha de creado </summary>
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de actualizado </summary>
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de eliminado </summary>
            entity.Property(p => p.DeleteAT).HasDefaultValue(null);
            //<summary> Relación con categoria </summary>
            entity.HasOne(f => f.Categoria)
                .WithMany(c => c.Funkos)
                .HasForeignKey(f => f.categoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(f => !f.IsDelete);
        });
        modelBuilder.Entity<Category>(entity =>
        {
            ///<summary> Key de categoria se genera automaticamente </summary>
            entity.HasKey(c => c.Id);
            entity.Property(c=> c.Id).ValueGeneratedOnAdd();
            ///<summary> 
            ///     Nombre como indice 
            ///     requerido y largo máximo de 100
            /// </summary>
            entity.Property(c=>c.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(c => c.Name);
            ///<summary> Eliminado lógico, falso por defecto </summary>
            entity.Property(c=>c.IsDeleted).HasDefaultValue(false);
            ///<summary> Fecha de creación valor por defecto "ahora" </summary>
            entity.Property(c=>c.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de actualización valor por defecto "ahora" </summary>
            entity.Property(c=>c.UpdateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            ///<summary> Fecha de eliminado </summary>
            entity.Property(p => p.DeleteAt).HasDefaultValue(null);

            entity.HasQueryFilter(f => !f.IsDeleted);

        });
    }
}