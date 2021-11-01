﻿// <auto-generated />
using System;
using GestionPersonas.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionPersonas.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20211101025937_Migracion_Inicial")]
    partial class Migracion_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("GestionPersonas.Entidades.AporteDetalle", b =>
                {
                    b.Property<int>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AporteId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int?>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TipoAporteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdDetalle");

                    b.HasIndex("AporteId");

                    b.HasIndex("PersonaId");

                    b.HasIndex("TipoAporteId");

                    b.ToTable("AporteDetalle");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Aportes", b =>
                {
                    b.Property<int>("AporteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Concepto")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TipoAporteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AporteId");

                    b.HasIndex("TipoAporteId");

                    b.ToTable("Aportes");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Grupos", b =>
                {
                    b.Property<int>("GrupoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CantidadPersonas")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.HasKey("GrupoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.GruposDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GrupoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Orden")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("PersonaId");

                    b.ToTable("GruposDetalle");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Personas", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CantidadGrupos")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cedula")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RolId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<float>("TotalAportado")
                        .HasColumnType("REAL");

                    b.HasKey("PersonaId");

                    b.HasIndex("RolId");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Roles", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.TipoAporte", b =>
                {
                    b.Property<int>("TipoAporteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("BaseMonto")
                        .HasColumnType("REAL");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<float>("MetaMonto")
                        .HasColumnType("REAL");

                    b.HasKey("TipoAporteId");

                    b.ToTable("TipoAporte");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.AporteDetalle", b =>
                {
                    b.HasOne("GestionPersonas.Entidades.Aportes", null)
                        .WithMany("AporteDetalle")
                        .HasForeignKey("AporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPersonas.Entidades.Personas", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId");

                    b.HasOne("GestionPersonas.Entidades.TipoAporte", "TipoAporte")
                        .WithMany()
                        .HasForeignKey("TipoAporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");

                    b.Navigation("TipoAporte");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Aportes", b =>
                {
                    b.HasOne("GestionPersonas.Entidades.TipoAporte", "TipoAportes")
                        .WithMany()
                        .HasForeignKey("TipoAporteId");

                    b.Navigation("TipoAportes");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.GruposDetalle", b =>
                {
                    b.HasOne("GestionPersonas.Entidades.Grupos", null)
                        .WithMany("Detalle")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionPersonas.Entidades.Personas", "Persona")
                        .WithMany()
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Personas", b =>
                {
                    b.HasOne("GestionPersonas.Entidades.Roles", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Aportes", b =>
                {
                    b.Navigation("AporteDetalle");
                });

            modelBuilder.Entity("GestionPersonas.Entidades.Grupos", b =>
                {
                    b.Navigation("Detalle");
                });
#pragma warning restore 612, 618
        }
    }
}
