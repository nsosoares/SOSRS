using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Data;

public class AbrigoEntityTypeConfiguration : IEntityTypeConfiguration<Abrigo>
{
    public void Configure(EntityTypeBuilder<Abrigo> builder)
    {
        builder.ToTable("Abrigos");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.Endereco,
               navigationBuilder =>
               {
                   navigationBuilder
                       .OwnsOne(x => x.Rua, navigationBuilder =>
                       {
                           navigationBuilder.Property(r => r.Value)
                           .HasColumnName("Rua")
                           .HasMaxLength(150);
                           navigationBuilder.Property(r => r.SearchableValue)
                           .HasColumnName("RuaSearchable")
                           .HasMaxLength(150);
                       });
                   navigationBuilder
                       .OwnsOne(x => x.Cidade, navigationBuilder =>
                       {
                           navigationBuilder.Property(r => r.Value)
                           .HasColumnName("Cidade")
                           .HasMaxLength(150)
                           .IsRequired();
                           navigationBuilder.Property(r => r.SearchableValue)
                           .HasColumnName("CidadeSearchable")
                           .HasMaxLength(150)
                           .IsRequired();
                       });
                   navigationBuilder
                     .OwnsOne(x => x.Estado, navigationBuilder =>
                     {
                         navigationBuilder.Property(r => r.Value)
                         .HasColumnName("Estado")
                         .HasMaxLength(150);
                         navigationBuilder.Property(r => r.SearchableValue)
                         .HasColumnName("EstadoSearchable")
                         .HasMaxLength(150);
                     });
                   navigationBuilder
                     .OwnsOne(x => x.Bairro, navigationBuilder =>
                     {
                         navigationBuilder.Property(r => r.Value)
                         .HasColumnName("Bairro")
                         .HasMaxLength(150)
                         .IsRequired();
                         navigationBuilder.Property(r => r.SearchableValue)
                         .HasColumnName("BairroSearchable")
                         .HasMaxLength(150)
                         .IsRequired();
                     });
                   navigationBuilder.Property(x => x.Numero)
                         .HasColumnName("Numero");
                   navigationBuilder.Property(x => x.Complemento)
                        .HasMaxLength(300)
                        .HasColumnName("Complemento");
                   navigationBuilder.Property(x => x.Cep)
                         .HasMaxLength(10)
                         .HasColumnName("Cep");
               });


        builder.OwnsOne(x => x.Nome, navigationBuilder =>
        {
            navigationBuilder.Property(x => x.Value)
                        .HasColumnName("Nome")
                        .HasMaxLength(150)
                        .IsRequired();
            navigationBuilder.Property(x => x.SearchableValue)
                        .HasColumnName("NomeSearchable")
                        .HasMaxLength(150)
                        .IsRequired();
        });


        //builder.Property(x => x.QuantidadeVagasDisponiveis)
        //      .IsRequired();

        builder.Property(x => x.Observacao)
               .HasMaxLength(800);

        builder.Property(x => x.ChavePix)
              .HasMaxLength(150);

        builder.Property(x => x.TipoChavePix)
              .HasMaxLength(150);      
    }
}
