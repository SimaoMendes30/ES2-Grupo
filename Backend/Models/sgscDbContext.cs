using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class sgscDbContext : DbContext
{
    public sgscDbContext()
    {
    }

    public sgscDbContext(DbContextOptions<sgscDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Membro> Membros { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    public virtual DbSet<Tarefa> Tarefas { get; set; }

    public virtual DbSet<Utilizador> Utilizadors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Membro>(entity =>
        {
            entity.HasKey(e => e.IdMembro).HasName("Membro_pkey");

            entity.ToTable("Membro");

            entity.HasIndex(e => e.IdProjeto, "idx_membro_projeto");

            entity.HasIndex(e => e.IdUtilizador, "idx_membro_utilizador");

            entity.Property(e => e.IdMembro).HasColumnName("id_membro");
            entity.Property(e => e.DataConvite)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("data_convite");
            entity.Property(e => e.DataEstado).HasColumnName("data_estado");
            entity.Property(e => e.EstadoAtividade)
                .HasMaxLength(250)
                .HasColumnName("estado_atividade");
            entity.Property(e => e.EstadoConvite)
                .HasMaxLength(250)
                .HasColumnName("estado_convite");
            entity.Property(e => e.IdProjeto).HasColumnName("id_projeto");
            entity.Property(e => e.IdUtilizador).HasColumnName("id_utilizador");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Membros)
                .HasForeignKey(d => d.IdProjeto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Membro_id_projeto_fkey");

            entity.HasOne(d => d.IdUtilizadorNavigation).WithMany(p => p.Membros)
                .HasForeignKey(d => d.IdUtilizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Membro_id_utilizador_fkey");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.IdProjeto).HasName("Projeto_pkey");

            entity.ToTable("Projeto");

            entity.HasIndex(e => e.IdUtilizador, "idx_projeto_utilizador");

            entity.Property(e => e.IdProjeto).HasColumnName("id_projeto");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.IdUtilizador).HasColumnName("id_utilizador");
            entity.Property(e => e.Nome)
                .HasMaxLength(250)
                .HasColumnName("nome");
            entity.Property(e => e.NomeCliente)
                .HasMaxLength(250)
                .HasColumnName("nome_cliente");
            entity.Property(e => e.PrecoHora).HasColumnName("preco_hora");

            entity.HasOne(d => d.IdUtilizadorNavigation).WithMany(p => p.Projetos)
                .HasForeignKey(d => d.IdUtilizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Projeto_id_utilizador_fkey");
        });

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.IdTarefa).HasName("Tarefa_pkey");

            entity.ToTable("Tarefa");

            entity.Property(e => e.IdTarefa).HasColumnName("id_tarefa");
            entity.Property(e => e.DataFim).HasColumnName("data_fim");
            entity.Property(e => e.DataHoraInicio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_hora_inicio");
            entity.Property(e => e.DataInicio)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("data_inicio");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.Estado)
                .HasMaxLength(250)
                .HasColumnName("estado");
            entity.Property(e => e.PrecoHora).HasColumnName("preco_hora");
            entity.Property(e => e.Responsavel).HasColumnName("responsavel");

            entity.HasMany(d => d.IdProjetos).WithMany(p => p.IdTarefas)
                .UsingEntity<Dictionary<string, object>>(
                    "TarefaProjeto",
                    r => r.HasOne<Projeto>().WithMany()
                        .HasForeignKey("IdProjeto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Tarefa_Projeto_id_projeto_fkey"),
                    l => l.HasOne<Tarefa>().WithMany()
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Tarefa_Projeto_id_tarefa_fkey"),
                    j =>
                    {
                        j.HasKey("IdTarefa", "IdProjeto").HasName("Tarefa_Projeto_pkey");
                        j.ToTable("Tarefa_Projeto");
                        j.HasIndex(new[] { "IdProjeto" }, "idx_tarefa_projeto_projeto");
                        j.HasIndex(new[] { "IdTarefa" }, "idx_tarefa_projeto_tarefa");
                        j.IndexerProperty<int>("IdTarefa").HasColumnName("id_tarefa");
                        j.IndexerProperty<int>("IdProjeto").HasColumnName("id_projeto");
                    });
        });

        modelBuilder.Entity<Utilizador>(entity =>
        {
            entity.HasKey(e => e.IdUtilizador).HasName("Utilizador_pkey");

            entity.ToTable("Utilizador");

            entity.HasIndex(e => e.Username, "Utilizador_username_key").IsUnique();

            entity.Property(e => e.IdUtilizador).HasColumnName("id_utilizador");
            entity.Property(e => e.Admin)
                .HasDefaultValue(false)
                .HasColumnName("admin");
            entity.Property(e => e.Nome)
                .HasMaxLength(250)
                .HasColumnName("nome");
            entity.Property(e => e.NumHoras).HasColumnName("num_horas");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.SuperUser)
                .HasDefaultValue(false)
                .HasColumnName("super_user");
            entity.Property(e => e.Username)
                .HasMaxLength(250)
                .HasColumnName("username");

            entity.HasMany(d => d.IdTarefas).WithMany(p => p.IdUtilizadors)
                .UsingEntity<Dictionary<string, object>>(
                    "TarefaUtilizador",
                    r => r.HasOne<Tarefa>().WithMany()
                        .HasForeignKey("IdTarefa")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Tarefa_Utilizador_id_tarefa_fkey"),
                    l => l.HasOne<Utilizador>().WithMany()
                        .HasForeignKey("IdUtilizador")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Tarefa_Utilizador_id_utilizador_fkey"),
                    j =>
                    {
                        j.HasKey("IdUtilizador", "IdTarefa").HasName("Tarefa_Utilizador_pkey");
                        j.ToTable("Tarefa_Utilizador");
                        j.HasIndex(new[] { "IdTarefa" }, "idx_tarefa_utilizador_tarefa");
                        j.HasIndex(new[] { "IdUtilizador" }, "idx_tarefa_utilizador_utilizador");
                        j.IndexerProperty<int>("IdUtilizador").HasColumnName("id_utilizador");
                        j.IndexerProperty<int>("IdTarefa").HasColumnName("id_tarefa");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
