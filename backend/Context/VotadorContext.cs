using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Context
{
    public class VotadorContext : DbContext
    {
        public VotadorContext()
        {
        }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Recurso> Recursos { get; set; }
        public virtual DbSet<Voto> Votos { get; set; }
        public DbQuery<RecursoVotado> RecursoVotados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppConfiguration config = new AppConfiguration();
            string connection = config.ConnectionString;
            optionsBuilder.UseNpgsql(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios", "public");
                entity.Property(e => e.ID)
                                    .HasColumnName("id");
                entity.Property(e => e.Nome)
                                    .IsRequired()
                                    .HasColumnName("nome");
                entity.Property(e => e.Senha)
                                    .IsRequired()
                                    .HasColumnName("senha");
                entity.Property(e => e.Email)
                                    .IsRequired()
                                    .HasColumnName("email");
                entity.Property(e => e.Perfil)
                                    .IsRequired()
                                    .HasColumnName("perfil");
            });

            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.ToTable("recursos", "public");
                entity.Property(e => e.ID)
                                    .HasColumnName("id");
                entity.Property(e => e.Descricao)
                                    .IsRequired()
                                    .HasColumnName("descricao");
            });

            modelBuilder.Entity<Voto>(entity =>
            {
                entity.ToTable("votos", "public");
                entity.Property(e => e.ID)
                                    .HasColumnName("id");
                entity.Property(e => e.UsuarioID)
                                    .IsRequired()
                                    .HasColumnName("usuario_id");
                entity.Property(e => e.RecursoID)
                                    .IsRequired()
                                    .HasColumnName("recurso_id");
                entity.Property(e => e.Comentario)
                                    .IsRequired()
                                    .HasColumnName("comentario");
                entity.Property(e => e.CreatedAt)
                                    .IsRequired()
                                    .HasColumnName("created_at");
            });

            modelBuilder.Entity<Voto>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Votos);
            modelBuilder.Entity<Voto>()
                .HasOne(v => v.Recurso)
                .WithMany(u => u.Votos);
        }
    }
}
