﻿// <auto-generated />
using System;
using Architecture.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Architecture.Infra.Data.Migrations
{
    [DbContext(typeof(ArchitectureDbContext))]
    [Migration("20231007184618_Start1q")]
    partial class Start1q
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Architecture.Application.Domain.DbContexts.Domains.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cep")
                        .HasColumnType("longtext");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext");

                    b.Property<int>("Situacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Architecture.Application.Domain.DbContexts.Domains.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<Guid>("EnderecoId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Situacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Architecture.Application.Domain.DbContexts.Domains.Pessoa", b =>
                {
                    b.HasOne("Architecture.Application.Domain.DbContexts.Domains.Endereco", "Endereco")
                        .WithOne("Pessoa")
                        .HasForeignKey("Architecture.Application.Domain.DbContexts.Domains.Pessoa", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Architecture.Application.Domain.DbContexts.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<Guid>("PessoaId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("PrimeiroNome")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("PrimeiroNome");

                            b1.Property<string>("Sobrenome")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("SobreNome");

                            b1.HasKey("PessoaId");

                            b1.ToTable("Pessoas");

                            b1.WithOwner()
                                .HasForeignKey("PessoaId");
                        });

                    b.Navigation("Endereco");

                    b.Navigation("Nome");
                });

            modelBuilder.Entity("Architecture.Application.Domain.DbContexts.Domains.Endereco", b =>
                {
                    b.Navigation("Pessoa");
                });
#pragma warning restore 612, 618
        }
    }
}
