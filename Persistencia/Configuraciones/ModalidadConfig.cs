using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuraciones
{
    internal class ModalidadConfig : IEntityTypeConfiguration<Modalidad>
    {
        public void Configure(EntityTypeBuilder<Modalidad> builder)
        {
            builder.HasMany(p => p.Intentos)
                   .WithOne(d => d.ModalidadNavigation)
                   .HasForeignKey(d => d.IdModalidad);
        }
    }
}
