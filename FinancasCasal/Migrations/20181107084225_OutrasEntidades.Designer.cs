﻿// <auto-generated />
using System;
using FinancasCasal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancasCasal.Migrations
{
    [DbContext(typeof(FinancasCasalContext))]
    [Migration("20181107084225_OutrasEntidades")]
    partial class OutrasEntidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinancasCasal.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Banco");

                    b.Property<int>("CodigoAgencia");

                    b.Property<int>("CodigoConta");

                    b.Property<double>("Saldo");

                    b.Property<int>("TipoConta");

                    b.HasKey("Id");

                    b.ToTable("Conta");
                });

            modelBuilder.Entity("FinancasCasal.Models.Despesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fim");

                    b.Property<DateTime>("Inicio");

                    b.Property<string>("Nome");

                    b.Property<bool>("SemFim");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Despesa");
                });

            modelBuilder.Entity("FinancasCasal.Models.Fundo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DonoId");

                    b.Property<string>("Nome");

                    b.Property<double>("Saldo");

                    b.HasKey("Id");

                    b.HasIndex("DonoId");

                    b.ToTable("Fundo");
                });

            modelBuilder.Entity("FinancasCasal.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("FinancasCasal.Models.Transacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContaId");

                    b.Property<DateTime>("Data");

                    b.Property<int?>("DespesaId");

                    b.Property<int?>("FundoId");

                    b.Property<string>("Nome");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.HasIndex("DespesaId");

                    b.HasIndex("FundoId");

                    b.ToTable("Transacao");
                });

            modelBuilder.Entity("FinancasCasal.Models.Fundo", b =>
                {
                    b.HasOne("FinancasCasal.Models.Pessoa", "Dono")
                        .WithMany("Fundos")
                        .HasForeignKey("DonoId");
                });

            modelBuilder.Entity("FinancasCasal.Models.Transacao", b =>
                {
                    b.HasOne("FinancasCasal.Models.Conta", "Conta")
                        .WithMany("Transacoes")
                        .HasForeignKey("ContaId");

                    b.HasOne("FinancasCasal.Models.Despesa", "Despesa")
                        .WithMany("Transacoes")
                        .HasForeignKey("DespesaId");

                    b.HasOne("FinancasCasal.Models.Fundo", "Fundo")
                        .WithMany("Transacoes")
                        .HasForeignKey("FundoId");
                });
#pragma warning restore 612, 618
        }
    }
}