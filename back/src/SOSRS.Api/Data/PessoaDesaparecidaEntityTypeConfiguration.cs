using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Data;

public class PessoaDesaparecidaEntityTypeConfiguration : IEntityTypeConfiguration<PessoaDesaparecida>
{
    public void Configure(EntityTypeBuilder<PessoaDesaparecida> builder)
    {
        builder.ToTable("PessoasDesaparecidas");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

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

        //builder.Property(x => x.QuantidadeNecessaria)
        //     .IsRequired();
    }
}   