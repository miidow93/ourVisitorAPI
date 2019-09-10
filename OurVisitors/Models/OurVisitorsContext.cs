using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OurVisitors.Models
{
    public partial class OurVisitorsContext : DbContext
    {
        public OurVisitorsContext()
        {
        }

        public OurVisitorsContext(DbContextOptions<OurVisitorsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Regle> Regle { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Societe> Societe { get; set; }
        public virtual DbSet<SousTraitant> SousTraitant { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Visiteur> Visiteur { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=OurVisitor;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Regle>(entity =>
            {
                entity.ToTable("regle");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumOrdre).HasColumnName("numOrdre");

                entity.Property(e => e.Show)
                    .HasColumnName("show")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libelle)
                    .HasColumnName("libelle")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("service");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NomService)
                    .HasColumnName("nomService")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Societe>(entity =>
            {
                entity.ToTable("societe");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NomSociete)
                    .HasColumnName("nomSociete")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SousTraitant>(entity =>
            {
                entity.ToTable("sousTraitant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CinCnss)
                    .HasColumnName("cin_cnss")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DateVisite).HasColumnType("date");

                entity.Property(e => e.HeureEntree).HasColumnName("heureEntree");

                entity.Property(e => e.HeureSortie).HasColumnName("heureSortie");

                entity.Property(e => e.IdSociete).HasColumnName("idSociete");

                entity.Property(e => e.NomComplet)
                    .HasColumnName("nomComplet")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumBadge).HasColumnName("numBadge");

                entity.Property(e => e.Prestation)
                    .HasColumnName("prestation")
                    .IsUnicode(false);

                entity.Property(e => e.Superviseur)
                    .HasColumnName("superviseur")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSocieteNavigation)
                    .WithMany(p => p.SousTraitant)
                    .HasForeignKey(d => d.IdSociete)
                    .HasConstraintName("FK__sousTrait__idSoc__5CD6CB2B");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.NomComplet)
                    .HasColumnName("nomComplet")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK__users__id_role__4222D4EF");
            });

            modelBuilder.Entity<Visiteur>(entity =>
            {
                entity.ToTable("visiteur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CinCnss)
                    .HasColumnName("cin_cnss")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DateVisite).HasColumnType("date");

                entity.Property(e => e.HeureEntree).HasColumnName("heureEntree");

                entity.Property(e => e.HeureSortie).HasColumnName("heureSortie");

                entity.Property(e => e.IdSociete).HasColumnName("idSociete");

                entity.Property(e => e.NomComplet)
                    .HasColumnName("nomComplet")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumBadge).HasColumnName("numBadge");

                entity.Property(e => e.PersonneService)
                    .HasColumnName("personne_service")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSocieteNavigation)
                    .WithMany(p => p.Visiteur)
                    .HasForeignKey(d => d.IdSociete)
                    .HasConstraintName("FK__visiteur__idSoci__4316F928");
            });
        }
    }
}
