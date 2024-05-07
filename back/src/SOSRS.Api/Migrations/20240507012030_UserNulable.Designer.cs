﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOSRS.Api.Data;

#nullable disable

namespace SOSRS.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240507012030_UserNulable")]
    partial class UserNulable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SOSRS.Api.Entities.Abrigo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CapacidadeTotalPessoas")
                        .HasColumnType("int");

                    b.Property<string>("ChavePix")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("Lotado")
                        .HasColumnType("bit");

                    b.Property<string>("Observacao")
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<int?>("QuantidadeNecessariaVoluntarios")
                        .HasColumnType("int");

                    b.Property<int?>("QuantidadeVagasDisponiveis")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TipoChavePix")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Abrigos", (string)null);
                });

            modelBuilder.Entity("SOSRS.Api.Entities.Alimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbrigoId")
                        .HasColumnType("int");

                    b.Property<int?>("QuantidadeNecessaria")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbrigoId");

                    b.ToTable("Alimentos", (string)null);
                });

            modelBuilder.Entity("SOSRS.Api.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoOperacao")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Logs", (string)null);
                });

            modelBuilder.Entity("SOSRS.Api.Entities.Abrigo", b =>
                {
                    b.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Nome", b1 =>
                        {
                            b1.Property<int>("AbrigoId")
                                .HasColumnType("int");

                            b1.Property<string>("SearchableValue")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("NomeSearchable");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Nome");

                            b1.HasKey("AbrigoId");

                            b1.ToTable("Abrigos");

                            b1.WithOwner()
                                .HasForeignKey("AbrigoId");
                        });

                    b.OwnsOne("SOSRS.Api.ValueObjects.EnderecoVO", "Endereco", b1 =>
                        {
                            b1.Property<int>("AbrigoId")
                                .HasColumnType("int");

                            b1.Property<string>("Cep")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)")
                                .HasColumnName("Cep");

                            b1.Property<string>("Complemento")
                                .HasMaxLength(300)
                                .HasColumnType("nvarchar(300)")
                                .HasColumnName("Complemento");

                            b1.Property<int?>("Numero")
                                .HasColumnType("int")
                                .HasColumnName("Numero");

                            b1.HasKey("AbrigoId");

                            b1.ToTable("Abrigos");

                            b1.WithOwner()
                                .HasForeignKey("AbrigoId");

                            b1.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Bairro", b2 =>
                                {
                                    b2.Property<int>("EnderecoVOAbrigoId")
                                        .HasColumnType("int");

                                    b2.Property<string>("SearchableValue")
                                        .IsRequired()
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("BairroSearchable");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("Bairro");

                                    b2.HasKey("EnderecoVOAbrigoId");

                                    b2.ToTable("Abrigos");

                                    b2.WithOwner()
                                        .HasForeignKey("EnderecoVOAbrigoId");
                                });

                            b1.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Cidade", b2 =>
                                {
                                    b2.Property<int>("EnderecoVOAbrigoId")
                                        .HasColumnType("int");

                                    b2.Property<string>("SearchableValue")
                                        .IsRequired()
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("CidadeSearchable");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("Cidade");

                                    b2.HasKey("EnderecoVOAbrigoId");

                                    b2.ToTable("Abrigos");

                                    b2.WithOwner()
                                        .HasForeignKey("EnderecoVOAbrigoId");
                                });

                            b1.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Estado", b2 =>
                                {
                                    b2.Property<int>("EnderecoVOAbrigoId")
                                        .HasColumnType("int");

                                    b2.Property<string>("SearchableValue")
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("EstadoSearchable");

                                    b2.Property<string>("Value")
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("Estado");

                                    b2.HasKey("EnderecoVOAbrigoId");

                                    b2.ToTable("Abrigos");

                                    b2.WithOwner()
                                        .HasForeignKey("EnderecoVOAbrigoId");
                                });

                            b1.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Rua", b2 =>
                                {
                                    b2.Property<int>("EnderecoVOAbrigoId")
                                        .HasColumnType("int");

                                    b2.Property<string>("SearchableValue")
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("RuaSearchable");

                                    b2.Property<string>("Value")
                                        .HasMaxLength(150)
                                        .HasColumnType("nvarchar(150)")
                                        .HasColumnName("Rua");

                                    b2.HasKey("EnderecoVOAbrigoId");

                                    b2.ToTable("Abrigos");

                                    b2.WithOwner()
                                        .HasForeignKey("EnderecoVOAbrigoId");
                                });

                            b1.Navigation("Bairro")
                                .IsRequired();

                            b1.Navigation("Cidade")
                                .IsRequired();

                            b1.Navigation("Estado")
                                .IsRequired();

                            b1.Navigation("Rua")
                                .IsRequired();
                        });

                    b.Navigation("Endereco")
                        .IsRequired();

                    b.Navigation("Nome")
                        .IsRequired();
                });

            modelBuilder.Entity("SOSRS.Api.Entities.Alimento", b =>
                {
                    b.HasOne("SOSRS.Api.Entities.Abrigo", "Abrigo")
                        .WithMany("Alimentos")
                        .HasForeignKey("AbrigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SOSRS.Api.ValueObjects.SearchableStringVO", "Nome", b1 =>
                        {
                            b1.Property<int>("AlimentoId")
                                .HasColumnType("int");

                            b1.Property<string>("SearchableValue")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("NomeSearchable");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Nome");

                            b1.HasKey("AlimentoId");

                            b1.ToTable("Alimentos");

                            b1.WithOwner()
                                .HasForeignKey("AlimentoId");
                        });

                    b.Navigation("Abrigo");

                    b.Navigation("Nome")
                        .IsRequired();
                });

            modelBuilder.Entity("SOSRS.Api.Entities.Abrigo", b =>
                {
                    b.Navigation("Alimentos");
                });
#pragma warning restore 612, 618
        }
    }
}
