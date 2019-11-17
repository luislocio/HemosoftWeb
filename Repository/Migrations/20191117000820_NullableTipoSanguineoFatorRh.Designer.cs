﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20191117000820_NullableTipoSanguineoFatorRh")]
    partial class NullableTipoSanguineoFatorRh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Doacao", b =>
                {
                    b.Property<int>("IdDoacao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataDoacao");

                    b.Property<int?>("DoadorIdDoador");

                    b.Property<int?>("IdImpedimentosDefinitivos");

                    b.Property<int?>("IdImpedimentosTemporarios");

                    b.Property<int?>("IdTriagemClinica");

                    b.Property<int?>("IdTriagemLaboratorial");

                    b.Property<int?>("SolicitacaoIdSolicitacao");

                    b.Property<int>("StatusDoacao");

                    b.Property<int?>("TriadorIdTriador");

                    b.HasKey("IdDoacao");

                    b.HasIndex("DoadorIdDoador");

                    b.HasIndex("IdImpedimentosDefinitivos")
                        .IsUnique()
                        .HasFilter("[IdImpedimentosDefinitivos] IS NOT NULL");

                    b.HasIndex("IdImpedimentosTemporarios")
                        .IsUnique()
                        .HasFilter("[IdImpedimentosTemporarios] IS NOT NULL");

                    b.HasIndex("IdTriagemClinica")
                        .IsUnique()
                        .HasFilter("[IdTriagemClinica] IS NOT NULL");

                    b.HasIndex("IdTriagemLaboratorial")
                        .IsUnique()
                        .HasFilter("[IdTriagemLaboratorial] IS NOT NULL");

                    b.HasIndex("SolicitacaoIdSolicitacao");

                    b.HasIndex("TriadorIdTriador");

                    b.ToTable("Doacoes");
                });

            modelBuilder.Entity("Domain.Models.Doador", b =>
                {
                    b.Property<int>("IdDoador")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<int>("EstadoCivil");

                    b.Property<int?>("FatorRh");

                    b.Property<int>("Genero");

                    b.Property<string>("NomeCompleto")
                        .IsRequired();

                    b.Property<int?>("TipoSanguineo");

                    b.Property<DateTime>("UltimaDoacao");

                    b.HasKey("IdDoador");

                    b.ToTable("Doadores");
                });

            modelBuilder.Entity("Domain.Models.ImpedimentosDefinitivos", b =>
                {
                    b.Property<int>("IdImpedimentosDefinitivos")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("AntecedenteAvc");

                    b.Property<bool?>("HepatiteB");

                    b.Property<bool?>("HepatiteC");

                    b.Property<bool?>("Hiv");

                    b.HasKey("IdImpedimentosDefinitivos");

                    b.ToTable("ImpedimentosDefinitivos");
                });

            modelBuilder.Entity("Domain.Models.ImpedimentosTemporarios", b =>
                {
                    b.Property<int>("IdImpedimentosTemporarios")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BebidaAlcoolica")
                        .IsRequired();

                    b.Property<int?>("BebidaAlcoolicaUltimaVez");

                    b.Property<int>("Gravidez");

                    b.Property<int?>("GravidezUltimaVez");

                    b.Property<bool?>("Gripe")
                        .IsRequired();

                    b.Property<int?>("GripeUltimaVez");

                    b.Property<bool?>("Tatuagem")
                        .IsRequired();

                    b.Property<int?>("TatuagemUltimaVez");

                    b.HasKey("IdImpedimentosTemporarios");

                    b.ToTable("ImpedimentosTemporarios");
                });

            modelBuilder.Entity("Domain.Models.Solicitacao", b =>
                {
                    b.Property<int>("IdSolicitacao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataSolicitacao");

                    b.Property<int?>("SolicitanteIdSolicitante");

                    b.HasKey("IdSolicitacao");

                    b.HasIndex("SolicitanteIdSolicitante");

                    b.ToTable("Solicitacoes");
                });

            modelBuilder.Entity("Domain.Models.Solicitante", b =>
                {
                    b.Property<int>("IdSolicitante")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnpj")
                        .IsRequired();

                    b.Property<string>("RazaoSocial")
                        .IsRequired();

                    b.Property<string>("Responsavel")
                        .IsRequired();

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.Property<int>("StatusUsuario");

                    b.HasKey("IdSolicitante");

                    b.ToTable("Solicitantes");

                    b.HasData(
                        new { IdSolicitante = 1, Cnpj = "11111111111111", RazaoSocial = "Solicitante 1", Responsavel = "Responsavel 1", Senha = "senhasolicitante", StatusUsuario = 1 },
                        new { IdSolicitante = 2, Cnpj = "12345678901234", RazaoSocial = "Solicitante 2", Responsavel = "Responsavel 2", Senha = "senhasolicitante", StatusUsuario = 1 },
                        new { IdSolicitante = 3, Cnpj = "33333333333333", RazaoSocial = "Solicitante 3", Responsavel = "Responsavel 3", Senha = "senhasolicitante", StatusUsuario = 0 }
                    );
                });

            modelBuilder.Entity("Domain.Models.Triador", b =>
                {
                    b.Property<int>("IdTriador")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Matricula")
                        .IsRequired();

                    b.Property<string>("NomeCompleto")
                        .IsRequired();

                    b.Property<string>("Senha")
                        .IsRequired();

                    b.Property<int>("StatusUsuario");

                    b.HasKey("IdTriador");

                    b.ToTable("Triadores");

                    b.HasData(
                        new { IdTriador = 1, Matricula = "1111111", NomeCompleto = "Triador 1", Senha = "senhatriador", StatusUsuario = 1 },
                        new { IdTriador = 2, Matricula = "1234567", NomeCompleto = "Triador 2", Senha = "senhatriador", StatusUsuario = 1 },
                        new { IdTriador = 3, Matricula = "3333333", NomeCompleto = "Triador 3", Senha = "senhatriador", StatusUsuario = 0 }
                    );
                });

            modelBuilder.Entity("Domain.Models.TriagemClinica", b =>
                {
                    b.Property<int>("IdTriagemClinica")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Peso")
                        .IsRequired();

                    b.Property<int?>("Pulso")
                        .IsRequired();

                    b.Property<int>("StatusTriagem");

                    b.Property<double?>("Temperatura")
                        .IsRequired();

                    b.HasKey("IdTriagemClinica");

                    b.ToTable("TriagensClinicas");
                });

            modelBuilder.Entity("Domain.Models.TriagemLaboratorial", b =>
                {
                    b.Property<int>("IdTriagemLaboratorial")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("HepatiteB");

                    b.Property<bool?>("HepatiteC");

                    b.Property<bool?>("Hiv");

                    b.Property<int?>("StatusTriagem");

                    b.HasKey("IdTriagemLaboratorial");

                    b.ToTable("TriagensLaboratoriais");
                });

            modelBuilder.Entity("Domain.Models.Doacao", b =>
                {
                    b.HasOne("Domain.Models.Doador", "Doador")
                        .WithMany("Doacoes")
                        .HasForeignKey("DoadorIdDoador");

                    b.HasOne("Domain.Models.ImpedimentosDefinitivos", "ImpedimentosDefinitivos")
                        .WithOne("Doacao")
                        .HasForeignKey("Domain.Models.Doacao", "IdImpedimentosDefinitivos");

                    b.HasOne("Domain.Models.ImpedimentosTemporarios", "ImpedimentosTemporarios")
                        .WithOne("Doacao")
                        .HasForeignKey("Domain.Models.Doacao", "IdImpedimentosTemporarios");

                    b.HasOne("Domain.Models.TriagemClinica", "TriagemClinica")
                        .WithOne("Doacao")
                        .HasForeignKey("Domain.Models.Doacao", "IdTriagemClinica");

                    b.HasOne("Domain.Models.TriagemLaboratorial", "TriagemLaboratorial")
                        .WithOne("Doacao")
                        .HasForeignKey("Domain.Models.Doacao", "IdTriagemLaboratorial");

                    b.HasOne("Domain.Models.Solicitacao", "Solicitacao")
                        .WithMany("Doacoes")
                        .HasForeignKey("SolicitacaoIdSolicitacao");

                    b.HasOne("Domain.Models.Triador", "Triador")
                        .WithMany("Doacoes")
                        .HasForeignKey("TriadorIdTriador");
                });

            modelBuilder.Entity("Domain.Models.Solicitacao", b =>
                {
                    b.HasOne("Domain.Models.Solicitante", "Solicitante")
                        .WithMany("Solicitacoes")
                        .HasForeignKey("SolicitanteIdSolicitante");
                });
#pragma warning restore 612, 618
        }
    }
}
