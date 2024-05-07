using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOSRS.Api.Entities;

namespace SOSRS.Api.Data
{
    public class UsuarioEntityTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);


            builder.Property(u => u.User)
                .HasColumnName("User")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("Password")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Cpf)
                .HasColumnName("Cpf")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany(u => u.Abrigos)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
