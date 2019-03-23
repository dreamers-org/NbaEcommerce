using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NbaEcommerce.Models
{
    public partial class NbaStoreContext : DbContext
    {
        public NbaStoreContext()
        {
        }

        public NbaStoreContext(DbContextOptions<NbaStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Immagine> Immagine { get; set; }
        public virtual DbSet<Marchio> Marchio { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=loft1mvc.database.windows.net;Initial Catalog=NbaStore;User ID=luca.bellavia.dev;Password=Pallone27@@;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Immagine>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.ImageInfo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProdottoNavigation)
                    .WithMany(p => p.Immagine)
                    .HasForeignKey(d => d.IdProdotto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Immagine_Prodotto");
            });

            modelBuilder.Entity<Marchio>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(8000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prodotto>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Prodotto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prodotto_Categoria1");

                entity.HasOne(d => d.IdMarchioNavigation)
                    .WithMany(p => p.Prodotto)
                    .HasForeignKey(d => d.IdMarchio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prodotto_Marchio1");
            });
        }
    }
}
