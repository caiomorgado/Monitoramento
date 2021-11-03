﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Monitoramento.Infra.Data.Context;

namespace Monitoramento.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Monitoramento.Domain.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acao")
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Aplicacao")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalhe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Excecao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestHttp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponseHttp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Usuario")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Versao")
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });
#pragma warning restore 612, 618
        }
    }
}
